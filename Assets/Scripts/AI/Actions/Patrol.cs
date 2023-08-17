using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.AI;
using Assets.Scripts.CharacterAbilities;
using System;

/// <summary>
/// Patrol
/// </summary>
[CreateAssetMenu(fileName = "Patrol", menuName = "AI/Actions/Patrol")]
public class Patrol : UtilityAction
{
    public override void Execute(Action action)
    {
        action?.Invoke();
    }

    protected override float CalculateUtilityScore()
    {
        return 1;
       //throw new System.NotImplementedException();
    }        
}
