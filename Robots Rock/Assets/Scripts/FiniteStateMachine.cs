using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute();
    void ExecuteFixed();
    void Exit();
}

public class FiniteStateMachine
{
    public IState CurrentState { get; private set; }

    public void ChangeState(IState newState)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = newState;
        CurrentState.Enter();
    }

    public void Execute()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute();
        }
    }

    public void ExecuteFixed()
    {
        if (CurrentState != null)
        {
            CurrentState.ExecuteFixed();
        }
    }
}
