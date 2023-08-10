using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

/// <summary>
/// Fire
/// </summary>
[CreateAssetMenu(fileName = "Fire", menuName = "AI/Actions/Fire")]
public class Fire : UtilityAction
{
    public override void Execute(UtilityAgent agent)
    {
        throw new System.NotImplementedException();
    }

    protected override float CalculateUtilityScore()
    {
        Debug.Log("A");
        throw new System.NotImplementedException();
    }        
}