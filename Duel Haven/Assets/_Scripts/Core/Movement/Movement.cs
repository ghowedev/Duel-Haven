using UnityEngine;

[RequireComponent(typeof(State))]
public class Movement : MonoBehaviour
{
    private bool isMoving = false;
    private bool wasMoving = false;
    private float isMovingThreshold = 0.2f;

    public Directions currentDirection { get; private set; }
    private Directions previousDirection;

    [SerializeField] private float HYSTERESIS = 7.5f;

    private IInput input;
    private AnimationController animationController;
    private State state;
    private Constants constants;

    [SerializeField] private float moveSpeed = 2.5f;
    public float MoveSpeed => moveSpeed;

    [SerializeField] private AnimationCurve knockbackDecay;

    void Awake()
    {
        input = GetComponent<IInput>();
        animationController = GetComponent<AnimationController>();
        state = GetComponent<State>();
        constants = Resources.Load<Constants>("Constants");
    }

    void Update()
    {
        isMoving = input.current.move.sqrMagnitude > isMovingThreshold;

        if (!state.canMove)
        {
            isMoving = false;
            wasMoving = false;
            if (state.knockbackActive) MovePlayerKnockback();
            return;
        }

        if (input.current.move != Vector2.zero) MovePlayer();

        Directions newDirection = Helpers.GetDirectionFromVector(input.current.move);
        if (isMoving && DirectionChanged())
            Face(newDirection);

        if (isMoving != wasMoving || currentDirection != previousDirection)
        {
            EventBus.Publish(new MovementEvent(gameObject, isMoving, currentDirection));
        }

        wasMoving = isMoving;
        previousDirection = currentDirection;
    }

    private void MovePlayer()
    {
        transform.position += (Vector3)(input.current.move * moveSpeed * Time.deltaTime);
    }

    private void MovePlayerKnockback()
    {
        var ctx = state.knockbackContext;
        float t = Mathf.InverseLerp(ctx.startTime, ctx.endTime, Time.time);

        if (t <= 0f || t >= 1f) return;

        Vector2 newPos = ctx.startPos + ctx.direction * constants.knockbackCurve.Evaluate(t);

        transform.position = newPos;
    }

    public void Face(Directions dir)
    {
        if (dir == currentDirection) return;
        currentDirection = dir;
    }

    private bool DirectionChanged()
    {
        Vector2 currentDirectionVector = Helpers.GetVectorFromDirection(currentDirection);
        float angleDifference = Vector2.Angle(currentDirectionVector, input.current.move);

        if (angleDifference > 22.5 + HYSTERESIS)
        {
            return true;
        }

        return false;
    }
}
