using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLogic : IMovementLogic
{
    private Vector2 input;
    public IMovementInput InputInfo { get; private set; }

    public bool HasInputModel { get { return this.input != null; } }


    public event Action<IMovementInput> InputChanged;

    public MovementLogic(IMovementInput inputInfo)
    {
        this.InputInfo = inputInfo;
    }


    public void ReadInput()
    {
        if (!HasInputModel)
            return;

        InputInfo.ResetChange();

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = input.normalized;
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
        if (!HasInputModel)
            return;

        InputInfo.IsRunning = false;
        InputInfo.MovementPosX = 0f;
        InputInfo.MovementPosY = 0f;
    }
}
