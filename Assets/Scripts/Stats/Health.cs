using Assets.Scripts.Stats;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : RegeneratingStats
{
    [SerializeField] float _regenRateInSeconds;
    [SerializeField] Armor _armor;
    [SerializeField, ReadOnly] bool _isInvincible;
    [SerializeField, ReadOnly] bool _isDead;
    [SerializeField, ReadOnly] int _tempHealth;

    public UnityEvent<Health> OnDeath;

    public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }
    public int TempHealth { get => _tempHealth; set => _tempHealth = value; }
    public Armor Armor { get => _armor; set => _armor = value; }
    protected override float RegenRateInSeconds { get { return _regenRateInSeconds; } set { _regenRateInSeconds = value; } }

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
        if(_armor != null)
        {
            _armor.Value = SubtractValues(ref damageAmount, _armor.Value);
        }
        TempHealth = SubtractValues(ref damageAmount, TempHealth);
        Value -= damageAmount;
        Debug.Log("deal damage " + damageAmount);
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
        Debug.Log("finished regenerating!");
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
