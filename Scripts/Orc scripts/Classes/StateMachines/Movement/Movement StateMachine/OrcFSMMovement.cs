using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcFSMMovement : IOrcFSMMovement
{
    public event Action StateChanged;

    bool concurrentCanRun;
    EOrcMovementStates goalID;
    EOrcMovementStates defaultStateID;
    EOrcMovementStates currentStateID;
    IOrcMovementLogic control;

    IState<IOrcMovementLogic, EOrcMovementStates> currentState;
    IState<IOrcMovementLogic, EOrcMovementStates> defaultState;
    List<IState<IOrcMovementLogic, EOrcMovementStates>> states;

    public OrcFSMMovement(ref IOrcMovementLogic control)
    {
        this.control = control;
        this.states = new List<IState<IOrcMovementLogic, EOrcMovementStates>>();
    }

    public EOrcMovementStates GoalID { get { return this.goalID; } }

    public IOrcMovementLogic Control { get { return this.control; } }

    public EOrcMovementStates StateType { get { return this.currentStateID; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public void AddState(IState<IOrcMovementLogic, EOrcMovementStates> state)
    {
        this.states.Add(state);
    }

    public void Reset()
    {
        currentState.Exit();
        currentStateID = defaultStateID;
        currentState = defaultState;
        defaultState.Enter();
    }

    public void SetDefaultState(IState<IOrcMovementLogic, EOrcMovementStates> state)
    {
        defaultState = state;
        defaultStateID = state.StateType;
    }

    public void TransitionState(EOrcMovementStates goal)
    {
        currentState = states.Find(x => x.StateType == goal);
    }

    public void UpdateMachine(float time)
    {
        if (states.Count == 0)
        {
            return;
        }

        if (currentStateID == EOrcMovementStates.None)
        {
            currentState = defaultState;
            currentStateID = defaultStateID;
        }
        if (currentState == null)
        {
            return;
        }

        goalID = currentState.CheckTransition();

        if (goalID != currentStateID)
        {
            currentState.Exit();
            TransitionState(goalID);
            currentStateID = goalID;
            this.concurrentCanRun = currentState.ConcurrentStateCanRun;
            currentState.Enter();

            if (StateChanged != null)
            {
                StateChanged();
            }
        }
        currentState.Update(time);
    }
    // No update implemented
    public void Update(float time)
    {
    }
    // No enter implemented
    public void Enter()
    {
    }
    // No exit implemented
    public void Exit()
    {
    }
    // No init implemented
    public void Init()
    {
    }
    // No checkTransition implemented
    public EOrcMovementStates CheckTransition()
    {
        return EOrcMovementStates.None;
    }
}
