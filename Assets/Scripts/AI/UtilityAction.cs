using Assets.Scripts.CharacterAbilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    public abstract class UtilityAction : ScriptableObject, IUtilityScoreProvider
    {
        [SerializeField] Need _targetNeed;
        [SerializeField] protected GameObject _target;
        [SerializeField] protected GameObject _agentGameObject;
        public Need TargetNeed { get => _targetNeed; protected set => _targetNeed = value; }

        public float GetUtilityScore()
        {
            return CalculateUtilityScore();
        }

        public abstract void Execute(GameObject agent);

        protected abstract float CalculateUtilityScore();


    }
}