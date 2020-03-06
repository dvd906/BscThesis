using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IMoveState
{
    float movementSpeed;
    bool concurrentCanRun;
    IMove characterController;
    IMovementAndMeleeCombatLogic controller;
    EMovementID currentID;

    Animator animator;
    int posXHash = Animator.StringToHash("PosX");
    int posYHash = Animator.StringToHash("PosY");
    int isMovingHash = Animator.StringToHash("IsMoving");
    bool isMoving;
    float dampTime = 0.1f;

    public IMovementAndMeleeCombatLogic Control { get { return this.controller; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public EMovementID StateType { get { return this.currentID; } }

    //TODO: movementInfo for the speed
    public WalkState(bool concurrentCanRun, float movementSpeed, ref IMove characterController,
        ref IMovementAndMeleeCombatLogic input, EMovementID currentID, ref Animator animator)
    {
        this.concurrentCanRun = concurrentCanRun;
        this.characterController = characterController;
        this.controller = input;
        this.currentID = currentID;
        this.movementSpeed = movementSpeed;
        this.animator = animator;
    }

    public EMovementID CheckTransition()
    {
        if (controller.InputInfo.IsFalling)
        {
            return EMovementID.Falling;
        }
        else if (controller.InputInfo.IsRunning)
        {
            return EMovementID.Running;
        }
        else
        {
            return this.currentID;
        }
    }

    public void Enter()
    {
        // No enter function needed
    }

    public void Exit()
    {
        // No exit function needed 
    }

    public void Init()
    {
        // No init funtcion needed
    }

    public void Update(float time)
    {
        animator.SetFloat(posXHash, controller.InputInfo.MovementPosX, dampTime, time);
        animator.SetFloat(posYHash, controller.InputInfo.MovementPosY, dampTime, time);
        animator.SetBool(isMovingHash, isMoving);

        if (controller.InputInfo.MovementPosX == 0 && controller.InputInfo.MovementPosY == 0)
        {
            isMoving = false;
            this.currentID = EMovementID.Idle;
            return;
        }
        else
        {
            this.currentID = EMovementID.Walking;
            isMoving = true;
            characterController.Move(controller.InputInfo.MovementPosX, controller.InputInfo.MovementPosY, movementSpeed, time);
        }
    }
}
