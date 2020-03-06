using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IMoveState
{
    private float movementSpeed;
    private bool concurrentCanRun;
    private IMove characterController;
    private IMovementAndMeleeCombatLogic controller;
    private EMovementID currentID;

    private Animator animator;
    private int isRunningHash = Animator.StringToHash("IsRunning");
    private int posYHash = Animator.StringToHash("PosY");

    public IMovementAndMeleeCombatLogic Control { get { return this.controller; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public EMovementID StateType { get { return this.currentID; } }

    //TODO: movementInfo for the speed
    public RunState(bool concurrentCanRun, float movementSpeed,
        ref IMove characterController, ref IMovementAndMeleeCombatLogic input,
        EMovementID currentID, ref Animator animator)
    {
        this.animator = animator;
        this.concurrentCanRun = concurrentCanRun;
        this.characterController = characterController;
        this.controller = input;
        this.currentID = currentID;
        this.movementSpeed = movementSpeed;
    }

    public EMovementID CheckTransition()
    {
        if (controller.InputInfo.IsFalling)
        {
            return EMovementID.Falling;
        }
        else if (!controller.InputInfo.IsRunning)
        {
            return EMovementID.Walking;
        }
        else if (controller.InputInfo.IsRunning && controller.InputInfo.IsAttackingMelee)
        {
            return EMovementID.Smashing;
        }
        else
        {
            return this.currentID;
        }
    }

    public void Enter()
    {
        animator.SetBool(isRunningHash, true);

    }

    public void Exit()
    {
        animator.SetBool(isRunningHash, false);
    }

    public void Init()
    {
        // No init funtcion needed
    }

    public void Update(float time)
    {
        characterController.Move(controller.InputInfo.MovementPosX, controller.InputInfo.MovementPosY, movementSpeed, time);
    }
}
