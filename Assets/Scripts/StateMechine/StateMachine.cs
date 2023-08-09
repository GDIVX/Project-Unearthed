using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    Dictionary<string, IState> _states;

    IState _currentState;
    public IState CurrentState { get => _currentState; private set => _currentState = value; }

    public StateMachine(IState initialState, string rootStateName)
    {
        CurrentState = initialState;
        _states = new Dictionary<string, IState>();

        AddState(initialState, rootStateName);
    }


    public void AddState(IState state, string name)
    {
        _states.Add(name, state);
    }

    public IState GetState(string name)
    {
        if (!_states.ContainsKey(name))
        {
            Debug.LogError($"State not found - name: {name}");
            return null;
        }

        return _states[name];
    }

    public void SetState(string name)
    {
        if (!_states.ContainsKey(name))
        {
            Debug.LogError($"State not found - name: {name}");
            return;
        }

        CurrentState?.Exit();
        CurrentState = _states[name];
        CurrentState.Enter();

    }

    public void ExecuteState()
    {
        if (_currentState == null)
        {
            Debug.LogError("Current state is null");
            return;
        }

        _currentState.Execute();
    }
}
