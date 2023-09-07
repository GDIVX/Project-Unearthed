using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : RegeneratingStats, IDamageable
{
    [SerializeField] float _regenRateInSeconds;
    //[SerializeField] Armor _armor;
    [SerializeField, ReadOnly] bool _isInvincible;
    [SerializeField, ReadOnly] int _tempHealth;

    public UnityEvent<Health> OnDeath;

    public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
    public bool IsDead => Value <= 0;
    public int TempHealth { get => _tempHealth; set => _tempHealth = value; }
    //public Armor Armor { get => _armor; set => _armor = value; }
    protected override float RegenRateInSeconds { get { return _regenRateInSeconds; } set { _regenRateInSeconds = value; } }

    public override void OnValueChange()
    {
        //if (Value <= 0) then die
        if (Value <= 0)
        {
            OnDeath?.Invoke(this);
        }

        base.OnValueChange();
    }

    public int TakeDamage(int damageAmount)
    {
        if (IsInvincible || IsDead) return -1;
        if (damageAmount < 0) damageAmount = 0;
        TempHealth = SubtractValues(ref damageAmount, TempHealth);
        Value = SubtractValues(ref damageAmount, Value);
        Debug.Log("deal damage " + damageAmount);
        return damageAmount;
    }

    public void AddTemporaryHealth(int healthAmount)
    {
        if (healthAmount <= 0) return;
        TempHealth += healthAmount;
    }

    public override IEnumerator Regenerate(int healAmount)
    {
        if (healAmount <= 0) yield break;
        int counter = 0;
        while (counter < healAmount)
        {
            if (IsDead) yield break;
            counter++;
            if (Value < MaxValue) Value++;
            yield return new WaitForSeconds(_regenRateInSeconds);
        }
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
}
