using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEnableScript : MonoBehaviour
{
    IMovementData[] mobs;
    bool isEnabled;

    void Start()
    {
        mobs = GetComponentsInChildren<IMovementData>();
        isEnabled = false;
        EnableOrDisableMobs(isEnabled, null);
    }

    private void EnableOrDisableMobs(bool isEnabled, Transform target)
    {
        bool hasTarget = target == null ? false : true;
        if (mobs != null && mobs.Length > 0)
        {
            for (int i = 0; i < mobs.Length; i++)
            {
                mobs[i].EnableComponent(isEnabled);
                if (hasTarget)
                {
                    mobs[i].Targeter.Target = target;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isEnabled)
        {
            isEnabled = true;
            EnableOrDisableMobs(isEnabled, other.transform);
        }
    }
}
