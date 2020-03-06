using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOptions : ICameraOptions
{
    private float rotationSmoothTime;
    private float minRotationX;
    private float maxRotationX;
    private float sphereRadius;
    private float lerpSpeed;
    private float maxCheckDistance;

    public float RotationSmoothTime { get { return this.rotationSmoothTime; } }

    public float MaxRotationLimitX { get { return this.maxRotationX; } }

    public float MinRotationLimitX { get { return this.minRotationX; } }

    public float MaxCheckDistance { get { return this.maxCheckDistance; } }

    public float SphereRadius { get { return this.sphereRadius; } }

    public float CameraLerpSpeed { get { return this.lerpSpeed; } }

    public CameraOptions(float rotationSmoothTime, float minRotationX, float maxRotationX, float sphereRadius, float lerpSpeed, float maxCheckDistance)
    {
        this.rotationSmoothTime = rotationSmoothTime;
        this.minRotationX = minRotationX;
        this.maxRotationX = maxRotationX;
        this.sphereRadius = sphereRadius;
        this.lerpSpeed = lerpSpeed;
        this.maxCheckDistance = maxCheckDistance;
    }
}
