using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : RegeneratingStats, IDamageable
{
    [SerializeField] float _regenRateInSeconds;
    [SerializeField, ReadOnly] int _tempHealth;

    public UnityEvent<Health> OnDeath;

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
        if (IsDead) return -1;
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
