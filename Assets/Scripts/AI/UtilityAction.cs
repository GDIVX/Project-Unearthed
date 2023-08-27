using Assets.Scripts.CharacterAbilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    public abstract class UtilityAction : MonoBehaviour, IUtilityScoreProvider
    {
        [SerializeField] Need _targetNeed;
        [SerializeField, ReadOnly] protected GameObject _target;
        [SerializeField] protected AnimationCurve _scoreCurve;
        [SerializeField, ReadOnly] protected float _actionScore;
        public Need TargetNeed { get => _targetNeed; protected set => _targetNeed = value; }



        // The selected values of the dropdown.
        [ValueDropdown(nameof(myScoreValuesList))]
        [SerializeField] protected List<string> _scoreParameters;

        // The selectable values for the dropdown, with custom names.
        protected abstract ValueDropdownList<string> myScoreValuesList { get; set; }

        // The Dictionary of the chosen score parameters.
        protected Dictionary<string, float> myScoreValues = new Dictionary<string, float>();

        public float GetUtilityScore(GameObject gameObject)
        {
            return CalculateUtilityScore();
        }

        public abstract void Execute(GameObject gameObject);

        protected abstract float CalculateUtilityScore();

        protected virtual void SetScoreValues()
        {
            myScoreValues.Clear();
            Debug.Log(" count:" +_scoreParameters.Count);
            if (_scoreParameters == null)
                Debug.LogError("set _scoreParameters ");
            foreach (var item in _scoreParameters)
            {
                myScoreValues.Add(item, 0);
                Debug.Log(" item" + item);
            }
        }
    }
}