using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class ArmorTests 
{
    private GameObject _character;
    private Armor _armor;
    private Health _health;
    private DamageHandler _damageHandler;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _character = new GameObject();
        _health = _character.AddComponent<Health>();
        _armor = _character.AddComponent<Armor>();
        _damageHandler = _character.AddComponent<DamageHandler>();
        _damageHandler.Armor = _armor;
        _damageHandler.Health = _health;
        _damageHandler.IsInvincible = false;
        _armor.MaxValue = 10;
        _armor.Value = _armor.MaxValue;
        _health.MaxValue = 10;
        _health.Value = _armor.MaxValue;
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(_armor);
        GameObject.Destroy(_health);
        GameObject.Destroy(_character);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_ArmorPointsExceedingDamageTaken()
    {
        int currentAP = _armor.Value;
        _damageHandler.HandleTakingDamage(1);
        Assert.IsTrue(_armor.Value < currentAP);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_ArmorPointsLessThanDamageTaken()
    {
        int currentHP = _health.Value;
        _damageHandler.HandleTakingDamage(11);
        Assert.IsTrue(_armor.Value == 0);
        Assert.IsTrue(_health.Value < currentHP);
        yield return null;
    } 
    
    [UnityTest]
    public IEnumerator Armor_ArmorPointsZeroAndTakingDamage()
    {
        int currentHP = _health.Value;
        _armor.Value = 0;
        _damageHandler.HandleTakingDamage(1);
        Assert.IsTrue(_armor.Value == 0);
        Assert.IsTrue(_health.Value < currentHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_NegativeArmorPoints()
    {
        _armor.Value = -1;
        Assert.IsTrue(_armor.Value == 0);
        yield return null;
    } 
    
    [UnityTest]
    public IEnumerator Armor_RegeneratingArmorFromZero()
    {
        _damageHandler.HandleTakingDamage(_armor.Value);
        yield return new WaitForSeconds(4.0f);
        Assert.IsTrue(_armor.Value > 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_RegeneratingAllArmorNotAboveMax()
    {
        _damageHandler.HandleTakingDamage(_armor.MaxValue);
        Assert.IsTrue(_armor.Value == 0);
        yield return new WaitForSeconds(_armor.MaxValue * 2);
        Assert.IsTrue(_armor.Value == _armor.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_TakingDamageMultipleTimes()
    {
        int currentAP = _armor.Value;
        _damageHandler.HandleTakingDamage(1);
        yield return new WaitForSeconds(1.0f);
        _damageHandler.HandleTakingDamage(1);
        yield return new WaitForSeconds(1.0f);
        _damageHandler.HandleTakingDamage(1);
        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(_armor.Value == currentAP - 3);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_RegeneratingArmorFromMoreThanZero()
    {
        _damageHandler.HandleTakingDamage(_armor.Value - 1);
        yield return new WaitForSeconds(4.0f);
        Assert.IsTrue(_armor.Value > 1); 
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_TakingDamageWhenInvincible()
    {
        int currentAP = _armor.Value;
        int seconds = 3;
        _damageHandler.SetInvincibilityForSeconds(seconds);
        _damageHandler.HandleTakingDamage(1);
        Assert.IsTrue(_armor.Value == currentAP);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_TakingNegativeDamage()
    {
        int currentAP = _armor.Value;
        _damageHandler.HandleTakingDamage(-1);
        Assert.IsTrue(_armor.Value == currentAP); 
        yield return null;
    }
}
