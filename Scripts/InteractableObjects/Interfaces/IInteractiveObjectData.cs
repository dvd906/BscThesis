using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractiveObjectData : IData
{
    IUnityInteractiveObject Object { get; }
}
