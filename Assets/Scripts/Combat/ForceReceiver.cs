using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    // 0: heavy metal, 10: feather
    [SerializeField] private float drag = 0.3f;

    private CharacterController controller;
    private NavMeshAgent agent;

    // damping = Á¦µ¿
    private Vector3 dampingVelocity;
    private Vector3 currPos;
    // y velocity (updown)
    private float verticalVelocity;

    public Vector3 Movement => currPos + Vector3.up * verticalVelocity;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // If verticalVelocity is less than 0 and player is grounded,
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            // verticalVelocity equals to y gravity
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        // If verticalVelocity is bigger than 0 or player is not grounded,
        else
        {
            // verticalVelocity is decreased by y gravity (It's decreased bc Physics.gravity.y = -9.81)
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // SmoothDamp(Vector3 currentPos, Vector3 targetPos, ref float currentVelocityWhenIt'sCalled, float timeFromCurrentToTarget)
        // : This will make the transition from currentPos to targetPos smooth by timeFromCurrentToTarget.
        currPos = Vector3.SmoothDamp(currPos, Vector3.zero, ref dampingVelocity, drag);

        // Enemies will go through this code
        if (agent != null)
        {
            // If currentPos' length^2 is less then 0.2^2,
            if (currPos.sqrMagnitude < 0.2f * 0.2f)
            {
                // Adjust currentPos as Vector3.zero
                currPos = Vector3.zero;
                // Now you can use NavMesh agent again
                agent.enabled = true;
            }
        }
    }

    // Add force to move
    public void AddForce(Vector3 force)
    {
        // Add force to the currPos
        currPos += force;
        
        // If it's a NavMesh agent, 
        if (agent != null)
        {
            // Turn off the agent (This will be enabled in Update() soon)
            agent.enabled = false;
        }
    }

    public void Jump(float jumpForce)
    {
        // jumpForce will be added to verticalVelocity => player goes up
        verticalVelocity += jumpForce;
    }
}
