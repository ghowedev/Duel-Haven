using UnityEditorInternal;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    string characterID;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterID = "BER";
    }

    void OnEnable()
    {
        EventBus.Subscribe<MovementEvent>(OnMovementEvent);
        EventBus.Subscribe<AbilityEvent>(OnAbilityEvent);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<MovementEvent>(OnMovementEvent);
        EventBus.Unsubscribe<AbilityEvent>(OnAbilityEvent);

    }

    public void Animate(string state, Directions dir)
    {
        string hash = $"{state}_{dir}";
        animator.Play(hash);
    }

    private void OnMovementEvent(MovementEvent evt)
    {
        if (evt.sender != gameObject) return;
        string state = evt.isMoving ? "Walk" : "Idle";
        string hash = $"{characterID}_{state}_{evt.direction}";
        animator.Play(hash, 0, 0);
    }

    private void OnAbilityEvent(AbilityEvent evt)
    {
        if (evt.sender.transform.root.gameObject != gameObject) return;
        string hash = $"{evt.abilityID}_{evt.direction}";
        animator.Play(hash, 0, 0);
    }
}