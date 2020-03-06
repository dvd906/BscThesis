using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IOrcAIInput
{
    IOrcMovementLogic MovementLogic { get; }
    IOrcAttackLogic CombatInput { get; }

    NavMeshAgent Agent { get; }
}
