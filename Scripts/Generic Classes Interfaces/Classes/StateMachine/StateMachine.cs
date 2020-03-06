using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<Controller, StateID> : IStateMachine<Controller, StateID> where Controller : class where StateID : IConvertible
{
    bool conCurrentCanRun;
    
    StateID currentStateID;
    StateID goalID;

    IState<Controller, StateID> defaultState;
    IState<Controller, StateID> currentState;
    List<IState<Controller, StateID>> states;

    protected StateID defaultStateID;
    protected Controller controller;

    public StateMachine(bool conCurrentCanRun, StateID defaultSateID, ref Controller controller)
    {
        this.conCurrentCanRun = conCurrentCanRun;
        this.defaultStateID = defaultSateID;
        this.controller = controller;
        this.states = new List<IState<Controller, StateID>>();
    }

    public StateID GoalID { get { return this.goalID; } }

    public Controller Control { get { return this.controller; } }

    public StateID StateType { get { return this.currentStateID; } }

    public bool ConcurrentStateCanRun { get { return conCurrentCanRun; } }

    public event Action StateChanged;

    public void AddState(IState<Controller, StateID> state)
    {
        if (!states.Contains(state))
        {
            states.Add(state);
        }
    }

    // Every state machine has it's own transition
    public StateID CheckTransition()
    {
        return defaultStateID;
    }

    // No enter implemented
    public void Enter()
    {

    }

    public void Exit()
    {
        currentState.Exit();
        currentState = defaultState;
        currentState.Enter();
        currentStateID = defaultStateID;
    }

    // No init implemented
    public void Init()
    {

    }

    public void Reset()
    {
        currentState.Exit();
        currentState = defaultState;
        currentState.Enter();
        currentStateID = defaultStateID;
    }

    public void SetDefaultState(IState<Controller, StateID> state)
    {
        this.defaultState = state;
        this.defaultStateID = state.StateType;
        if (currentState == null)
        {
            this.currentState = defaultState;
            this.currentStateID = defaultStateID;
        }
    }

    public void TransitionState(StateID goal)
    {
        currentState = this.states.Find(x => x.StateType.Equals(goal));
    }

    // No update implemented
    public void Update(float time)
    {
    }

    public void UpdateMachine(float time)
    {
        if (states.Count == 0)
        {
            return;
        }

        if (currentStateID == null)
        {
            currentState = defaultState;
            currentStateID = defaultStateID;
        }
        if (currentState == null)
        {
            return;
        }

        goalID = currentState.CheckTransition();

        if (!goalID.Equals(currentStateID))
        {
            currentState.Exit();
            TransitionState(goalID);
            currentStateID = goalID;
            conCurrentCanRun = currentState.ConcurrentStateCanRun;
            currentState.Enter();

            if (StateChanged != null)
            {
                StateChanged();
            }
        }
        currentState.Update(time);
    }
}
