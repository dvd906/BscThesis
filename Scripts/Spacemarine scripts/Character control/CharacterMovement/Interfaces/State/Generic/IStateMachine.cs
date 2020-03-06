using System;
// T is the state controller type
// G is the goal type like enum
public interface IStateMachine<StateController, StateID> : IState<StateController, StateID> where StateController : class where StateID : IConvertible
{
    event Action StateChanged;

    void UpdateMachine(float time);
    void AddState(IState<StateController, StateID> state);
    void SetDefaultState(IState<StateController, StateID> state);
    StateID GoalID { get; }
    void TransitionState(StateID goal);
    void Reset();
}
