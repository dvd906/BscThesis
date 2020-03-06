using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOrcMovementInput : IInput
{
    bool IsDriveUp { get; set; }
    Vector3 DriveUpDirection { get; set; }

    bool IsFalling { get; }

    float PosX { get; set; }
    float PosY { get; set; }
    // TODO another targeter interface?
    ITarget Targeter { get; }
}
