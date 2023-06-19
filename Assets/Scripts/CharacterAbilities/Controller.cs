using Assets.Scripts.CharacterAbilities;
using System;
using System.Collections;
using UnityEngine;

namespace CharacterAbilities.Assets.Scripts.CharacterAbilities
{
    // This is a base class for all controllers that can be used to control a character
    public abstract class Controller : MonoBehaviour
    {
        public Action onFire;
        public Action onReload;

        /// <summary>
        /// Returns a vector that represents the movement input
        /// </summary>
        /// <returns>Normolized Vector2</returns>
        public abstract Vector2 GetMovementVector();

    }
}