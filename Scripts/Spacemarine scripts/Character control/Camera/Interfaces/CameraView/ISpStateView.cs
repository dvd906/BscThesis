using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpStateView
{
    ISpView BasicView { get; }
    ISpView AimView { get; }
    ISpView RunView { get; }
}
