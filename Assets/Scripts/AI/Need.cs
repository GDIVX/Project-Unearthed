using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public abstract class Need : ScriptableObject, IUtilityScoreProvider
    {
        [SerializeField] protected float _score;
        public float GetUtilityScore()
        {
            return CalculateUtilityScore();
        }
        protected abstract float CalculateUtilityScore();
    }
}