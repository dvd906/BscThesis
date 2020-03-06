using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIOrcInput : IOrcAIInput
{
    LayerMask eyesLayer;

    IOrcMovementLogic orcMovementInput;
    IOrcAttackLogic orcAttackInput;
    NavMeshAgent agent;

    public LayerMask EyeLayer { get { return this.eyesLayer; } }

    public IOrcMovementLogic MovementLogic { get { return this.orcMovementInput; } }

    public IOrcAttackLogic CombatInput { get { return this.orcAttackInput; } }

    public NavMeshAgent Agent { get { return this.agent; } }

    public AIOrcInput(IOrcMovementLogic orcMovementInput, IOrcAttackLogic orcAttackInput, NavMeshAgent agent)
    {
        this.orcMovementInput = orcMovementInput;
        this.orcAttackInput = orcAttackInput;
        this.agent = agent;
    }
}
