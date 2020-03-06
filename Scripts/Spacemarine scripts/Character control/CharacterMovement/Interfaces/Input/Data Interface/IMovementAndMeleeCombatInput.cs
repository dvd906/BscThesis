using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementAndMeleeCombatInput
{
    // Moving Interface
    float MovementPosY { get; set; }
    float MovementPosX { get; set; }
    bool IsRunning { get; set; }
    bool IsFalling { get; }
    // Melee combat
    bool IsAttackingMelee { get; set; }
    bool IsInputChanged { get; }

    void ResetChange();
}
