using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;
using Sirenix.OdinInspector;

/// <summary>
/// SearchPlayer
/// </summary>
[CreateAssetMenu(fileName = "SearchPlayer", menuName = "AI/Actions/SearchPlayer")]
public class SearchPlayer : UtilityAction
{
    protected override ValueDropdownList<string> myScoreValuesList { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void Execute(GameObject gameObject)
    {
       
    }

    protected override float CalculateUtilityScore()
    {
       throw new System.NotImplementedException();
    }        
}
