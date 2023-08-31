using Assets.Scripts.Stats;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : Stat
{
    [SerializeField] bool _isInvincible;
    [SerializeField, ReadOnly] bool _isDead;

    public UnityEvent<Health> OnDeath;

    public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }

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

    public void Regenerate(int healAmount)
    {

    }

    public void SetInvincibilityForSeconds(float seconds)
    {
        IsInvincible = true;
        StartCoroutine(TimedInvincibilityCoroutine(seconds));
    }

    private IEnumerator TimedInvincibilityCoroutine(float seconds)
    {
        IsInvincible = true;

        yield return new WaitForSeconds(seconds);

        IsInvincible = false;
    }
}
