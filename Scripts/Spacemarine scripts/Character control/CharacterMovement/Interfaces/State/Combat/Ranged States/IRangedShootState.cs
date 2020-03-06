using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedShootState : IRangedState
{
    float CurrentShootDuration { get; set; }
}
