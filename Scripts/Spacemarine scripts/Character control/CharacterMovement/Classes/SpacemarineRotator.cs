using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacemarineRotator : IRotateObject
{
    //Rotationspeed info?
    private float rotationSpeed;
    private Transform toRotate;
    private float currentVelocity;
    private bool canRotate;

    public bool CanRotate
    {
        get
        {
            return canRotate;
        }

        set
        {
            canRotate = value;
        }
    }

    public SpacemarineRotator(Transform toRotate, float rotationSpeed)
    {
        this.toRotate = toRotate;
        this.rotationSpeed = rotationSpeed;
    }

    public void RotateObject(Transform copyRotationFrom)
    {
        if (canRotate)
        {
            float desiredAngle = Mathf.SmoothDampAngle(toRotate.eulerAngles.y, copyRotationFrom.eulerAngles.y, ref currentVelocity, rotationSpeed * Time.deltaTime);
            toRotate.eulerAngles = Vector3.up * desiredAngle;
        }
    }
}
