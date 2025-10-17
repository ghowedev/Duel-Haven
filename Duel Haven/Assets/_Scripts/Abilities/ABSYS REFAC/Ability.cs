using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField]
    public AbilityData data;
    protected Movement movement;
    protected Mana mana;
    protected State state;
    [SerializeField] protected Hitbox hitbox;

    public bool isActive { get; protected set; }
    public float endTime { get; protected set; }
    public float cooldownTimer { get; protected set; } = 0f;

    protected virtual void Awake()
    {
        movement = GetComponentInParent<Movement>();
        state = GetComponentInParent<State>();
        mana = GetComponentInParent<Mana>();
        hitbox?.Initialize(transform.root.gameObject, this);
    }

    protected virtual void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanCast()
    {
        return cooldownTimer <= 0f
            && mana.current >= data.cost
            && state.canCast;
    }

    public virtual void Use(InputData input)
    {
        mana.Consume(data.cost);
    }

    protected virtual void CleanUp()
    {
        if (hitbox) hitbox.SetActive(false);
        // Event.Publish(new AbilityCancelEvent(data.abilityID))?
    }

    public virtual void Interrupt() { }

    public virtual void ResolveHit(Collider2D other)
    {
        var caster = transform.root.gameObject;
        var target = other.gameObject;
        var payload = new Payload();

        foreach (var effect in data.effectData)
            if (effect is ICombatEffect combatEffect)
                payload.effects.Add(combatEffect);

        CombatController.Instance.ResolveHit(payload, caster, target, this);
    }
}

