using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<Controller, StateID> where Controller : class where StateID : IConvertible
{
    Controller Control { get; }
    StateID StateType { get; }

    bool ConcurrentStateCanRun { get; }

    void Enter();
    void Exit();
    void Update(float time);
    void Init();
    StateID CheckTransition();
}
