using Assets.Scripts.CharacterAbilities;
using System.Collections;
using UnityEngine;

namespace CharacterAbilities.Assets.Scripts.CharacterAbilities
{
    // This is a base class for all controllers that can be used to control a character
    public abstract class Controller : MonoBehaviour
    {
        public abstract Vector2 GetMovementVector();
    }
}