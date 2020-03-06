using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AIOrcMovementLogic : IOrcMovementLogic
{
    float animationPosX;
    float animationPosY;
    Vector2 values;

    IOrcMovementInput orcMoveInput;
    NavMeshAgent navMeshAgent;
    Vector2 input;

    Transform transform;

    public IOrcMovementInput InputInfo { get { return this.orcMoveInput; } }

    public bool HasInputModel { get { return this.orcMoveInput != null; } }

    public event Action<IOrcMovementInput> InputChanged;

    // TODO: Navmesh here
    public AIOrcMovementLogic(IOrcMovementInput orcMoveInput, NavMeshAgent navMeshAgent, Transform transform)
    {
        this.orcMoveInput = orcMoveInput;
        this.navMeshAgent = navMeshAgent;
        this.transform = transform;
        this.navMeshAgent.updateRotation = false;
        this.navMeshAgent.updatePosition = false;
    }

    public void ReadInput()
    {
        if (!HasInputModel)
            return;

        if (orcMoveInput.Targeter.HasTarget)
        {
            navMeshAgent.SetDestination(orcMoveInput.Targeter.Target.position);
        }
        else
        {
            navMeshAgent.SetDestination(orcMoveInput.Targeter.LastPosition);
        }
        input = new Vector2(navMeshAgent.desiredVelocity.x, navMeshAgent.desiredVelocity.z);
        orcMoveInput.PosX = input.x;
        orcMoveInput.PosY = input.y;
    }

    public void Reset()
    {
        orcMoveInput.PosX = 0;
        orcMoveInput.PosY = 0;
    }
}
