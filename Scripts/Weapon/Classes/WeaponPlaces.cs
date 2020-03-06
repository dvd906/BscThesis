using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlaces : IWeaponPlace
{
    Transform idle;
    Transform use;

    public Transform Idle { get { return this.idle; } }

    public Transform Use { get { return this.use; } }

    public WeaponPlaces(ref Transform idle, ref Transform use)
    {
        this.idle = idle;
        this.use = use;
    }
}
