using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

/// <summary>
/// AttackPlayer
/// </summary>
[CreateAssetMenu(fileName = "AttackPlayer", menuName = "AI/Needs/AttackPlayer")]
public class AttackPlayer : Need
{    
    protected override float CalculateUtilityScore()
    {
        return _score;
    }
}