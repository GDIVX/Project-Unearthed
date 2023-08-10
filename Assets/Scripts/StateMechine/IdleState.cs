using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a state that does nothing.
/// </summary>
public class IdleState : State
{
    public IdleState(StateMachine parent) : base(parent)
    {
    }

    public override void Enter()
    {
        base.Execute();

    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Execute();

    }
}
