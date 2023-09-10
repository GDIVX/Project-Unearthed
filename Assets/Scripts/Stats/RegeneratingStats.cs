using Assets.Scripts.Stats;
using System.Collections;

public abstract class RegeneratingStats : Stat
{
    public abstract float RegenRateInSeconds { get ; set ; }
    public abstract IEnumerator Regenerate(int healAmount);
    public void Heal(int healAmount)
    {
        if (healAmount <= 0) return;
        Value += healAmount;
    }
}
