using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMRangedSpacemarine : StateMachine<IRangedCombatLogic, ERangedAttackID>, IFSMRangedSpacemarine
{
    IInvertoryLogic invertoryLogic;

    public FSMRangedSpacemarine(bool concurrentMachineCanRun, ERangedAttackID defaultID,
        ref IRangedCombatLogic control, ref IInvertoryLogic invertoryLogic) : base(concurrentMachineCanRun, defaultID, ref control)
    {
        this.invertoryLogic = invertoryLogic;
    }

    public IInvertoryLogic InvertoryLogic { get { return this.invertoryLogic; } }

    public new ERangedAttackID CheckTransition()
    {
        if (invertoryLogic.InputInfo.CurrentWeapons.CurrentRangedWeapon != null)
        {
            if (controller.InputInfo.IsAttackingRanged)
            {
                return ERangedAttackID.Shoot;
            }
            else if (controller.InputInfo.IsReloadEnabled)
            {
                return ERangedAttackID.Reload;
            }
            else
            {
                return this.defaultStateID;
            }
        }
        else
        {
            return ERangedAttackID.None;
        }

    }

    public new void UpdateMachine(float time)
    {
        if (invertoryLogic.HasInputModel)
            base.UpdateMachine(time);
    }
}
