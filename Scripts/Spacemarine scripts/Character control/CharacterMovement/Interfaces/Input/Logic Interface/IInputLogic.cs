using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handling the user input
public interface IInputLogic<T> where T : class
{
    event Action<T> InputChanged;
    bool HasInputModel { get; }
    T InputInfo { get; }
    void ReadInput();
    void Reset();
}
