using System;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float max { get; private set; } = 200f;
    public float current { get; private set; }
    public event Action<float> ManaChanged;

    void Awake()
    {
        current = max;
        ManaChanged?.Invoke(current);
    }

    public void Consume(float amount)
    {
        current = Mathf.Clamp(current - amount, 0, max);
        ManaChanged.Invoke(current);
    }
}
