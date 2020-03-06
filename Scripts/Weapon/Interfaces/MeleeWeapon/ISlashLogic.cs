using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlashLogic
{
    int CurrentSlashIndex { get; }
    float BetweenTimeSlash { get; }
    ISlashInfo CurrentSlash { get; }
    void Reset();
    void NextSlash();
}
