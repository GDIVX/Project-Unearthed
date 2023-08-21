using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

/// <summary>
/// SearchPlayer
/// </summary>
[CreateAssetMenu(fileName = "SearchPlayer", menuName = "AI/Actions/SearchPlayer")]
public class SearchPlayer : UtilityAction
{
    public override void Execute(GameObject agent)
    {
       
    }

    protected override float CalculateUtilityScore()
    {
       throw new System.NotImplementedException();
    }        
}
