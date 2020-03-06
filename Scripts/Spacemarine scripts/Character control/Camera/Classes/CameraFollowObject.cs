using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : IFollowObject
{
    private float lerpSpeed;
    private Transform target;
    private Transform myTransform;

    public float FollowLerpSpeed
    {
        get { return this.lerpSpeed; }
        set { this.lerpSpeed = value; }
    }

    Transform IFollowObject.ObjectToFollow
    {
        get { return this.target; }
        set { this.target = value; }
    }

    public CameraFollowObject(float lerpSpeed, Transform toFollow, Transform myPosition)
    {
        this.lerpSpeed = lerpSpeed;
        this.target = toFollow;
        this.myTransform = myPosition;
    }
    
    //Follows the object with linear interpolation by the lerpSpeed.
    public void FollowObject(float time)
    {
        Vector3 newPosition = Vector3.Lerp(myTransform.position, target.position, time * lerpSpeed);
        this.myTransform.position = newPosition;
    }
}
