using System.Collections;
using UnityEngine;

public class Armor : RegeneratingStats
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
