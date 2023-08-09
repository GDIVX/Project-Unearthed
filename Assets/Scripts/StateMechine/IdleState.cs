using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a state that does nothing.
/// </summary>
public class IdleState : State
{
    public override void Enter()
    {
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
    }
}
