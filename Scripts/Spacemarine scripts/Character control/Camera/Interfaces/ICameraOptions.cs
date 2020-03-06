using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraOptions
{
    //Basic properties
    float RotationSmoothTime { get; }
    float MaxRotationLimitX { get; }
    float MinRotationLimitX { get; }
    //Sphere collision
    float MaxCheckDistance { get; }
    float SphereRadius { get; }
    //Return to the desired position with speed
    float CameraLerpSpeed { get; }
}
