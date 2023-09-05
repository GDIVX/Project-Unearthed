using Assets.Scripts.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RegeneratingStats : Stat
{
    protected abstract float RegenRateInSeconds { get ; set ; }
    public abstract IEnumerator Regenerate(int healAmount);
}
