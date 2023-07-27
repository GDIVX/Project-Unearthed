using Assets.Scripts.AI;
using Assets.Scripts.CharacterAbilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UtilityAgent : MonoBehaviour, IController
{
    [SerializeField] List<UtilityAction> _actions;
    [SerializeField] List<Need> _needs;

    public GameObject GameObject => gameObject;

    public List<UtilityAction> Actions { get => _actions; set => _actions = value; }
    public List<Need> Needs { get => _needs; set => _needs = value; }

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



    private void ExecuteAction()
    {
        Need need = ChooseNeed();

        UtilityAction action = ChooseAction(need);

        if (action == null)
        {
            Debug.LogError("No action was chosen");
            return;
        }

        action.Execute(this);
    }

    protected Need ChooseNeed()
    {
        float maxScore = -1;
        Need chosenNeed = null;

        if (Needs.Count == 0)
        {
            Debug.LogError("No needs were added to agent");
        }

        foreach (Need need in Needs)
        {
            if (need.GetUtilityScore() >= maxScore)
            {
                maxScore = need.GetUtilityScore();
                chosenNeed = need;
            }
        }

        return chosenNeed ?? Needs.FirstOrDefault();
    }


    protected UtilityAction ChooseAction(Need need)
    {
        if (need is null)
        {
            throw new ArgumentNullException(nameof(need));
        }

        List<UtilityAction> relevantActions = Actions.Where((action) => action.TargetNeed == need).ToList();

        if (relevantActions.Count == 0)
        {
            Debug.LogError($"No relevant actions were found for the need {need.name}");
        }

        float maxScore = float.MinValue;
        UtilityAction chosenAction = null;
        foreach (UtilityAction action in relevantActions)
        {
            float actionScore = action.GetUtilityScore();
            if (actionScore >= maxScore)
            {
                maxScore = actionScore;
                chosenAction = action;
            }
        }
        return chosenAction;
    }

}


