using System.Collections;
using UnityEngine;

public class Armor : RegeneratingStats, IDamageable
{
    [SerializeField] int _cooldownInSeconds = 2;
    
    private Coroutine _cooldownCoroutine;
    private Coroutine _regeneratingCoroutine;

    protected override float RegenRateInSeconds { get; set; } = 0.5f;

    public override void OnValueChange()
    {
        if (_cooldownCoroutine != null)
        {
            StopCoroutine(_cooldownCoroutine);
        }
        if (_regeneratingCoroutine != null)
        {
            StopCoroutine(_regeneratingCoroutine);
        }
        _cooldownCoroutine = StartCoroutine(Cooldown());
        base.OnValueChange();
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldownInSeconds);
        _regeneratingCoroutine = StartCoroutine(Regenerate(MaxValue - Value));
    }

    public override IEnumerator Regenerate(int healAmount)
    {
        if (healAmount <= 0) yield break;
        int counter = 0;
        while (counter < healAmount)
        {
            counter++;
            if (Value < MaxValue) Value++;
            yield return new WaitForSeconds(RegenRateInSeconds);
        }
    }

    public int TakeDamage(int damageAmount)
    {
        Debug.Log("detected incoming damage: " + damageAmount);
        if (damageAmount < 0) damageAmount = 0;
        Value = SubtractValues(ref damageAmount, Value);
        Debug.Log("Armor after taking damage: " + Value);
        Debug.Log("deal damage " + damageAmount);
        return damageAmount;
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
