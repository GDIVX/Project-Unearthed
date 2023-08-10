using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

/// <summary>
/// Patrol
/// </summary>
[CreateAssetMenu(fileName = "Patrol", menuName = "AI/Actions/Patrol")]
public class Patrol : UtilityAction
{
    public override void Execute(UtilityAgent agent)
    {
       
    }

    protected override float CalculateUtilityScore()
    {
       throw new System.NotImplementedException();
    }        
}
