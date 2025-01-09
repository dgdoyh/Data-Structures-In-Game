using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    // All the classes inherite State have to override these methods.
    // Function for when entering to a State
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    // Functino for when exiting from a State
    public abstract void Exit();

    // Get normalized time of an animator
    protected float GetNormalizedTime(Animator animator)
    {
        // Current Animation info
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Next Animation info
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);
        
        // If there's a transition on layer 0 and next animation's tag is "Attack",
        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            // Return normalizedTime of the next animation
            return nextInfo.normalizedTime;
        }
        // If there's no transition on layer 0 and current animation's tag is "Attack",
        else if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            // Return normalizedTime of the current animation (if it's 0.5, it's playing the middle of the animation)
            return currentInfo.normalizedTime;
        }
        // In all the other cases,
        else
        {
            // return 0
            return 0f;
        }
    }
}
