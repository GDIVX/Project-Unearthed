using System;
using System.Collections;
using UnityEngine;

namespace CharacterAbilities.Assets.Scripts.CharacterAbilities
{
    public interface IProjectileWeaponInput
    {
        GameObject GameObject { get; }
        event Action onFire;
        event Action onReload;

        Vector3 GetAimPoint();

    }
}