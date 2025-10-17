using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    protected Dictionary<KeyCode, Ability> abilityBindings = new Dictionary<KeyCode, Ability>();
    private IInput input;

    void Awake()
    {
        var abilities = GetComponentsInChildren<Ability>();
        input = GetComponent<IInput>();

        foreach (var ability in abilities)
        {
            var key = ability.data.keybind;
            if (key != KeyCode.None && !abilityBindings.ContainsKey(key)) abilityBindings[key] = ability;
        }
    }

    protected virtual void Update()
    {
        foreach (var binding in abilityBindings)
        {
            if (!binding.Value.CanCast()) continue;

            if (Input.GetKeyDown(binding.Key))
            {
                binding.Value.Use(input.current);
            }
        }
    }

    public virtual void InterruptAll()
    {

    }
}