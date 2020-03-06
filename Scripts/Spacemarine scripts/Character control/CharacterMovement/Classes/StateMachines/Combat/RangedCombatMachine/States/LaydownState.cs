using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaydownState : IRangedState
{
    bool conCurrentCanRun;
    IRangedCombatLogic control;
    ERangedAttackID currenStateID;
    IInvertoryLogic invertoryLogic;

    public IInvertoryLogic InvertoryLogic { get { return this.invertoryLogic; } }

    public IRangedCombatLogic Control { get { return this.control; } }

    public ERangedAttackID StateType { get { return this.currenStateID; } }

    public bool ConcurrentStateCanRun { get { return this.conCurrentCanRun; } }

    public LaydownState(bool conCurrentCanRun, ref IRangedCombatLogic control, ERangedAttackID currentID, ref IInvertoryLogic invertoryLogic)
    {
        this.conCurrentCanRun = conCurrentCanRun;
        this.control = control;
        this.currenStateID = currentID;
        this.invertoryLogic = invertoryLogic;
    }

    public ERangedAttackID CheckTransition()
    {
        if (control.InputInfo.IsAttackingRanged)
        {
            return ERangedAttackID.Shoot;
        }
        else if (control.InputInfo.IsReloadEnabled
            && invertoryLogic.InputInfo.CurrentWeapons.CurrentRangedWeapon.IsReloadEnabled)
        {
            return ERangedAttackID.Reload;
        }
        else
        {
            return this.currenStateID;
        }
    }

    // No enter needed
    public void Enter()
    {
    }
    // No exit needed
    public void Exit()
    {
    }
    // No init needed
    public void Init()
    {
    }
    // No update needed
    public void Update(float time)
    {
    }
}
