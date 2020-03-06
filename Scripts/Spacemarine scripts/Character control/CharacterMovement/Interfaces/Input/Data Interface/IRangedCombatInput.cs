using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedCombatInput : IInput
{
    bool IsAttackingRanged { get; set; }
    bool IsReloadEnabled { get; set; }
    bool IsZoomingEnabled { get; set; }
}
