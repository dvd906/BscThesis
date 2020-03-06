using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : IMoveState
{
    bool concurrentCanRun;
    IMovementAndMeleeCombatLogic controller;
    IMove characterController;
    EMovementID currentID;
    float landingTime;

    float elapsedTime;
    bool isElapsed;

    public IMovementAndMeleeCombatLogic Control { get { return this.controller; } }

    public EMovementID StateType { get { return this.currentID; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public LandState(float landingTime, bool concurrentCanRun,
        ref IMovementAndMeleeCombatLogic controller, ref IMove move, EMovementID currentID)
    {
        this.landingTime = landingTime;
        this.concurrentCanRun = concurrentCanRun;
        this.controller = controller;
        this.currentID = currentID;
        this.characterController = move;
    }


    public void Enter()
    {
        elapsedTime = 0.0f;
        isElapsed = false;
    }

    public void Update(float time)
    {
        elapsedTime += time;
        if (elapsedTime > landingTime)
        {
            isElapsed = true;
        }
        this.characterController.Move(0, 0, 0, time);
    }

    public EMovementID CheckTransition()
    {
        if (isElapsed)
        {
            return EMovementID.Walking;
        }
        else
        {
            return this.currentID;
        }
    }

    //No init function needed
    public void Init()
    {

    }
    //No exit function implemented
    public void Exit()
    {

    }

}
