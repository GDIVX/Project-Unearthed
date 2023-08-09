using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class State
{
    [ShowInInspector] Dictionary<State, Func<bool>> _transitions;

    [SerializeField] StateMachine _parent;

    public StateMachine Parent { get => _parent; private set => _parent = value; }

    public abstract void Enter();
    public abstract void Exit();
    public virtual void Execute()
    {
        //check if transition condition is met, and if so call the parent to change state
        foreach (var transition in _transitions)
        {
            if (transition.Value.Invoke())
            {
                //transition to this state
                Parent.SetState(transition.Key);
                return;
            }
        }
    }

    public void AddTransition(State state, Func<bool> condition)
    {
        if (_transitions == null)
        {
            _transitions = new();
        }

        _transitions.Add(state, condition);
    }
}
