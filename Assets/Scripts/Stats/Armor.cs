using System.Collections;
using UnityEngine;

public class Armor : RegeneratingStats
{
    [SerializeField] int _cooldownInSeconds = 2;
    
    private Coroutine cooldownCoroutine;

    protected override float RegenRateInSeconds { get; set; } = 0.5f;

    public override void OnValueChange()
    {
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
        cooldownCoroutine = StartCoroutine(Cooldown());
        base.OnValueChange();
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldownInSeconds);
        StartCoroutine(Regenerate(MaxValue - Value));
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
        Debug.Log("finished regenerating!");
    }
}
