using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcFallState : IOrcMovementState
{
    bool concurrentCanRun;
    IMove orcMove;
    EOrcMovementStates currentStateID;
    IOrcMovementLogic control;

    float lastMoveMultiplier;
    Vector3 gravityVector;
    bool isFirstFall;

    Animator animator;
    int isFallingHash = Animator.StringToHash("IsFalling");

    public OrcFallState(float lastMoveMultiplier, Vector3 fallVector, bool concurrentCanRun,
        ref IMove orcMove, EOrcMovementStates currentStateID, ref IOrcMovementLogic control, ref Animator animator)
    {
        this.lastMoveMultiplier = lastMoveMultiplier;
        this.gravityVector = fallVector;
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
        if (!control.InputInfo.IsFalling)
        {
            return EOrcMovementStates.Walking;
        }
        return this.currentStateID;
    }

    public void Enter()
    {
        isFirstFall = true;
        animator.SetBool(isFallingHash, true);
        gravityVector.x = lastMoveMultiplier * control.InputInfo.PosX;
        gravityVector.z = lastMoveMultiplier * control.InputInfo.PosY;
    }

    public void Exit()
    {
        animator.SetBool(isFallingHash, false);
    }

    public void Init()
    {
        // No init function implemented
    }

    public void Update(float time)
    {
        if (isFirstFall)
        {
            isFirstFall = false;
            orcMove.Move(gravityVector, time);
            gravityVector.x = 0;
            gravityVector.z = 0;
        }
        else
        {
            orcMove.Move(gravityVector, time);
        }
    }
}
