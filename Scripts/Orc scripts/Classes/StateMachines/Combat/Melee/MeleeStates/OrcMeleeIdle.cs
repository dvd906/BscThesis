using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMeleeIdle : IOrcCombatState
{
    bool conCurrentCanRun;
    IOrcAttackLogic control;
    EOrcCombatState currentID;

    public OrcMeleeIdle(bool conCurrentCanRun, ref IOrcAttackLogic control, EOrcCombatState currentID)
    {
        this.conCurrentCanRun = conCurrentCanRun;
        this.control = control;
        this.currentID = currentID;
    }

    public IOrcAttackLogic Control { get { return this.control; } }

    public EOrcCombatState StateType { get { return this.currentID; } }

    public bool ConcurrentStateCanRun { get { return this.conCurrentCanRun; } }

    public EOrcCombatState CheckTransition()
    {
        if (control.InputInfo.IsAttacking)
        {
            return EOrcCombatState.Attacking;
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
    // No update implemented
    public void Update(float time)
    {
    }
}
