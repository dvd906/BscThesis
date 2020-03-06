using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IMoveState
{
    bool concurrentCanRun;
    IMove characterController;
    IMovementAndMeleeCombatLogic controller;
    EMovementID currentID;

    float lastMoveMultiplier;
    Vector3 gravityVector;
    bool isFirstFall;

    Animator animator;
    int isFallinghash = Animator.StringToHash("IsFalling");

    public IMovementAndMeleeCombatLogic Control { get { return this.controller; } }

    public EMovementID StateType { get { return this.currentID; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public FallState(float lastMoveMultiplier, Vector3 fallVector, bool concurrentCanRun, ref IMove characterController,
        ref IMovementAndMeleeCombatLogic controller, EMovementID currentID, ref Animator animator)
    {
        this.lastMoveMultiplier = lastMoveMultiplier;
        this.gravityVector = fallVector;
        this.concurrentCanRun = concurrentCanRun;
        this.characterController = characterController;
        this.controller = controller;
        this.currentID = currentID;
        this.animator = animator;
    }

    public EMovementID CheckTransition()
    {
        if (!controller.InputInfo.IsFalling)
        {
            return EMovementID.Landing;
        }
        else
        {
            return this.currentID;
        }
    }

    public void Enter()
    {
        isFirstFall = true;
        animator.SetBool(isFallinghash, true);
        gravityVector.x = lastMoveMultiplier * controller.InputInfo.MovementPosX;
        gravityVector.z = lastMoveMultiplier * controller.InputInfo.MovementPosY;
    }

    public void Exit()
    {
        animator.SetBool(isFallinghash, false);
    }

    //No init function needed
    public void Init()
    {

    }

    public void Update(float time)
    {
        if (isFirstFall)
        {
            isFirstFall = false;
            characterController.Move(gravityVector, time);
            gravityVector.x = 0;
            gravityVector.z = 0;
        }
        else
        {
            characterController.Move(gravityVector, time);
        }

    }
}
