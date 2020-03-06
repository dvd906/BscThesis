using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialog : IUnityInteractiveObject
{
    string Title { get; set; }
    string[] BulletPoints { get; set; }

    string GetFormattedBulletPoints();
}
