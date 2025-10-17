using UnityEngine;

public class CombatController : MonoBehaviour
{
    public static CombatController Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ResolveHit(Payload payload, GameObject caster, GameObject target, Ability source)
    {
        if (payload == null || target == null || source == null) return;

        foreach (var effect in payload.effects)
        {
            effect.Apply(caster, target, source);
        }
    }
}