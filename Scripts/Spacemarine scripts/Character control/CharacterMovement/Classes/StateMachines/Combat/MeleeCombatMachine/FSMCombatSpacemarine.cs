using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMCombatSpacemarine : StateMachine<IMovementAndMeleeCombatLogic, EMeleeAttackID>, IFSMCombatSpaceMarine
{

    IInvertoryLogic invertoryLogic;

    public FSMCombatSpacemarine(bool concurrentMachineCanRun, EMeleeAttackID defaultStateID,
        ref IMovementAndMeleeCombatLogic control, ref IInvertoryLogic invertoryLogic) : base(concurrentMachineCanRun, defaultStateID, ref control)
    {
        this.invertoryLogic = invertoryLogic;
    }

    public new void UpdateMachine(float time)
    {
        if (!invertoryLogic.HasInputModel)
            return;

        if (invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon != null)
            base.UpdateMachine(time);
    }
}
