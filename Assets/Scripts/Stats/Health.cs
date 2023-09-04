using Assets.Scripts.Stats;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : Stat
{
    [SerializeField] float _regenRateInSeconds;
    [SerializeField] Armor _armor;
    [SerializeField, ReadOnly] bool _isInvincible;
    [SerializeField, ReadOnly] bool _isDead; ///////////////
    [SerializeField, ReadOnly] int _tempHealth;

    public UnityEvent<Health> OnDeath;

    public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }
    public int TempHealth { get => _tempHealth; set => _tempHealth = value; }

    public override void OnValueChange()
    {
        //if (Value <= 0) then die
        if (Value <= 0)
        {
            IsDead = true;
            OnDeath?.Invoke(this);
        }

        base.OnValueChange();
    }

    public void TakeDamage(int damageAmount)
    {
        if (IsInvincible || IsDead) return;
        if (damageAmount < 0) damageAmount = 0;
        int totalDamage = damageAmount;
        if(_armor != null)
        {
            _armor.Value = SubtractValues(ref damageAmount, _armor.Value);
        }
        TempHealth = SubtractValues(ref damageAmount, TempHealth);
        //damageAmount = Mathf.Clamp(damageAmount - TempHealth, 0, damageAmount);
        //TempHealth = Mathf.Clamp(TempHealth - totalDamage, 0, TempHealth);
        Value -= damageAmount;
        Debug.Log("deal damage " + damageAmount);
    }

    private int SubtractValues(ref int firstValue, int secondValue)
    {
        if (firstValue < secondValue)
        {
            secondValue = secondValue - firstValue;
            firstValue = 0;
        }
        else
        {
            firstValue = firstValue - secondValue;
            secondValue = 0;
        }
        return secondValue;
    }

    public IEnumerator Regenerate(int healAmount)
    {
        if (healAmount <= 0) yield break;
        int counter = 0;
        while(counter < healAmount)
        {
            if (IsDead) yield break;
            counter++;
            if (Value < MaxValue) Value++;
            yield return new WaitForSeconds(_regenRateInSeconds);
        }
        Debug.Log("finished regenerating!");
    }

    public void AddTemporaryHealth(int healthAmount)
    {
        if (healthAmount <= 0) return;
        TempHealth += healthAmount;
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
