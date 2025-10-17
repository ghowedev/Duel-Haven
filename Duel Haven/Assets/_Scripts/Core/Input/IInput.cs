using UnityEngine;

public interface IInput
{
    InputData current { get; }
    void ReadInput();
}

public struct InputData
{
    public Vector2 move;
    public Vector2 aim;
    public Directions direction;
}