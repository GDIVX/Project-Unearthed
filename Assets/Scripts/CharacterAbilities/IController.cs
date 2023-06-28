using System;
using UnityEngine;

namespace Assets.Scripts.CharacterAbilities
{
    public interface IController
    {
        GameObject GameObject { get;}

        event Action onFire;
        event Action onReload;

        Vector3 GetAimPoint();
        Vector2 GetMovementVector();
    }
}
