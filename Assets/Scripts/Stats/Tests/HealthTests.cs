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
        _hp.Value = _hp.MaxValue; 
        int healAmount = _hp.MaxValue + 1;
        _hp.Value = _hp.MaxValue - 1;
        _hp.Regenerate(healAmount);
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
        _hp.IsInvincible = true;
        int currentHP = _hp.Value;
        int damageAmount = 1;
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
        _hp.Value = _hp.MaxValue - amount;
        _hp.IsInvincible = true;
        _hp.Regenerate(amount);
        Assert.IsTrue(_hp.Value == _hp.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingWhileDead()
    {
        int amount = 1;
        _hp.Value = _hp.MaxValue - amount;
        int currentHP = _hp.Value;
        _hp.IsDead = true;
        _hp.Regenerate(amount);
        Assert.IsTrue(currentHP == _hp.Value);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingNegativeAmount()
    {
        int amount = -1;
        _hp.TakeDamage(-amount);
        int currentHP = _hp.Value;
        _hp.Regenerate(amount);
        Assert.IsTrue(currentHP == _hp.Value);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingWhenTakingDamage()
    {
        _hp.MaxValue = 10;
        _hp.Value = _hp.MaxValue; 
        int damageAmount = _hp.MaxValue / 2;
        int healingAmount = _hp.MaxValue;
        _hp.Regenerate(healingAmount);
        _hp.TakeDamage(damageAmount);
        yield return new WaitForSeconds(0.5f);
        Assert.IsTrue(_hp.Value == _hp.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Health_HealingWhenHealthReachesZeroMidway()
    {
        int healingAmount = 10;
        _hp.Regenerate(healingAmount);
        _hp.Value = 0;
        yield return new WaitForSeconds(0.5f);
        Assert.IsTrue(_hp.Value == 0);
        yield return null;
    }
}
