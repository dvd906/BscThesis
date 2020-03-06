using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO: create a whole control which houeses all the current states
public class FSMachineSpacemarineMove : StateMachine<IMovementAndMeleeCombatLogic, EMovementID>, IFSMMovementSpacemarine
{
    // TODO: Idle state for movement, than you can rotate it if it is not already ranged
    public FSMachineSpacemarineMove(EMovementID defaultStateID, ref IMovementAndMeleeCombatLogic control, bool concurrentMachineCanRun) : base
        (concurrentMachineCanRun, defaultStateID, ref control)
    {
    }

    public new void UpdateMachine(float time)
    {
        if (controller.HasInputModel)
            base.UpdateMachine(time);
    }
}
