using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;
using System;

/// <summary>
/// Pose
/// </summary>
[CreateAssetMenu(fileName = "Pose", menuName = "AI/Actions/Pose")]
public class Pose : UtilityAction
{
    public override void Execute(Action agent)
    {
       
    }

    protected override float CalculateUtilityScore()
    {
       throw new System.NotImplementedException();
    }        
}
