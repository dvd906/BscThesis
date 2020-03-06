using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOrcRotator : IRotateObject
{
    float rotationSpeed;
    Transform rotatetableObject;
    Quaternion targetRotation;
    Vector3 lookPos;

    public bool CanRotate { get; set; }

    public AIOrcRotator(Transform rotatetableObject, float rotationSpeed)
    {
        this.rotatetableObject = rotatetableObject;
        this.rotationSpeed = rotationSpeed;
    }

    public void RotateObject(Transform target)
    {
        if (CanRotate)
        {
            lookPos = target.position - this.rotatetableObject.position; // A - B vector
            lookPos.y = 0; // no rotation on y
            targetRotation = Quaternion.LookRotation(lookPos);
            this.rotatetableObject.rotation = Quaternion.Slerp(rotatetableObject.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
