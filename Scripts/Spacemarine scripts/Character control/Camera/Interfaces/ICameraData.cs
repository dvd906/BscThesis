using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraData : IData
{
    ISpCameraFollow SpCamera { get; }
}
