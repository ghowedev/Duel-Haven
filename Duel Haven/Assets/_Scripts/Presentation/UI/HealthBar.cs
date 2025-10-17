using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health health;
    private Slider slider;

    void Awake()
    {
        health = GetComponentInParent<Health>();
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = health.max;
    }

    private void OnHealthChanged(float current)
    {
        slider.value = current;
    }

    void OnEnable()
    {
        if (health != null) health.HealthChanged += OnHealthChanged;
        if (health != null) OnHealthChanged(health.current);
    }

    void OnDisable()
    {
        if (health != null) health.HealthChanged -= OnHealthChanged;
    }
}