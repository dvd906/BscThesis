using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcFSMCombat : IOrcFSMCombat
{
    public event Action StateChanged;

    bool concurrentCanRun;
    EOrcCombatState goalID;
    EOrcCombatState defaultStateID;
    EOrcCombatState currentStateID;
    IOrcAttackLogic control;

    IState<IOrcAttackLogic, EOrcCombatState> currentState;
    IState<IOrcAttackLogic, EOrcCombatState> defaultState;
    List<IState<IOrcAttackLogic, EOrcCombatState>> states;

    public OrcFSMCombat(ref IOrcAttackLogic control)
    {
        this.control = control;
        this.states = new List<IState<IOrcAttackLogic, EOrcCombatState>>();
    }

    public EOrcCombatState GoalID { get { return this.goalID; } }

    public IOrcAttackLogic Control { get { return this.control; } }

    public EOrcCombatState StateType { get { return this.currentStateID; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public void AddState(IState<IOrcAttackLogic, EOrcCombatState> state)
    {
        this.states.Add(state);
    }

    public void Exit()
    {
        currentState.Exit();
        currentState = defaultState;
        currentStateID = defaultStateID;
    }

    public void SetDefaultState(IState<IOrcAttackLogic, EOrcCombatState> state)
    {
        this.defaultState = state;
        this.defaultStateID = state.StateType;
    }

    public void TransitionState(EOrcCombatState goal)
    {
        currentState = states.Find(x => x.StateType == goal);
    }

    public void UpdateMachine(float time)
    {
        if (states.Count == 0)
        {
            return;
        }

        if (currentStateID == EOrcCombatState.None)
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

    //No checktransition implemented
    public EOrcCombatState CheckTransition()
    {
        return EOrcCombatState.Idle;
    }
    // No update function implemented
    public void Update(float time)
    {
    }
    // No init function implemented
    public void Init()
    {
    }
    // No reset function implemented
    public void Reset()
    {
    }
    // No enter function implemented
    public void Enter()
    {
    }
}
