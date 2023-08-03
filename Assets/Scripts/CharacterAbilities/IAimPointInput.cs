using System.Collections;
using UnityEngine;

namespace CharacterAbilities.Assets.Scripts.CharacterAbilities
{
    public interface IAimPointInput
    {
        GameObject GameObject { get; }
        Vector3 GetAimPoint();
    }
}