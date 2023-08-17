using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public enum needEnum
    {
        Idle,
        PlayerAwareness,
        AttackPlayer
    }
    public abstract class Need : ScriptableObject, IUtilityScoreProvider
    {
        public needEnum NeedState;
        public float GetUtilityScore()
        {
            return CalculateUtilityScore();
        }
        protected abstract float CalculateUtilityScore();
    }
}