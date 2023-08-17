using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.AI
{
    public abstract class UtilityAction : ScriptableObject, IUtilityScoreProvider
    {
        [SerializeField] Need _targetNeed;
        
        public Need TargetNeed { get => _targetNeed; protected set => _targetNeed = value; }

        public float GetUtilityScore()
        {
            return CalculateUtilityScore();
        }

        public abstract void Execute(Action action);

        protected abstract float CalculateUtilityScore();


    }
}