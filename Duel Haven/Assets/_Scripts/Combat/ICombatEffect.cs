using UnityEngine;

public interface ICombatEffect
{
    bool interrupts { get; }
    void Apply(GameObject caster, GameObject target, Ability source);
}