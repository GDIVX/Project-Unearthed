using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : RegeneratingStats, IDamageable
{
    [SerializeField, Min(0)] float _regenRateInSeconds;
    [SerializeField, Min(0)] float _drainRateInSeconds;
    [SerializeField, Min(0)] int _drainAmount = 1;
    [SerializeField, ReadOnly, Min(0)] int _tempHealth;

    public UnityEvent<Health> OnDeath;

    public bool IsDead => Value <= 0;
    public int TempHealth { get => _tempHealth; set => _tempHealth = value; }
    public float DrainRateInSeconds { get { return _drainRateInSeconds; } set { _drainRateInSeconds = value; } }
    public override float RegenRateInSeconds { get { return _regenRateInSeconds; } set { _regenRateInSeconds = value; } }

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
        StartCoroutine(DrainTemporaryHealthOverTime());
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
            yield return new WaitForSeconds(RegenRateInSeconds);
        }
    }

    private IEnumerator DrainTemporaryHealthOverTime()
    {
        while(TempHealth > 0)
        {
            yield return new WaitForSeconds(DrainRateInSeconds);
            TempHealth -= _drainAmount;
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
