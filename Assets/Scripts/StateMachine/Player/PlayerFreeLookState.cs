using UnityEngine;


// This inherites PlayerBaseState(:State)
public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    // Use ID instead of animation name to reduce the risk of typo
    // Get FreeLookBlenTree(blend tree)'s ID
    private int FreeLookBlendTreeID = Animator.StringToHash("Free Look Blend Tree");
    // Get FreeLookSpeed(parameter)'s ID
    private int FreeLookSpeedID = Animator.StringToHash("FreeLookSpeed");

    // Time taken for transition between two animations (for SetFloat())
    private const float AnimatorDampTime = 0.1f;
    // Time taken for transition between two animations (to start an animation)
    private const float CrossFadeDuration = 0.1f;

    public override void Enter() 
    {
        // OnTarget() and OnJump() subscribes events for each function
        playerStateMachine.InputReader.TargetEvent += OnTarget;
        playerStateMachine.InputReader.JumpEvent += OnJump;

        // Do transition to currAnimation -> FreeLookBlendTree with CrossFadeDuration smoothly
        playerStateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeID, CrossFadeDuration);
    }

    public override void Exit() 
    {
        playerStateMachine.InputReader.TargetEvent -= OnTarget;
        playerStateMachine.InputReader.JumpEvent -= OnJump;
    }

    public override void Tick(float deltaTime) 
    {
        // If there's an attack input,
        if (playerStateMachine.InputReader.IsAttacking)
        {
            // Switch current state to PlayerAttackState
            playerStateMachine.SwitchState(new PlayerAttackState(playerStateMachine, 0));

            return;
        }

        // movement = normalized forward and right value
        Vector3 movement = CalculateMovement();

        // Call Move(verticalMove * speed, deltaTime)
        Move(movement * playerStateMachine.FreeLookMovementSpeed, deltaTime);

        // If there's no movement input,
        if (playerStateMachine.InputReader.MovementValue == Vector2.zero)
        {
            // Set FreeLookSpeed value smoothly
            // AnimatorDampTime = how long it takes for the transition
            playerStateMachine.Animator.SetFloat(FreeLookSpeedID, 0, AnimatorDampTime, deltaTime);
            return;
        }

        // If there's a movement input,
        // Set FreeLookSpeed value smoothly
        playerStateMachine.Animator.SetFloat(FreeLookSpeedID, 1, AnimatorDampTime, deltaTime);

        // Player faces its movement direction
        FaceMovementDirection(movement, deltaTime);
    }

    private void OnTarget()
    {
        //if (!playerStateMachine.Targeter.SelectTarget()) { return; }

        //playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
    }

    private void OnJump()
    {
        //playerStateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        // Quaternion.Lerp(Quaternion currRotation, Quaternion goalRotation, float t_normalizedFloatBetweenCurrAndGoal)
        // if t = 0, it returns currRotation,
        // if t = 1, it returns goalRotation,
        // if t = 0.5, it returns middle rotation between them.
        // This is used to make the rotation smooth.
        playerStateMachine.transform.rotation = Quaternion.Lerp(
            // current rotation
            playerStateMachine.transform.rotation,
            // goal rotation
            Quaternion.LookRotation(movement),
            // this affets how fast player rotates
            deltaTime * playerStateMachine.RotationDamping);
    }

    private Vector3 CalculateMovement()
    {
        // Player's forward will be the same as MainCamera's forward
        Vector3 forward = playerStateMachine.MainCameraTransform.forward;
        // Player's right(1,0,0) will be the same as MainCamera's right
        Vector3 right = playerStateMachine.MainCameraTransform.right;

        // y doesn't change
        forward.y = 0f;
        right.y = 0f;

        // Normalize player's forward and right value
        forward.Normalize();
        right.Normalize();

        // Return normalized forward and right value when movement input is detected
        return forward * playerStateMachine.InputReader.MovementValue.y +
            right * playerStateMachine.InputReader.MovementValue.x;
    }
}
