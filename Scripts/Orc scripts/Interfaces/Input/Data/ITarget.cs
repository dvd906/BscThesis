using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    bool HasTarget { get; }
    Transform Target { get; set; }
    Vector3 LastPosition { get; }
}
