using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Descripton for objectives", menuName = "Descriptions/Objective")]
public class DescriptionScriptableObject : ScriptableObject
{
    [SerializeField]
    string description;

    public string Description { get { return description; } }
}
