using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public abstract class Need : ScriptableObject, IUtilityScoreProvider
    {
        [SerializeField] protected AnimationCurve _scoreCurve;
        [SerializeField] protected float _score;


        public float GetUtilityScore(GameObject gameObject)
        {
            return _score;
        }
        public float AnimationCurveSet(float dependedValue)
        {
           float score = _scoreCurve.Evaluate(dependedValue);
            return CalculateUtilityScore(score);
        }
        protected virtual float CalculateUtilityScore(float score)
        {
            return _score = score;
        }
    }
}