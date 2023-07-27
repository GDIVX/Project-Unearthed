using System;
using System.Collections;
using UnityEngine;

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

        public abstract void Execute(UtilityAgent agent);

        protected abstract float CalculateUtilityScore();


    }
}