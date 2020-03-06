using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeIdle : IMeleeState
{
    bool concurrentCanRun;
    EMeleeAttackID currentID;
    IMovementAndMeleeCombatLogic control;

    public IMovementAndMeleeCombatLogic Control { get { return this.control; } }

    public EMeleeAttackID StateType { get { return this.currentID; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public MeleeIdle(bool concurrentCanRun, EMeleeAttackID currentID, ref IMovementAndMeleeCombatLogic control)
    {
        this.concurrentCanRun = concurrentCanRun;
        this.currentID = currentID;
        this.control = control;
    }

    public EMeleeAttackID CheckTransition()
    {
        if (control.InputInfo.IsAttackingMelee && !control.InputInfo.IsRunning)
        {
            return EMeleeAttackID.Attacking;
        }
        else
        {
            return this.currentID;
        }
    }

    // No enter implemented
    public void Enter()
    {
    }

    // No exit implemented
    public void Exit()
    {
    }

    // No init implemented
    public void Init()
    {
    }

    public void Update(float time)
    {
        control.ReadInput();
    }
}
