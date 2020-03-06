using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWalkState : IOrcMovementState
{
    float movementSpeed;
    bool concurrentCanRun;
    IMove orcMove;
    EOrcMovementStates currentStateID;
    IOrcMovementLogic control;

    Animator animator;
    int posXHash = Animator.StringToHash("PosX");
    int posYHash = Animator.StringToHash("PosY");
    float dampTime = 0.1f;

    public OrcWalkState(float movementSpeed, bool concurrentCanRun, ref IMove orcMove, EOrcMovementStates currentStateID, ref IOrcMovementLogic control, ref Animator animator)
    {
        this.movementSpeed = movementSpeed;
        this.concurrentCanRun = concurrentCanRun;
        this.orcMove = orcMove;
        this.currentStateID = currentStateID;
        this.control = control;
        this.animator = animator;
    }

    public IOrcMovementLogic Control { get { return this.control; } }

    public EOrcMovementStates StateType { get { return this.currentStateID; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public EOrcMovementStates CheckTransition()
    {
        if (control.InputInfo.IsDriveUp)
        {
            return EOrcMovementStates.DrivenUp;
        }
        else if (control.InputInfo.IsFalling)
        {
            return EOrcMovementStates.Falling;
        }
        return currentStateID;
    }

    public void Exit()
    {
    }

    public void Update(float time)
    {
        animator.SetFloat(posXHash, control.InputInfo.PosY, dampTime, time);
        animator.SetFloat(posYHash, control.InputInfo.PosX, dampTime, time);

        if (control.InputInfo.PosY == 0 && control.InputInfo.PosX == 0)
        {
            currentStateID = EOrcMovementStates.Idle;
            return;
        }
        else
        {
            currentStateID = EOrcMovementStates.Walking;
            orcMove.Move(control.InputInfo.PosX, control.InputInfo.PosY, movementSpeed, time);
        }
    }

    public void Enter()
    {
        // No init implemented
    }

    public void Init()
    {
        // No init implemented
    }
}
