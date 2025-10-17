using UnityEngine;

public struct MovementEvent
{
    public GameObject sender;
    public bool isMoving;
    public Directions direction;

    public MovementEvent(GameObject sender, bool isMoving, Directions direction)
    {
        this.sender = sender;
        this.isMoving = isMoving;
        this.direction = direction;
    }
}