using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : Stat
{
    public UnityEvent<Health> OnDeath;
    public override void OnValueChange()
    {
        //if (Value <= 0) then die
        if (Value <= 0)
        {
            OnDeath?.Invoke(this);
        }

        base.OnValueChange();
    }
}
