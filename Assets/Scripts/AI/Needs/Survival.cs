using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

/// <summary>
/// Survival
/// </summary>
[CreateAssetMenu(fileName = "Survival", menuName = "AI/Needs/Survival")]
public class Survival : Need
{    
    protected override float CalculateUtilityScore()
    {
        throw new System.NotImplementedException();
    }
}