using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance { get; private set; }

    private static Dictionary<Type, Delegate> events = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void Publish<T>(T evt)
    {
        if (events.TryGetValue(typeof(T), out var del))
        {
            if (del is Action<T> action)
            {
                action.Invoke(evt);
            }
            else
            {
                Debug.LogError($"EventBus: Delegate for {typeof(T)} is not of expected type {typeof(Action<T>)}.");
            }
        }
    }

    public static void Subscribe<T>(Action<T> listener)
    {
        if (events.TryGetValue(typeof(T), out var del))
        {
            events[typeof(T)] = Delegate.Combine(del, listener);
        }
        else
        {
            events[typeof(T)] = listener;
        }
    }

    public static void Subscribe<T>(Action listener)
    {
        Action<T> wrapper = _ => listener();
        Subscribe(wrapper);
    }

    public static void Unsubscribe<T>(Action<T> listener)
    {
        if (events.TryGetValue(typeof(T), out var del))
        {
            var currentDel = Delegate.Remove(del, listener);
            if (currentDel == null) events.Remove(typeof(T));
            else events[typeof(T)] = currentDel;
        }
    }

    public static void Unsubscribe<T>(Action listener)
    {
        Action<T> wrapper = _ => listener();
        Unsubscribe(wrapper);
    }
}
