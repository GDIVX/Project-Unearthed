using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    [ShowInInspector] Dictionary<string, State> _states;

    State _currentState;
    public State CurrentState { get => _currentState; private set => _currentState = value; }

    public StateMachine Initialize(State initialState, string rootStateName)
    {
        CurrentState = initialState;
        _states = new Dictionary<string, State>();

        AddState(initialState, rootStateName);

        return this;
    }

    public void AddState(State state, string name)
    {
        _states.Add(name, state);
    }

    public State GetState(string name)
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

    public void SetState(State state)
    {
        CurrentState?.Exit();
        CurrentState = state;
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

    public void AddTransition(string fromState, string toState, Func<bool> condition)
    {
        if (!_states.ContainsKey(fromState))
        {
            Debug.LogError($"State not found - name: {fromState}");
            return;
        }

        if (!_states.ContainsKey(toState))
        {
            Debug.LogError($"State not found - name: {toState}");
            return;
        }

        _states[fromState].AddTransition(_states[toState], condition);
    }
}
