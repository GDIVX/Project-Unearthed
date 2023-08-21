using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

/// <summary>
/// Pursue
/// </summary>
[CreateAssetMenu(fileName = "Pursue", menuName = "AI/Actions/Pursue")]
public class Pursue : UtilityAction
{
    public override void Execute(UtilityAgent agent)
    {
       
    }

    protected override float CalculateUtilityScore()
    {
       throw new System.NotImplementedException();
    }        
}
