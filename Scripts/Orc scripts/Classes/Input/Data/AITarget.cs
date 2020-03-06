using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITarget : ITarget
{
    bool hasTarget;
    Vector3 lastPosition;
    Transform target;
    Transform character;

    public AITarget(Transform charaterTransform)
    {
        this.character = charaterTransform;
    }

    public Transform Target
    {
        get { return this.target; }
        set
        {
            if (value == null)
            {
                if (target != null)
                {
                    lastPosition = this.target.transform.position;
                }
                else
                {
                    lastPosition = this.character.transform.position;
                }
                hasTarget = false;
            }
            else
            {
                hasTarget = true;
            }
            target = value;
        }
    }

    public bool HasTarget { get { return this.hasTarget; } }

    public Vector3 LastPosition { get { return this.lastPosition; } }
}
