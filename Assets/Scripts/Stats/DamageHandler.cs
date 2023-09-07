using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] Armor _armor;
    [SerializeField] Health _health;

    public bool IsInvincible { get; set; }
    public Armor Armor { get { return _armor; } set { _armor = value; } }
    public Health Health { get { return _health; } set { _health = value; } }


    // This method takes an int, and goes through health and armor components
    // and invokes TakeDamage on each one.
    // TakeDamage returns an int representing the damage the next damageable needs to handle.
    public void HandleTakingDamage(int damageAmount)
    {
        if (damageAmount <= 0 || IsInvincible) return;
        if(_armor != null)  damageAmount = _armor.TakeDamage(damageAmount);
        _health.TakeDamage(damageAmount);
    }

    public void SetInvincibilityForSeconds(float seconds)
    {
        IsInvincible = true;
        StartCoroutine(TimedInvincibilityCoroutine(seconds));
    }

    public IEnumerator TimedInvincibilityCoroutine(float seconds)
    {
        IsInvincible = true;

        yield return new WaitForSeconds(seconds);

        IsInvincible = false;
    }

}
