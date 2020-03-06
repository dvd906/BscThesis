using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The weapons positions in the game
public interface IWeaponPlace
{
    Transform Idle { get; }
    Transform Use { get; }
    // Transform Unequipped { get; }
}
