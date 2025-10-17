using UnityEngine;

public class AIInput : MonoBehaviour, IInput
{
    [SerializeField] private DirectionUIController directionUI;
    public InputData current { get; private set; }

    void Start()
    {
        ReadInput();
    }

    public void ReadInput()
    {
        var move = Vector2.zero;
        var aim = Vector2.zero;
        var dir = Directions.Down;

        current = new InputData { move = move, aim = aim, direction = dir };
    }
}
