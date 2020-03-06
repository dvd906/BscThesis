using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Includes all the localpositions
public interface ICameraPlaces
{
    Vector3 IdlePosition { get; }
    Vector3 AimPosition { get; }
    Vector3 RunPosition { get; }
}
