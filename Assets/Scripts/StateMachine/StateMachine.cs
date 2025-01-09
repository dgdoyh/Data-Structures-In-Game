using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    public void SwitchState(State newState)
    {
        // Exit from the currentState
        currentState?.Exit();
        // Assign a newState to currentState
        currentState = newState;
        // Enter to the currentState
        currentState?.Enter();
    }

    private void Update()
    {
        // Implement Tick(Time.deltaTime) of the currentState
        currentState?.Tick(Time.deltaTime);
    }
}
