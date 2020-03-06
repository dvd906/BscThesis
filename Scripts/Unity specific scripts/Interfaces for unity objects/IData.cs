using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Every I..Data interface derives from it, these interfaces for the mono and scriptableobject classes.
public interface IData
{
    bool IsEnabled { get; }
    void EnableComponent(bool isEnabled);
}
