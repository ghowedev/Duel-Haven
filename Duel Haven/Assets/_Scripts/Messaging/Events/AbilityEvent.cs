using UnityEngine;

public struct AbilityEvent
{
    public GameObject sender;
    public Directions direction;
    public string abilityID;

    public AbilityEvent(GameObject sender, string abilityID, Directions direction)
    {
        this.sender = sender;
        this.direction = direction;
        this.abilityID = abilityID;

        /*
            EventBus.Publish(new AbilityEvent(data.abilityID, movement.currentDirection))
            Play Animation
        */
    }
}