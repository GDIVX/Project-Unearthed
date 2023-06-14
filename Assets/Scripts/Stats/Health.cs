using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : Stat
{
    [SerializeField] bool _isInvincible;

    public UnityEvent<Health> OnDeath;

    public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }

    public override void OnValueChange()
    {
        //if (Value <= 0) then die
        if (Value <= 0)
        {
            OnDeath?.Invoke(this);
        }

        base.OnValueChange();
    }

    public void TakeDamage(int damageAmount)
    {
        if (IsInvincible) return;
        Value -= damageAmount;

        Debug.Log("deal damage " + damageAmount);
    }
}
