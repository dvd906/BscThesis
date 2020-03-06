using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldPistolAnimationInfo : IHoldObjectAnimator<bool>
{
    float HoldPistolTimeSeconds { get; set; }
    bool IsHoldingPistol { get; set; }
}
