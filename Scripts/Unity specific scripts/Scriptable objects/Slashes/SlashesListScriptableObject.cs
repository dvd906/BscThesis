using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New slashes collection", menuName = "Weapons/Information/Slashes Collection")]
public class SlashesListScriptableObject : ScriptableObject
{
    [SerializeField]
    float timeToWait;
    [SerializeField]
    List<SlashInfoScriptableObject> slashes = new List<SlashInfoScriptableObject>();

    public float TimeToWait
    {
        get
        {
            return timeToWait;
        }
    }

    public List<SlashInfoScriptableObject> Slashes
    {
        get
        {
            return slashes;
        }
    }
}
