using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSMRangedSpacemarine : IStateMachine<IRangedCombatLogic, ERangedAttackID>
{
    IInvertoryLogic InvertoryLogic { get; }
}
