using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraFollow<T>
{
    LayerMask ToCollideWith { get; }
    //Root of the object
    Transform Root { get; }
    Transform TargetTransform { get; }
    Transform CameraTransform { get; }
    //Check the camera from here
    Transform PivotTransform { get; }

    ISensitivity SensitivityOptions { get; }
    ICameraOptions CameraSettings { get; }
    IFollowObject ObjectToFollow { get; }
    IRotateObject ObjectRotator { get; }

    void UpdateLocalPosition(float inputX, float inputY, float time);
    void LateUpdate(float time);

}
