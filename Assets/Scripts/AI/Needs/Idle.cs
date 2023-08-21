using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

/// <summary>
/// Idle
/// </summary>
[CreateAssetMenu(fileName = "Idle", menuName = "AI/Needs/Idle")]
public class Idle : Need
{    
    protected override float CalculateUtilityScore()
    {
        return 0;
        //throw new System.NotImplementedException();
    }
}