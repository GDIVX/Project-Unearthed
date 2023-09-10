using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTests : MonoBehaviour
{
    private GameObject _character;
    private Health _health;
    private DamageHandler _damageHandler;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _character = new GameObject();
        _health = _character.AddComponent<Health>();
        _damageHandler = _character.AddComponent<DamageHandler>();
        _damageHandler.Health = _health;
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        _health.DrainRateInSeconds = 1;
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(_health);
        GameObject.Destroy(_damageHandler);
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        yield return null;
    }

    [UnityTest]
    public IEnumerator Health_DiesWhenHealthReachesZero()
    {
        _health.Value = 0;
        Assert.IsTrue(_health.IsDead);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_NotDeadWhenHealthAboveZero()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        Assert.IsFalse(_health.IsDead);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_RegenerateNegativeHealth()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue - 1;
        int currentHP = _health.Value;
        int healAmount = -1;
        _health.Regenerate(healAmount);
        Assert.IsTrue(currentHP == _health.Value);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingAmountAboveMaxHealth()
    {
        _health.MaxValue = 10;
        int healAmount = _health.MaxValue + 1;
        _health.Value = _health.MaxValue - 1;
        _damageHandler.HandleTakingDamage(1);
        yield return _health.Regenerate(healAmount);
        Assert.IsTrue(_health.Value == _health.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TakeDamage()
    {

        _health.MaxValue = 10;
        _health.Value = _health.MaxValue; 
        int currentHP = _health.Value;
        int damageAmount = 1;
        _damageHandler.HandleTakingDamage(damageAmount);
        Assert.IsTrue(_health.Value < currentHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TakeNegativeDamage()
    {

        _health.MaxValue = 10;
        _health.Value = _health.MaxValue - 5;
        int currentHP = _health.Value;
        int damageAmount = -1;
        _damageHandler.HandleTakingDamage(damageAmount);
        Assert.IsTrue(_health.Value == currentHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TakingDamageWhileInvincible()
    {

        int seconds = 3;
        int damageAmount = 1;
        int currentHP = _health.Value;
        _damageHandler.SetInvincibilityForSeconds(seconds);
        _damageHandler.HandleTakingDamage(damageAmount);
        Assert.IsTrue(_health.Value == currentHP); 
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TakingDamageWhileDead()
    {

        _health.Value = 0;
        int damageAmount = 1;
        _damageHandler.HandleTakingDamage(damageAmount);
        Assert.IsTrue(_health.Value == 0);
        Assert.IsTrue(_health.IsDead);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingWhileInvincible()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue; 
        int amount = 1;
        int seconds = 3;
        _health.Value = _health.MaxValue - amount;
        _damageHandler.SetInvincibilityForSeconds(seconds);
        yield return _health.Regenerate(amount);
        Assert.IsTrue(_health.Value == _health.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_InvincibilityRunningOut()
    {
        int seconds = 3;
        _damageHandler.SetInvincibilityForSeconds(seconds);
        yield return new WaitForSeconds(seconds);
        Assert.IsFalse(_damageHandler.IsInvincible);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingWhileDead()
    {

        _damageHandler.HandleTakingDamage(_health.MaxValue);
        yield return _health.Regenerate(1);
        Assert.IsTrue(_health.Value == 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingNegativeAmount()
    {
        int amount = -1;
        _damageHandler.HandleTakingDamage(-amount);
        int currentHP = _health.Value;
        yield return _health.Regenerate(amount);
        Assert.IsTrue(currentHP == _health.Value);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_CanSetInvincibility()
    {
        int seconds = 4;
        Assert.IsFalse(_damageHandler.IsInvincible);
        _damageHandler.SetInvincibilityForSeconds(seconds);
        Assert.IsTrue(_damageHandler.IsInvincible);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingWhenHealthReachesZeroMidway()
    {
        int healingAmount = 10;
        yield return _health.Regenerate(healingAmount);
        _health.Value = 0;
        Assert.IsTrue(_health.Value == 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_CanGainTemporaryHealthAboveMaxHealth()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        _health.DrainRateInSeconds = 1;
        int tmpHP = 3;
        _health.AddTemporaryHealth(3);
        Assert.IsTrue(_health.Value + _health.TempHealth == _health.MaxValue + tmpHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TemporaryHealthExceedingDamageTaken()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        int tmpHP = 3;
        int currentHP = _health.Value;
        _health.AddTemporaryHealth(3);
        _damageHandler.HandleTakingDamage(tmpHP - 1);
        Assert.IsTrue(_health.Value == currentHP);
        Assert.IsTrue(_health.TempHealth == tmpHP - 2);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TemporaryHealthLowerThanDamageTaken()
    {

        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        int tmpHP = 3;
        int currentHP = _health.Value;
        _health.AddTemporaryHealth(tmpHP);
        _damageHandler.HandleTakingDamage(tmpHP + 1);
        Assert.IsTrue(_health.Value < currentHP);
        Assert.IsTrue(_health.TempHealth == 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_NegativeDamageWithTemporaryHealth()
    {

        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        int tmpHP = 3;
        int currentHP = _health.Value;
        _health.AddTemporaryHealth(tmpHP);
        _damageHandler.HandleTakingDamage(-1);
        Assert.IsTrue(_health.Value == currentHP);
        Assert.IsTrue(_health.TempHealth == tmpHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_CannotGainNegativeTemporaryHealth()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        _health.TempHealth = 0;
        int tmpHP = -3;
        _health.AddTemporaryHealth(tmpHP);
        Assert.IsTrue(_health.TempHealth == 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Health_DrainTemporaryHealth()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        _health.TempHealth = 0;
        _health.DrainRateInSeconds = 1;
        int tmpHP = 3;
        _health.AddTemporaryHealth(tmpHP);
        Assert.IsTrue(_health.TempHealth > 0);
        yield return new WaitForSeconds(tmpHP * _health.DrainRateInSeconds + _health.DrainRateInSeconds);
        Assert.IsTrue(_health.TempHealth == 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Health_HealingNegativeHealth()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue; 
        int healAmount = -1;
        _damageHandler.HandleTakingDamage(1);
        _health.Heal(healAmount);
        Assert.IsTrue(_health.Value < _health.MaxValue); 
        yield return null;
    }

    [UnityTest]
    public IEnumerator Health_HealingAboveMaxHealth()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        int healAmount = 1;
        _health.Heal(healAmount);
        Assert.IsTrue(_health.Value == _health.MaxValue);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Health_TakingDamageThenHealing()
    {
        _health.MaxValue = 10;
        _health.Value = _health.MaxValue;
        int healAmount = 1;
        int damageAmount = healAmount;
        _damageHandler.HandleTakingDamage(damageAmount);
        _health.Heal(healAmount);
        Assert.IsTrue(_health.Value == _health.MaxValue);
        yield return null;
    }
}
