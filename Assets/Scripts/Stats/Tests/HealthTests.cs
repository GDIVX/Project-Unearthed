using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTests : MonoBehaviour
{
    private Health _hp;
    private GameObject _character;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _character = new GameObject();
        _hp = _character.AddComponent<Health>();
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue; 
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(_character);
        GameObject.Destroy(_hp);
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue;
        yield return null;
    }

    [UnityTest]
    public IEnumerator Health_DiesWhenHealthReachesZero()
    {
        _hp.Value = 0;
        _hp.OnValueChange();
        Assert.IsTrue(_hp.IsDead);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_NotDeadWhenHealthAboveZero()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue;
        Assert.IsFalse(_hp.IsDead);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_RegenerateNegativeHealth()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue - 1;
        int currentHP = _hp.Value;
        int healAmount = -1;
        _hp.Regenerate(healAmount);
        Assert.IsTrue(currentHP == _hp.Value);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingAmountAboveMaxHealth()
    {
        _hp.MaxValue = 10;
        int healAmount = _hp.MaxValue + 1;
        _hp.Value = _hp.MaxValue - 1;
        _hp.TakeDamage(1);
        yield return _hp.Regenerate(healAmount);
        Assert.IsTrue(_hp.Value == _hp.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TakeDamage()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue; 
        int currentHP = _hp.Value;
        int damageAmount = 1;
        _hp.TakeDamage(damageAmount);
        Assert.IsTrue(_hp.Value < currentHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TakeNegativeDamage()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue - 5;
        int currentHP = _hp.Value;
        int damageAmount = -1;
        _hp.TakeDamage(damageAmount);
        Assert.IsTrue(_hp.Value == currentHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TakingDamageWhileInvincible()
    {
        int seconds = 3;
        int damageAmount = 1;
        int currentHP = _hp.Value;
        _hp.SetInvincibilityForSeconds(seconds);
        _hp.TakeDamage(damageAmount);
        Assert.IsTrue(_hp.Value == currentHP); 
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TakingDamageWhileDead()
    {
        _hp.Value = 0;
        int damageAmount = 1;
        _hp.TakeDamage(damageAmount);
        Assert.IsTrue(_hp.Value == 0);
        Assert.IsTrue(_hp.IsDead);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingWhileInvincible()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue; 
        int amount = 1;
        int seconds = 3;
        _hp.Value = _hp.MaxValue - amount;
        _hp.SetInvincibilityForSeconds(seconds);
        yield return _hp.Regenerate(amount);
        Assert.IsTrue(_hp.Value == _hp.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_InvincibilityRunningOut()
    {
        int seconds = 3;
        _hp.SetInvincibilityForSeconds(seconds);
        yield return new WaitForSeconds(seconds);
        Assert.IsFalse(_hp.IsInvincible);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingWhileDead()
    {
        int amount = 1;
        _hp.Value = _hp.MaxValue - amount;
        int currentHP = _hp.Value;
        _hp.IsDead = true;
        yield return _hp.Regenerate(amount);
        Assert.IsTrue(currentHP == _hp.Value);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingNegativeAmount()
    {
        int amount = -1;
        _hp.TakeDamage(-amount);
        int currentHP = _hp.Value;
        yield return _hp.Regenerate(amount);
        Assert.IsTrue(currentHP == _hp.Value);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_CanSetInvincibility()
    {
        int seconds = 4;
        Assert.IsFalse(_hp.IsInvincible);
        _hp.SetInvincibilityForSeconds(seconds);
        Assert.IsTrue(_hp.IsInvincible);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingWhenHealthReachesZeroMidway()/////////////
    {
        int healingAmount = 10;
        yield return _hp.Regenerate(healingAmount);
        _hp.Value = 0;
        Assert.IsTrue(_hp.Value == 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_CanGainTemporaryHealthAboveMaxHealth()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue;
        int tmpHP = 3;
        _hp.AddTemporaryHealth(3);
        Assert.IsTrue(_hp.Value + _hp.TempHealth == _hp.MaxValue + tmpHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TemporaryHealthExceedingDamageTaken()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue;
        int tmpHP = 3;
        int currentHP = _hp.Value;
        _hp.AddTemporaryHealth(3);
        _hp.TakeDamage(tmpHP - 1);
        Assert.IsTrue(_hp.Value == currentHP);
        Assert.IsTrue(_hp.TempHealth == tmpHP - 2);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_TemporaryHealthLowerThanDamageTaken()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue;
        int tmpHP = 3;
        int currentHP = _hp.Value;
        _hp.AddTemporaryHealth(tmpHP);
        _hp.TakeDamage(tmpHP + 1);
        Assert.IsTrue(_hp.Value < currentHP);
        Assert.IsTrue(_hp.TempHealth == 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_NegativeDamageWithTemporaryHealth()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue;
        int tmpHP = 3;
        int currentHP = _hp.Value;
        _hp.AddTemporaryHealth(tmpHP);
        _hp.TakeDamage(-1);
        Assert.IsTrue(_hp.Value == currentHP);
        Assert.IsTrue(_hp.TempHealth == tmpHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_CannotGainNegativeTemporaryHealth()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue;
        _hp.TempHealth = 0;
        int tmpHP = -3;
        _hp.AddTemporaryHealth(tmpHP);
        Assert.IsTrue(_hp.TempHealth == 0);
        yield return null;
    }
}
