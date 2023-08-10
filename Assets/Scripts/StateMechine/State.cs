using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class State
{
    [ShowInInspector] Dictionary<State, Func<bool>> _transitions = new Dictionary<State, Func<bool>>();

    [SerializeField] StateMachine _parent;

    public event Action OnEnter, OnExit;

    public StateMachine Parent { get => _parent; private set => _parent = value; }

    public State(StateMachine parent)
    {
        Parent = parent;
    }

    public virtual void Enter()
    {
        OnEnter?.Invoke();
    }
    public virtual void Exit()
    {
        OnExit?.Invoke();
    }
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

        _transitions.Add(state, condition);
    }
}
