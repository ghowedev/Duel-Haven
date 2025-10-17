using UnityEngine;
using System.Collections.Generic;

public abstract class Hitbox : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected GameObject hitboxGO;
    protected Ability sourceAbility;
    public GameObject caster;
    protected HashSet<GameObject> hitTargets = new HashSet<GameObject>();

    public void Initialize(GameObject caster, Ability sourceAbility)
    {
        this.caster = caster;
        this.sourceAbility = sourceAbility;
        this.hitboxGO = transform.gameObject;
        SetActive(false);
    }

    public virtual void SetColor(Color color)
    {
        if (spriteRenderer != null)
        {
            var c = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, c.a);
        }
    }

    public virtual void SetOpacity(float opacity)
    {
        if (spriteRenderer != null)
        {
            var c = spriteRenderer.color;
            spriteRenderer.color = new Color(c.r, c.g, c.b, opacity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.gameObject;

        if (target == caster) return;
        if (!hitTargets.Add(target)) return;

        sourceAbility.ResolveHit(other);
    }

    void OnEnable()
    {
        hitTargets.Clear();
    }
    void OnDisable()
    {
        hitTargets.Clear();
    }

    public abstract void SetActive(bool setActive);
}
