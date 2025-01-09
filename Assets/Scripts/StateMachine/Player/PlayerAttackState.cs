using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private Attack currAttack;


    // When you create a new PlayerAttackState, get stateMachine and attackIndex
    public PlayerAttackState(PlayerStateMachine playerStateMachine, int attackIndex) : base(playerStateMachine) 
    {
        currAttack = playerStateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        // Before it starts to attack, set attack damage and knockback value based on the weapon equiped
        playerStateMachine.WeaponManager.SetAttack(currAttack.Damage, currAttack.Knockback);
        // Smoothly play attack animation with transitionDuration
        playerStateMachine.Animator.CrossFadeInFixedTime(currAttack.AnimationName, currAttack.TransitionDuration);
    }

    public override void Tick(float deltaTime) 
    {
        // Stop the player
        Move(deltaTime);

        // Face the target
        FaceTarget();

        // Get which point attack animation is playing (if it's 0.5, it's playing the middle of the animation)
        float normalizedTime = GetNormalizedTime(playerStateMachine.Animator);

        // If the current attack animation is playing, 
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            // If it passed the currAttack.ForceTime,
            if (normalizedTime >= currAttack.ForceTime)
            {
                // Apply force
                TryApplyForce();
            }

            // If there's an attack input while playing the attack animation,
            if (playerStateMachine.InputReader.IsAttacking)
            {
                // Try combo attack
                TryComboAttack(normalizedTime);
            }
        }
        // If the current attack animation is not playing,
        else
        {
            //if (playerStateMachine.Targeter.CurrentTarget != null)
            //{
            //    playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
            //}
            //else
            //{
            //    playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
            //}

            // ** remove this code when decommented upper codes **
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }

        previousFrameTime = normalizedTime;
    }
    public override void Exit() { }

    // This will apply force (to move forward little bit) to player when it attacks
    private void TryApplyForce()
    {
        // If the force is already applied, ignore
        if (alreadyAppliedForce) { return; }

        // If the force is not applied yet, add force to player to go forward little bit based on currAttack's force value
        playerStateMachine.ForceReceiver.AddForce(playerStateMachine.transform.forward * currAttack.Force);

        // Now the force is applied
        alreadyAppliedForce = true;
    }

    // Try combo attack
    private void TryComboAttack(float normalizedTime)
    {
        // If this attack's ComboStateIndex is -1, cannot do combo attack
        // Last attack of combo attacks will have -1 as its ComboStateIndex
        if (currAttack.ComboStateIndex == -1) { return; }

        // If current attack animation doesn't reach to its ComboAttackTime, cannot do combo attack
        if (normalizedTime < currAttack.ComboAttackTime) { return; }

        // If ComboStateIndex is not -1 and current attack animation reaches to the ComboAttackTime, do combo attack
        playerStateMachine.SwitchState
        (
            new PlayerAttackState
            (
                playerStateMachine,
                currAttack.ComboStateIndex
            )
        );
    }
}
