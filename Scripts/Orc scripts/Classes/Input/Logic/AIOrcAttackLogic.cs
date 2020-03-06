using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOrcAttackLogic : IAIOrcAttackLogic
{
    public event Action<IOrcAttackInput> InputChanged;

    Transform character;
    IAIAttackInput attackInput;
    IOrcMovementLogic movementLogic;
    float attackDistance;
    float currentDistance;

    public AIOrcAttackLogic(Transform character, ref IAIAttackInput attackInput, float attackDistance, ref IOrcMovementLogic movementLogic)
    {
        this.attackInput = attackInput;
        this.attackDistance = attackDistance;
        this.movementLogic = movementLogic;
        this.character = character;
    }

    public IOrcAttackInput InputInfo { get { return this.attackInput; } }

    public bool HasInputModel { get { return this.attackInput != null; } }

    public void ReadInput()
    {
        if (!HasInputModel)
            return;

        // Melee attack, this is a beta version 
        // Newer version only use the current inputData
        if (movementLogic.InputInfo.Targeter.HasTarget || attackInput.Targeter.HasTarget)
        {
            if (attackInput.Targeter.Target == null ||
                (attackInput.Targeter.Target != movementLogic.InputInfo.Targeter.Target
                && movementLogic.InputInfo.Targeter.Target != null))
            {
                attackInput.Targeter.Target = movementLogic.InputInfo.Targeter.Target;
            }
            currentDistance = (attackInput.Targeter.Target.position - character.position).magnitude;
            if (currentDistance <= attackDistance)
            {
                attackInput.IsAttacking = true;
            }
            else
            {
                attackInput.IsAttacking = false;
            }
        }
        else
        {
            attackInput.IsAttacking = false;
        }
    }

    public void Reset()
    {
        attackInput.IsAttacking = false;
    }
}
