using UnityEngine;

public class State : MonoBehaviour
{
    #region Variables
    private SpriteRenderer stunSprite;

    public PlayerState state { get; private set; } = PlayerState.IDLE;
    public bool canMove { get; private set; } = true;
    public bool canCast { get; private set; } = true;

    public bool stunActive { get; private set; } = false;
    public struct StunContext { public float startTime, endTime; }
    public StunContext stunContext { get; private set; }

    public bool knockbackActive { get; private set; } = false;
    public struct KnockbackContext { public float startTime, endTime; public Vector2 startPos, direction; }
    public KnockbackContext knockbackContext { get; private set; }
    #endregion

    void Awake()
    {
        stunSprite = transform.Find("EffectVisuals/StunVisual").GetComponent<SpriteRenderer>();
        if (stunSprite) stunSprite.enabled = false;
    }

    void Update()
    {
        if (stunActive && Time.time >= stunContext.endTime)
            ExitStun();

        if (knockbackActive && Time.time >= knockbackContext.endTime)
            ExitKnockback();
    }

    public void Set(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                canMove = true;
                canCast = true;
                break;
            case PlayerState.CHANNELING:
                canMove = true;
                canCast = false;
                break;
            case PlayerState.CANCELABLE:
                canMove = false;
                canCast = true;
                break;
            case PlayerState.DISABLED:
                canMove = false;
                canCast = false;
                break;
        }
    }

    public void EnterKnockback(float duration, Vector2 direction)
    {
        knockbackContext = new KnockbackContext
        {
            startTime = Time.time,
            endTime = Time.time + duration,
            direction = direction,
            startPos = transform.position
        };
        knockbackActive = true;
        if (stunSprite) stunSprite.enabled = true;
        Set(PlayerState.DISABLED);
    }

    public void ExitKnockback()
    {
        knockbackContext = default;
        knockbackActive = false;
        if (stunSprite) stunSprite.enabled = false;
        Set(PlayerState.IDLE);
    }

    public void EnterStun(float duration)
    {
        stunContext = new StunContext { startTime = Time.time, endTime = Time.time + duration };
        stunActive = true;
        if (stunSprite) stunSprite.enabled = true;
        Set(PlayerState.DISABLED);
    }

    public void ExitStun()
    {
        stunContext = default;
        stunActive = false;
        if (stunSprite) stunSprite.enabled = false;
        Set(PlayerState.IDLE);
    }
}