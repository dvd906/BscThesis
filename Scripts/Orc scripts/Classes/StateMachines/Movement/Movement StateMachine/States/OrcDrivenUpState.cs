using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcDrivenUpState : IOrcMovementState
{
    float timeToWaitForWakeUp;
    bool concurrentCanRun;
    IMove orcMove;
    EOrcMovementStates currentStateID;
    IOrcMovementLogic control;

    float elapsedTime;
    float speedofDrivenUp;
    Vector3 velocityDrivenUp;

    Animator animator;
    int isDriveUpHash = Animator.StringToHash("IsDriveUp");

    public OrcDrivenUpState(float speedOfDrivenUp, float timeToWaitForWakeUp, bool concurrentCanRun,
        ref IMove orcMove, EOrcMovementStates currentStateID, ref IOrcMovementLogic control)
    {
        this.speedofDrivenUp = speedOfDrivenUp;
        this.timeToWaitForWakeUp = timeToWaitForWakeUp;
        this.concurrentCanRun = concurrentCanRun;
        this.orcMove = orcMove;
        this.currentStateID = currentStateID;
        this.control = control;
        this.elapsedTime = 0;
    }

    public IOrcMovementLogic Control { get { return this.control; } }

    public EOrcMovementStates StateType { get { return this.currentStateID; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public EOrcMovementStates CheckTransition()
    {
        if (elapsedTime >= timeToWaitForWakeUp)
        {
            return EOrcMovementStates.GetUp;
        }
        return currentStateID;
    }

    public void Enter()
    {
        elapsedTime = 0;
        velocityDrivenUp = control.InputInfo.DriveUpDirection * speedofDrivenUp;
        animator.SetBool(isDriveUpHash, true);
    }

    public void Exit()
    {
        animator.SetBool(isDriveUpHash, false);
    }

    public void Init()
    {
        // No init implemented
    }

    public void Update(float time)
    {
        elapsedTime += time;
        orcMove.Move(velocityDrivenUp, time);
    }
}
