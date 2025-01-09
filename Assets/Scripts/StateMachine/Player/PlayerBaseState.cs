using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine playerStateMachine;
    
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.playerStateMachine = stateMachine;
    }

    public override void Enter() { }
    public override void Tick(float deltaTime) { }
    public override void Exit() { }


    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    // Move Player
    protected void Move(Vector3 motion, float deltaTime)
    {
        // motion = vertical movement
        // Here, horizontal movement (jump) will also be implemented
        playerStateMachine.Controller.Move((motion + playerStateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void FaceTarget()
    {
        //if (playerStateMachine.Targeter.CurrentTarget == null) { return; }

        //Vector3 lookPos = playerStateMachine.Targeter.CurrentTarget.transform.position - playerStateMachine.transform.position;
        //lookPos.y = 0f;

        //playerStateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void ReturnToLocomotion()
    {
        //if (playerStateMachine.Targeter.CurrentTarget != null)
        //{
        //    playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
        //}
        //else
        //{
        //    playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        //}
    }
}
