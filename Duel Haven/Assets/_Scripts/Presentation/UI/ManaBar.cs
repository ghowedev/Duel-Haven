using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour
{
    private Mana mana;
    private Slider slider;
    void Awake()
    {
        mana = GetComponentInParent<Mana>();
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = mana.max;
    }

    private void OnManaChanged(float current)
    {
        slider.value = current;
    }

    void OnEnable()
    {
        if (mana != null) mana.ManaChanged += OnManaChanged;
        if (mana != null) OnManaChanged(mana.current);
    }

    void OnDisable()
    {
        if (mana != null) mana.ManaChanged -= OnManaChanged;
    }
}
