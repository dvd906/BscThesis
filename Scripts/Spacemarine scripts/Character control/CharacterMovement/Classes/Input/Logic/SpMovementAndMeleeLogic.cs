using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpMovementAndMeleeLogic : IMovementAndMeleeCombatLogic
{
    public IMovementAndMeleeCombatInput InputInfo { get; private set; }

    public bool HasInputModel { get { return this.InputInfo != null; } }

    public event Action<IMovementAndMeleeCombatInput> InputChanged;

    Vector2 input;

    public SpMovementAndMeleeLogic(IMovementAndMeleeCombatInput model)
    {
        this.InputInfo = model;
    }

    public void ReadInput()
    {
        if (!HasInputModel)
            return;

        InputInfo.ResetChange();

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = input.normalized;
        //Attack input
        InputInfo.IsAttackingMelee = Input.GetButton("Fire2");
        //Movement input
        InputInfo.IsRunning = Input.GetKey(KeyCode.LeftShift);
        InputInfo.MovementPosX = input.x;
        InputInfo.MovementPosY = input.y;

        if (InputChanged != null && InputInfo.IsInputChanged)
        {
            InputChanged(this.InputInfo);
        }
    }

    public void Reset()
    {
        InputInfo.IsAttackingMelee = false;
        InputInfo.IsAttackingMelee = false;
        InputInfo.IsRunning = false;
        InputInfo.MovementPosX = 0f;
        InputInfo.MovementPosY = 0f;
    }
}
