using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacemarineMove : IMove
{
    CharacterController characterController;
    Transform objTransform;
    LayerMask collideWith;

    float rayMaxDistance;
    float currentSpeed = 0f;
    float currentVelocity;
    float smoothTime = 10f;
    Vector3 moveDirection;

    public SpacemarineMove(ref CharacterController characterController, ref Transform objTransform, LayerMask collideWith)
    {
        this.characterController = characterController;
        this.objTransform = objTransform;
        this.rayMaxDistance = 0.05f;
        this.moveDirection = Vector3.zero;
        this.collideWith = collideWith;
    }

    public bool IsGrounded
    {
        get
        {
            return characterController.isGrounded;
        }
    }

    public void Move(float inputX, float inputY, float movementSpeed, float time)
    {

        Vector3 desiredMove = objTransform.forward * inputY + objTransform.right * inputX;
        RaycastHit hit;
        Physics.SphereCast(objTransform.position, characterController.radius, Vector3.down, out hit, rayMaxDistance, collideWith);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hit.normal).normalized;

        moveDirection.y += Physics.gravity.y * time;

        currentSpeed = movementSpeed * time;
        moveDirection.x = desiredMove.x * currentSpeed;
        moveDirection.z = desiredMove.z * currentSpeed;

        if (characterController.isGrounded)
        {
            moveDirection.y = 0;
        }

        characterController.Move(moveDirection);
    }

    public void Move(Vector3 movementDir, float time)
    {
        characterController.Move(time * movementDir); // use physics properties
    }
}
