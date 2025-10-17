using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BER_B1 : Ability
{
    /*
    private AbilitySO currentAbilityData;
    private int stepIndex = 0;
    private AbilitySO[] abilityDatas;
    [SerializeField]
    private AbilitySO abilityData2;
    [SerializeField]
    private AbilitySO abilityData3;

    void Awake()
    {
        currentAbilityData = abilityData;
        abilityDatas = new AbilitySO[] { abilityData, abilityData2, abilityData3 };
    }

    protected override void PlayFX()
    {

    }

    public override void UpdateAbility()
    {

    }

    public override void UseAbility()
    {
        currentAbilityData = abilityDatas[stepIndex];
        ExecuteAbility();

        // stepIndex++;
        if (stepIndex == 2) stepIndex = 0;
    }

    private void ExecuteAbility()
    {
        stateManager.SetState(PlayerState.DISABLED);
        UseMana(_abilityData.manaCost);
        PlayAudio();
        StartAnimation();
        StartTimeline();
        if (hitbox) hitbox.SetActive(true);
    }

    protected override async UniTask RunAbilityTimeline(CancellationToken token)
    {
        try
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                float elapsed = Time.time - abilityStartTime;
                var t = Mathf.Clamp01(elapsed / abilityDuration);

                OnTimelineTick(t);

                if (t >= 1f) break;

                await UniTask.NextFrame(token);
            }
            stateManager.SetState(PlayerState.IDLE);
            animator.Play($"BER_Idle_{movement.currentDirection}", 0);
        }
        catch (OperationCanceledException) { }
        finally
        {
            CleanUp();
        }
    }

    protected override void CleanUp()
    {
        if (hitbox) hitbox.SetActive(false);
        // turn off Audio, VFX, etc.
    }

    public override void ReleaseAbility() { }

    protected override void PlayAudio()
    {
        // Debug.Log("Audio played");
    }

    protected override void StartAnimation()
    {
        FaceCursor();
        animator.Play(currentAnimHash, 0, 0);
    }

    public override void Interrupt()
    {
        animationCTS?.Cancel();
    }

    public override void ResolveHit(Collider2D other)
    {
        base.ResolveHit(other);
    }
    */
}
