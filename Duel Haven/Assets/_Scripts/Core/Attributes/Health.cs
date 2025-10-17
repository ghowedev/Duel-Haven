using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float max { get; private set; } = 200f;
    public float current { get; private set; }
    public event Action<float> HealthChanged;

    void Awake()
    {
        current = max;
        HealthChanged?.Invoke(current);
    }

    public void TakeDamage(float damage)
    {
        current = Mathf.Clamp(current - damage, 0, max);
        HealthChanged?.Invoke(current);

        if (current == 0) Die();
    }

    public void Heal(float heal)
    {
        current = Mathf.Clamp(current + heal, 0, max);
        HealthChanged?.Invoke(current);
    }

    public void Die()
    {
        Debug.Log("Dead");
    }
}
