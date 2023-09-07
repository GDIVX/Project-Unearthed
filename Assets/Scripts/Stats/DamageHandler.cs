using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private List<IDamageable> damageables;
    [SerializeField] int a;

    public DamageHandler(List<IDamageable> damageable)
    {
        this.damageables = damageable;
    }

    // This method takes an int, and goes through list of damageables
    // and invokes TakeDamage on each one.
    // TakeDamage returns an int representing the damage the next damageable needs to handle.
    public void HandleTakingDamage(int damageAmount)
    {
        foreach (var damageable in damageables)
        {
            if (damageAmount <= 0 || damageable.IsInvincible) return;
            damageAmount = damageable.TakeDamage(damageAmount);
        }
    }
}
