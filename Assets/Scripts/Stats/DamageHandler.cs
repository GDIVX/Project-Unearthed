public class DamageHandler
{
    private IDamageable[] damageables;

    public DamageHandler(IDamageable[] damageable)
    {
        this.damageables = damageable;
    }

    public void TakeDamage(int damageAmount)
    {
        foreach (var damageable in damageables)
        {
            if (damageAmount <= 0) return;
            if (damageable.IsInvincible) continue;
            damageAmount = damageable.TakeDamage(damageAmount);
        }
    }
}
