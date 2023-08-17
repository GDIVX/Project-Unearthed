using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    [ShowInInspector] Dictionary<string, State> _states;
    [ShowInInspector] Dictionary<State, string> _stateNames = new Dictionary<State, string>(); 


    [ShowInInspector] State _currentState;
    public State CurrentState { get => _currentState; private set => _currentState = value; }
    public string CurrentStateName => _stateNames.ContainsKey(_currentState) ? _stateNames[_currentState] : null;


    public StateMachine()
    {
        _states = new Dictionary<string, State>();
    }

    public void AddState(State state, string name)
    {
        if (_states.ContainsKey(name))
        {
            Debug.LogError($"State with the name: {name} already exists. Cannot add duplicate state.");
            return;
        }

        _states.Add(name, state);
        _stateNames.Add(state, name); // New
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
