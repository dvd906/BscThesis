using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIOrcMove : IMove
{
    NavMeshAgent agent;
    Transform objTransform;
    LayerMask walkableArea;
    float maxDistance;
    CharacterController characterController;
    Vector3 moveDir;
    float currentSpeed;

    public AIOrcMove(Transform character, LayerMask walkableArea, float maxDistance,
        CharacterController characterController, NavMeshAgent agent)
    {
        this.objTransform = character;
        this.walkableArea = walkableArea;
        this.maxDistance = maxDistance;
        this.characterController = characterController;
        this.agent = agent;
    }

    public bool IsGrounded
    {
        get
        {
            return Physics.Raycast(objTransform.position, Vector3.down, maxDistance, walkableArea);
        }
    }

    public void Move(float x, float y, float movementSpeed, float time)
    {
        if (agent.speed != movementSpeed)
        {
            agent.speed = movementSpeed;
        }
        currentSpeed = movementSpeed * time;
        moveDir.y += Physics.gravity.y * time;

        moveDir.x = x * currentSpeed;
        moveDir.z = y * currentSpeed;
        if (characterController.isGrounded)
        {
            moveDir.y = 0;
        }
        characterController.Move(moveDir);
        agent.velocity = characterController.velocity;
    }

    public void Move(Vector3 movementDir, float time)
    {
        characterController.Move(movementDir * time);
        agent.velocity = characterController.velocity;
    }
}
