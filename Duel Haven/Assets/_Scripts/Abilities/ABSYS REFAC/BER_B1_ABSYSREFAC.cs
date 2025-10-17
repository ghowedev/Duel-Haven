using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BER_B1_ABSYSREFAC : Ability
{
    // private int stepIndex = 0;
    private AbilityData currentAbilityData;

    /*
    private AbilityData[] abilityDatas;
    [SerializeField]
    private AbilityData abilityData1;
    [SerializeField]
    private AbilityData abilityData2;
    [SerializeField]
    private AbilityData abilityData3;
    */

    protected override void Awake()
    {
        base.Awake();
        currentAbilityData = data;
        // abilityDatas = new AbilityData[] { abilityData1, abilityData2, abilityData3 };
    }

    protected override void Update()
    {
        base.Update();

        if (isActive && Time.time >= endTime)
        {
            CleanUp();
            isActive = false;
        }
    }

    public override void Use(InputData input)
    {
        mana.Consume(data.cost);
        movement.Face(input.direction);
        state.Set(PlayerState.DISABLED);
        hitbox.SetActive(true);

        EventBus.Publish(new AbilityEvent(gameObject, data.abilityID, movement.currentDirection));

        isActive = true;
        endTime = Time.time + data.duration;
    }

    protected override void CleanUp()
    {
        if (hitbox) hitbox.SetActive(false);
        state.Set(PlayerState.IDLE);
        EventBus.Publish(new MovementEvent(gameObject, false, movement.currentDirection));
    }

    public override void Interrupt()
    {
    }
}
