using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpCamPositions : ICameraPlaces
{
    Vector3 idlePos;
    Vector3 aimPos;
    Vector3 runPosition;

    public Vector3 IdlePosition { get { return this.idlePos; } }

    public Vector3 AimPosition { get { return this.aimPos; } }

    public Vector3 RunPosition { get { return this.runPosition; } }

    public SpCamPositions(Vector3 idlePos, Vector3 aimPos, Vector3 runPosition)
    {
        this.idlePos = idlePos;
        this.aimPos = aimPos;
        this.runPosition = runPosition;
    }
}
