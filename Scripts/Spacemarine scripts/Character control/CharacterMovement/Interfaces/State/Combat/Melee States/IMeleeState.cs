using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeState : IState<IMovementAndMeleeCombatLogic, EMeleeAttackID>
{
}
