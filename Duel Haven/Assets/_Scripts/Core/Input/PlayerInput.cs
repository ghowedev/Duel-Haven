using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{
    [SerializeField] private DirectionUIController directionUI;
    public InputData current { get; private set; }

    void Update()
    {
        ReadInput();
    }

    public void ReadInput()
    {
        var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (move.sqrMagnitude > 1f) move.Normalize();
        ;
        var aim = Helpers.GetMousePlayerPosition(transform);
        var dir = Helpers.GetDirectionFromVector(aim);

        current = new InputData { move = move, aim = aim, direction = dir };

        directionUI?.SetDirection(move);
    }
}
