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
        //Debug.Log("patrolling");
    }

    protected override float CalculateUtilityScore()
    {
        return 0;
       //throw new System.NotImplementedException();
    }        
}
