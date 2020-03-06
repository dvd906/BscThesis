using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPistol : IHoldWeapon
{
    public float HoldTime { get; private set; }

    public HoldPistol(float holdTime)
    {
        this.HoldTime = holdTime;
    }
}
