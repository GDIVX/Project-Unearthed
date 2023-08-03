using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public abstract class Need : ScriptableObject, IUtilityScoreProvider
    {
        public float GetUtilityScore()
        {
            return CalculateUtilityScore();
        }

        protected abstract float CalculateUtilityScore();
    }
}