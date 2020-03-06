using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashState : IMoveState
{
    private float movementSpeed;
    //TODO: MovementInfo type for smash
    private float lastPosX;
    private float lastPosY;
    private float smashTime;
    private float elapsedTime;
    private bool isTimeOver;
    private float borderTime;

    private Animator animator;
    private int isRunningHash = Animator.StringToHash("IsRunning");
    private int isAttackingMelee = Animator.StringToHash("IsAttackingMelee");

    private bool concurrentCanRun;
    private IMove characterController;
    private IMovementAndMeleeCombatLogic controller;
    private EMovementID currentID;

    public IMovementAndMeleeCombatLogic Control { get { return this.controller; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public EMovementID StateType { get { return this.currentID; } }

    //TODO: movementInfo for the speed
    public SmashState(bool concurrentCanRun, float movementSpeed, float smashTime,
        ref IMove characterController, ref IMovementAndMeleeCombatLogic input, EMovementID currentID, ref Animator animator)
    {
        this.concurrentCanRun = concurrentCanRun;
        this.characterController = characterController;
        this.controller = input;
        this.currentID = currentID;
        this.movementSpeed = movementSpeed;
        this.smashTime = smashTime;
        this.isTimeOver = false;
        this.borderTime = smashTime / 1.1f;
        this.animator = animator;
    }

    public EMovementID CheckTransition()
    {
        if (isTimeOver)
        {
            return EMovementID.Walking;
        }
        else if (controller.InputInfo.IsFalling)
        {
            return EMovementID.Falling;
        }
        else
        {
            return this.currentID;
        }
    }

    public void Enter()
    {
        lastPosX = controller.InputInfo.MovementPosX;
        lastPosY = controller.InputInfo.MovementPosY;
        isTimeOver = false;
        this.animator.SetBool(isAttackingMelee, true);
        this.animator.SetBool(isRunningHash, true);
    }

    public void Update(float time)
    {
        if (elapsedTime < smashTime)
        {
            float currentSpeed = elapsedTime < borderTime ? movementSpeed : movementSpeed * (time * smashTime / smashTime);
            characterController.Move(lastPosX, lastPosY, currentSpeed, time);
            elapsedTime += time;
        }
        else
        {
            elapsedTime = 0;
            isTimeOver = true;
        }
    }

    public void Exit()
    {
        this.animator.SetBool(isAttackingMelee, false);
        this.animator.SetBool(isRunningHash, false);
    }

    public void Init()
    {
        // No init funtcion needed
    }
}
