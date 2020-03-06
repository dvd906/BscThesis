using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControl<T>
{
    T Control { get; }

    void Update(float time);
    void UpdatePerceptions(float time);
    void Init();
}
