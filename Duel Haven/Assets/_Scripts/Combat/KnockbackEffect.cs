using UnityEngine;

[CreateAssetMenu(menuName = "Combat Effect/Knockback")]
public class KnockbackEffect : ScriptableObject, ICombatEffect
{
    public float duration;
    public bool interrupts => true;

    public void Apply(GameObject caster, GameObject target, Ability source)
    {
        // target.GetComponent<AbilityController>().InterruptAll();
        Vector2 direction = (target.transform.position - caster.transform.position).normalized;
        target.GetComponent<State>().EnterKnockback(duration, direction);
    }
}