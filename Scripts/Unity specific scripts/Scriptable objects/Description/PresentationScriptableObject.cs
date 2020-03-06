using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Descripton for objectives", menuName = "Descriptions/Presentation")]
public class PresentationScriptableObject : ScriptableObject
{
    [SerializeField]
    string title;
    [SerializeField]
    string[] bulletPoints;

    public string Title { get { return title; } }

    public string[] BulletPoints { get { return bulletPoints; } }
}
