using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public interface IUtilityScoreProvider
    {
        public float GetUtilityScore();

    }
}