using System.Collections.Generic;

public class DamageHandler
{
    private List<IDamageable> damageables;

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
