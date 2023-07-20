using Assets.Scripts.AI;
using Assets.Scripts.CharacterAbilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UtilityAgent : MonoBehaviour, IController
{
    [SerializeField] List<UtilityAction> _actions;
    [SerializeField] List<Need> _needs;

    public GameObject GameObject => gameObject;

    public event Action onFire;
    public event Action onReload;

    public Vector3 GetAimPoint()
    {
        return CalculateAimPoint();
    }

    public Vector2 GetMovementVector()
    {
        return CalculateMovementVector();
    }

    protected virtual Vector3 CalculateAimPoint()
    {
        return default;
    }

    protected virtual Vector2 CalculateMovementVector()
    {
        return default;
    }

    private void Update()
    {
        ExecuteAction();
    }

    public void ExecuteAction()
    {
        Need need = ChooseNeed();
        UtilityAction action = ChooseAction(need);

        action.Execute(this);
    }

    protected Need ChooseNeed()
    {
        float maxScore = float.MinValue;
        Need chosenNeed = null;
        foreach (Need need in _needs)
        {
            if (need.GetUtilityScore() > maxScore)
            {
                maxScore = need.GetUtilityScore();
                chosenNeed = need;
            }
        }
        return chosenNeed;
    }

    protected UtilityAction ChooseAction(Need need)
    {

        List<UtilityAction> relevantActions = _actions.Where((action) => action.TargetNeed == need).ToList();

        float maxScore = float.MinValue;
        UtilityAction chosenAction = null;
        foreach (UtilityAction action in relevantActions)
        {
            if (need.GetUtilityScore() > maxScore)
            {
                maxScore = need.GetUtilityScore();
                chosenAction = action;
            }
        }
        return chosenAction;
    }

}
