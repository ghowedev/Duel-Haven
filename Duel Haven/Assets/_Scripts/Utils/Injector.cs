using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
public sealed class InjectAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public sealed class ProvideAttribute : Attribute { }

public interface IDependencyProvider { }

[DefaultExecutionOrder(-1000)]
public class Injector : MonoBehaviour
{
    const BindingFlags k_bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    readonly Dictionary<Type, object> registry = new Dictionary<Type, object>();

    void Awake()
    {
        var providers = FindMonoBehaviours().OfType<IDependencyProvider>();
        foreach (var provider in providers)
        {
            Register(provider);
        }

        var injectables = FindMonoBehaviours().Where(IsInjectable);
        foreach (var injectable in injectables)
        {
            Inject(injectable);
        }

        ValidateDependencies();
    }

    void Inject(object instance)
    {
        var type = instance.GetType();
        var injectableFields = type.GetFields(k_bindingFlags)
            .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

        foreach (var injectableField in injectableFields)
        {
            var fieldType = injectableField.FieldType;
            var resolvedInstance = Resolve(fieldType);
            if (resolvedInstance == null)
            {
                throw new Exception($"Failed to inject {fieldType.Name} into {type.Name}");
            }

            injectableField.SetValue(instance, resolvedInstance);
        }

        var injectableMethods = type.GetMethods(k_bindingFlags)
            .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

        foreach (var injectableMethod in injectableMethods)
        {
            var requiredParameters = injectableMethod.GetParameters()
                .Select(requiredParameters => requiredParameters.ParameterType)
                .ToArray();
            var resolvedInstances = requiredParameters.Select(Resolve).ToArray();
            if (resolvedInstances.Any(resolvedInstances => resolvedInstances == null))
            {
                throw new Exception($"Failed to inject {type.Name}.{injectableMethod.Name}");
            }

            injectableMethod.Invoke(instance, resolvedInstances);
        }
    }

    void Register(IDependencyProvider provider)
    {
        var methods = provider.GetType().GetMethods(k_bindingFlags);

        foreach (var method in methods)
        {
            if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;

            var returnType = method.ReturnType;
            var providedInstance = method.Invoke(provider, null);
            if (providedInstance != null)
            {
                registry.Add(returnType, providedInstance);
            }
            else
            {
                throw new Exception($"Provider {provider.GetType().Name} returned null for {returnType.Name}");
            }
        }
    }

    public void ValidateDependencies()
    {
        var monoBehaviours = FindMonoBehaviours();
        var providers = monoBehaviours.OfType<IDependencyProvider>();
        var providedDependencies = GetProvidedDependencies(providers);

        var invalidDependencies = monoBehaviours
            .SelectMany(mb => mb.GetType().GetFields(k_bindingFlags), (mb, field) => new { mb, field })
            .Where(t => Attribute.IsDefined(t.field, typeof(InjectAttribute)))
            .Where(t => !providedDependencies.Contains(t.field.FieldType) && t.field.GetValue(t.mb) == null)
            .Select(t => $"[Validation] {t.mb.GetType().Name} is missing dependency {t.field.FieldType.Name} on GameObject {t.mb.gameObject.name}");

        var invalidDependencyList = invalidDependencies.ToList();

        if (!invalidDependencyList.Any())
        {
            // Debug.Log("[Validation] All dependencies are valid.");
        }
        else
        {
            Debug.LogError($"[Validation] {invalidDependencyList.Count} dependencies are invalid:");
            foreach (var invalidDependency in invalidDependencyList)
            {
                Debug.LogError(invalidDependency);
            }
        }
    }

    HashSet<Type> GetProvidedDependencies(IEnumerable<IDependencyProvider> providers)
    {
        var providedDependencies = new HashSet<Type>();
        foreach (var provider in providers)
        {
            var methods = provider.GetType().GetMethods(k_bindingFlags);

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;

                providedDependencies.Add(method.ReturnType);
            }
        }
        return providedDependencies;
    }

    public void ClearDependencies()
    {
        foreach (var monoBehaviour in FindMonoBehaviours())
        {
            var type = monoBehaviour.GetType();
            var injectableFields = type.GetFields(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableField in injectableFields)
            {
                injectableField.SetValue(monoBehaviour, null);
            }
        }

        Debug.Log("[Injector] All injectable fields cleared.");
    }

    object Resolve(Type type)
    {
        registry.TryGetValue(type, out var resolvedInstance);
        return resolvedInstance;
    }


    static MonoBehaviour[] FindMonoBehaviours()
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);
    }

    static bool IsInjectable(MonoBehaviour obj)
    {
        var members = obj.GetType().GetMembers(k_bindingFlags);
        return members.Any(members => Attribute.IsDefined(members, typeof(InjectAttribute)));
    }
}
