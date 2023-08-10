using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

/// <summary>
/// needy
/// </summary>
[CreateAssetMenu(fileName = "needy", menuName = "AI/Needs/needy")]
public class needy : Need
{    
    protected override float CalculateUtilityScore()
    {
        throw new System.NotImplementedException();
    }
}