using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Need1", menuName = "AI/Needs/Need1")]
public class Need1 : Assets.Scripts.AI.Need
{
    protected override float CalculateUtilityScore()
    {
        throw new System.NotImplementedException();
    }
}
