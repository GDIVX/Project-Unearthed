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
        public static Need CreateInstance()
        {
            return CreateInstance<Need>();
        }
        protected abstract float CalculateUtilityScore();
    }
}