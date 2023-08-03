using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action1", menuName = "AI/Actions/Action1")]
public class Action1 : UtilityAction
{
    public override void Execute(UtilityAgent agent)
    {
        throw new System.NotImplementedException();
    }

    protected override float CalculateUtilityScore()
    {
        throw new System.NotImplementedException();
    }
}
