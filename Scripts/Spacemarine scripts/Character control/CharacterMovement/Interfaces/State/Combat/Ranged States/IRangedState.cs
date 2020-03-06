using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedState : IState<IRangedCombatLogic, ERangedAttackID>
{
    IInvertoryLogic InvertoryLogic { get; }
}
