using UnityEngine;

[CreateAssetMenu(menuName = "Combat Effect/Stun")]
public class StunEffect : ScriptableObject, ICombatEffect
{
    [SerializeField]
    public float duration;
    public bool interrupts => false;

    public void Apply(GameObject caster, GameObject target, Ability source)
    {
        target.GetComponent<AbilityController>().InterruptAll();
        target.GetComponent<State>().EnterStun(duration);
    }
}
