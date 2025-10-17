using UnityEngine;

public class CircleHitbox : Hitbox
{
    [Range(0.1f, 5f)]
    public float testRadius = 1f;

    [Range(0.1f, 5f)]
    public float testOpacity = 0.5f;

    private CircleCollider2D circleCollider;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        Sync();
    }

    void OnValidate()
    {
        if (!circleCollider) circleCollider = GetComponent<CircleCollider2D>();
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        Sync();
    }

    void Update()
    {
        Sync();
    }

    void Sync()
    {
        SetRadius(testRadius);
        SetOpacity(testOpacity);
    }

    public void SetRadius(float radius)
    {
        circleCollider.radius = radius / transform.lossyScale.x;

        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            float spriteDiameter = Mathf.Max(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
            float scale = (radius * 2f) / spriteDiameter;
            spriteRenderer.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    public override void SetActive(bool isActive)
    {
        hitboxGO.SetActive(isActive);
    }
}