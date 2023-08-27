using Assets.Scripts.AI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMock : UtilityAction
{
    public bool isExecuted = false;

    public float utilityScore = 0;

    protected override ValueDropdownList<string> myScoreValuesList { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public static ActionMock Create(Need need, float utilityScore,GameObject gameObject)
    {
        ActionMock mock = gameObject.GetComponent<ActionMock>();

        mock.TargetNeed = need;
        mock.utilityScore = utilityScore;

        return mock;
    }

    public override void Execute(GameObject gameObject)
    {
        isExecuted = true;
    }

    protected override float CalculateUtilityScore()
    {
        return utilityScore;
    }
}
