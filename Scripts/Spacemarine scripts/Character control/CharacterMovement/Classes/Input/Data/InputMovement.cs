using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : IMovementInput
{
    private float movementPosX;
    private float movementPosY;
    private bool isRunning;
    private bool isInputChanged;
    private bool isFalling;

    private float stopRunTreshold = 0.9f;

    public float MovementPosY
    {
        get { return this.movementPosY; }
        set
        {
            if (!isInputChanged && movementPosY != 0)
            {
                isInputChanged = true;
            }
            movementPosY = value;
        }
    }
    public float MovementPosX
    {
        get { return this.movementPosX; }
        set
        {
            if (!isInputChanged && movementPosX != 0)
            {
                isInputChanged = true;
            }
            movementPosX = value;
        }
    }
    public bool IsRunning
    {
        get { return this.isRunning; }
        set
        {
            if (isRunning && movementPosY < stopRunTreshold)
            {
                isRunning = false;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
            else if (value == true)
            {
                isRunning = true;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }
    public bool IsInputChanged { get { return isInputChanged; } }

    //TODO: Raycast if falling
    public bool IsFalling { get { return this.isFalling; } }

    public InputMovement()
    {
        movementPosX = 0f;
        movementPosY = 0f;
        isRunning = false;
        isInputChanged = false;
        isFalling = false;
    }

    public void ResetChange()
    {
        isInputChanged = false;
    }
}
