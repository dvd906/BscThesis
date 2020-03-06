using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertoryIdle : IInvertoryState
{
    bool conCurrentCanRun;

    IInvertoryLogic control;

    public IInvertoryLogic Control { get { return this.control; } }

    public EInvertoryStateID StateType { get { return EInvertoryStateID.None; } }

    public bool ConcurrentStateCanRun { get { return this.conCurrentCanRun; } }

    public InvertoryIdle(bool conCurrentCanRun, ref IInvertoryLogic control)
    {
        this.control = control;
        this.conCurrentCanRun = conCurrentCanRun;
    }

    public EInvertoryStateID CheckTransition()
    {
        if (control.IsChangeNeeded)
        {
            return EInvertoryStateID.Switching;
        }
        else
        {
            return EInvertoryStateID.None;
        }
    }

    public void Update(float time)
    {
        control.ReadInput();
    }
    //No enter function needed
    public void Enter()
    {
    }
    //No exit function needed
    public void Exit()
    {
    }
    //No init function needed
    public void Init()
    {
    }
}
