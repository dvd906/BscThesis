using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine<T>
{
    void Enter(T stateObject);
    void Update(T stateObject);
    void Exit(T stateObject);
}
