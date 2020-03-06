using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotateObject
{
    bool CanRotate { get; set; }
    void RotateObject(Transform copyRotationFrom);
}
