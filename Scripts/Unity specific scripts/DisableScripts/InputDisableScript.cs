using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Use for characters after the death
public class InputDisableScript : MonoBehaviour
{
    IDamagable currentCharacter;
    IData[] disableObjects;
    IData[] disableInChildren;
    Collider[] colliders;

    private void Start()
    {
        currentCharacter = GetComponentInChildren<IDamagableData>().Character;
        currentCharacter.Death += CurrentCharacter_Death;
        disableObjects = GetComponents<IData>();
        disableInChildren = GetComponentsInChildren<IData>();
        colliders = GetComponentsInChildren<Collider>();
    }

    private void CurrentCharacter_Death(int obj)
    {
        DisableData(disableObjects);
        DisableData(disableInChildren);

        if (colliders != null && colliders.Length != 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
        }

    }

    private void DisableData(IData[] disableObjects)
    {
        if (disableObjects != null && disableObjects.Length > 0)
        {
            for (int i = 0; i < disableObjects.Length; i++)
            {
                disableObjects[i].EnableComponent(false);
            }
        }
    }
}
