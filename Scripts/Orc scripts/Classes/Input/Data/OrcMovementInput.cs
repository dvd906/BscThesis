using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMovementInput : IOrcMovementInput
{
    float posX;
    float posY;
    bool isInputChanged;
    ITarget target;

    public OrcMovementInput(ref ITarget target)
    {
        this.target = target;
    }

    public bool IsDriveUp { get; set; }

    public float PosX
    {
        get { return this.posX; }
        set
        {
            if (!isInputChanged && posX != 0)
            {
                isInputChanged = true;
            }
            posX = value;
        }
    }

    public float PosY
    {
        get { return this.posY; }
        set
        {
            if (!isInputChanged && posY != 0)
            {
                isInputChanged = true;
            }
            posY = value;
        }
    }

    public bool IsInputChanged { get { return this.isInputChanged; } }

    public ITarget Targeter { get { return this.target; } }

    public Vector3 DriveUpDirection { get; set; }

    public bool IsFalling
    {
        get
        {
            // TODO: create the fall in orc input
            return false;
        }
    }

    public void ResetChange()
    {
        this.isInputChanged = false;
        DriveUpDirection = Vector3.zero;
    }
}
