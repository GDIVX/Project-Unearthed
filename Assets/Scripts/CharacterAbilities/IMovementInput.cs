using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAbilities
{
    public interface IMovementInput
    {
        GameObject GameObject { get; }
        Vector2 GetMovementVector();

        event Action OnDodge;
    }
}
