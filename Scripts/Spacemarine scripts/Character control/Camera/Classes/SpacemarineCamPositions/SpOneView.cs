using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpOneView : ISpView
{
    Transform position;
    ICameraOptions camSateOption;

    public ICameraOptions CameraOption { get { return this.camSateOption; } }

    public Transform OnePosition { get { return this.position; } }

    public SpOneView(Transform position, Transform camPivotTransform,
        float rotationSmoothTime, float minRotationX, float maxRotationX, float sphereRadius, float lerpSpeed)
    {
        this.position = position;
        float maxCheckDistance = Mathf.Round((camPivotTransform.position - position.position).magnitude); //0.2f is the plus range
        this.camSateOption = new CameraOptions(rotationSmoothTime, minRotationX, maxRotationX, sphereRadius, lerpSpeed, maxCheckDistance);
    }
}
