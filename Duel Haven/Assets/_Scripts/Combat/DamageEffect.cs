using UnityEngine;

[CreateAssetMenu(menuName = "Combat Effect/Damage")]
public class DamageEffect : ScriptableObject, ICombatEffect
{
    [SerializeField]
    public int damage;
    public bool interrupts => false;

    public void Apply(GameObject caster, GameObject target, Ability source)
    {
        target.GetComponent<Health>()?.TakeDamage(damage);
    }
}
