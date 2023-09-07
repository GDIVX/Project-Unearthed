using System.Collections;

public interface IDamageable
{
    public bool IsInvincible { get; set; }
    public void SetInvincibilityForSeconds(float seconds);
    public IEnumerator TimedInvincibilityCoroutine(float seconds);
    int TakeDamage(int damageAmount);

}
