using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMInvertory : IFSMInvertory
{
    public event Action StateChanged;

    bool concurrentMachineCanRun;

    EInvertoryStateID goalID;
    EInvertoryStateID currentStateID;
    EInvertoryStateID defaultStateID;

    IState<IInvertoryLogic, EInvertoryStateID> currentState;
    IState<IInvertoryLogic, EInvertoryStateID> defaultState;
    List<IState<IInvertoryLogic, EInvertoryStateID>> states;

    IInvertoryLogic control;

    public bool ConcurrentStateCanRun { get { return this.concurrentMachineCanRun; } }

    public EInvertoryStateID GoalID { get { return this.goalID; } }

    public EInvertoryStateID StateType { get { return this.currentStateID; } }

    public IInvertoryLogic Control { get { return this.control; } }

    public FSMInvertory(EInvertoryStateID defaultStateID, ref IInvertoryLogic control)
    {
        this.defaultStateID = defaultStateID;
        this.states = new List<IState<IInvertoryLogic, EInvertoryStateID>>();
        this.control = control;
    }

    public void AddState(IState<IInvertoryLogic, EInvertoryStateID> state)
    {
        this.states.Add(state);
    }

    public void SetDefaultState(IState<IInvertoryLogic, EInvertoryStateID> state)
    {
        this.defaultState = state;
        this.defaultStateID = state.StateType;
    }

    public void TransitionState(EInvertoryStateID goal)
    {
        currentState = states.Find(x => x.StateType == goal);
    }

    public void UpdateMachine(float time)
    {
        if (states.Count == 0)
        {
            return;
        }

        if (currentState == null)
        {
            currentState = defaultState;
            return;
        }

        goalID = currentState.CheckTransition();

        if (goalID != currentStateID)
        {
            currentState.Exit();
            TransitionState(goalID);
            currentStateID = goalID;
            concurrentMachineCanRun = currentState.ConcurrentStateCanRun;
            currentState.Enter();
            if (StateChanged!=null)
            {
                StateChanged();
            }
        }
        currentState.Update(time);
    }

    public void Exit()
    {
        this.currentState.Exit();
        this.currentState = defaultState;
        this.currentStateID = defaultStateID;
        this.currentState.Enter();
    }

    public void Reset()
    {
        this.currentState.Exit();
        this.currentState = defaultState;
        this.currentStateID = defaultStateID;
        this.currentState.Enter();
    }


    public EInvertoryStateID CheckTransition()
    {
        return EInvertoryStateID.None;
    }
    // No enter needed
    public void Enter()
    {
    }
    // No init needed
    public void Init()
    {
    }
    // No update needed
    public void Update(float time)
    {
    }
}
