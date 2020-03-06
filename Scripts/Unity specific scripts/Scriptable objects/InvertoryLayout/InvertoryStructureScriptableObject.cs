using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New invertory layout", menuName = "Invertory/InvertoryStructure")]
public class InvertoryStructureScriptableObject : ScriptableObject
{
    [SerializeField]
    List<InvertoryLayoutScriptableObject> stashStructure = new List<InvertoryLayoutScriptableObject>();

    public List<InvertoryLayoutScriptableObject> StashStructure
    {
        get
        {
            return stashStructure;
        }
    }
}
