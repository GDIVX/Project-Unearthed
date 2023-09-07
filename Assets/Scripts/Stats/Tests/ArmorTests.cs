using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class ArmorTests : MonoBehaviour
{
    private Armor _armor;
    private GameObject _character;
    private Health _hp;
    private DamageHandler _damageHandler;
    List<IDamageable> damageables = new List<IDamageable>();


    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _character = new GameObject();
        _armor = _character.AddComponent<Armor>();
        _hp = _character.AddComponent<Health>();
        _armor.MaxValue = 10;
        _armor.Value = _armor.MaxValue;
        _hp.MaxValue = 10;
        _hp.Value = _armor.MaxValue;
        SetUpDamageableList();
        _damageHandler = new DamageHandler(damageables);
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(_character);
        GameObject.Destroy(_armor);
        GameObject.Destroy(_hp);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_ArmorPointsExceedingDamageTaken()
    {
        SetUpDamageableList();
        int currentAP = _armor.Value;
        _damageHandler.HandleTakingDamage(1);
        Assert.IsTrue(_armor.Value < currentAP);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_ArmorPointsLessThanDamageTaken()
    {
        SetUpDamageableList();
        int currentHP = _hp.Value;
        _damageHandler.HandleTakingDamage(11);
        Assert.IsTrue(_armor.Value == 0);
        Assert.IsTrue(_hp.Value < currentHP);
        yield return null;
    } 
    
    [UnityTest]
    public IEnumerator Armor_ArmorPointsZeroAndTakingDamage()
    {
        SetUpDamageableList();
        int currentHP = _hp.Value;
        _armor.Value = 0;
        _damageHandler.HandleTakingDamage(1);
        Assert.IsTrue(_armor.Value == 0);
        Assert.IsTrue(_hp.Value < currentHP);
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
        SetUpDamageableList();
        _damageHandler.HandleTakingDamage(_armor.Value);
        yield return new WaitForSeconds(4.0f);
        Assert.IsTrue(_armor.Value > 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_RegeneratingAllArmorNotAboveMax()
    {
        SetUpDamageableList();
        _damageHandler.HandleTakingDamage(_armor.MaxValue);
        Assert.IsTrue(_armor.Value == 0);
        yield return new WaitForSeconds(_armor.MaxValue * 2);
        Assert.IsTrue(_armor.Value == _armor.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_TakingDamageMultipleTimes()
    {
        SetUpDamageableList();
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
        SetUpDamageableList();
        _damageHandler.HandleTakingDamage(_armor.Value - 1);
        yield return new WaitForSeconds(4.0f);
        Assert.IsTrue(_armor.Value > 1); 
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_TakingDamageWhenInvincible()
    {
        SetUpDamageableList();
        int currentAP = _armor.Value;
        _armor.IsInvincible = true;
        _damageHandler.HandleTakingDamage(1);
        Assert.IsTrue(_armor.Value == currentAP);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_TakingNegativeDamage()
    {
        SetUpDamageableList();
        int currentAP = _armor.Value;
        _damageHandler.HandleTakingDamage(-1);
        Assert.IsTrue(_armor.Value == currentAP); 
        yield return null;
    }

    private void SetUpDamageableList()
    {
        damageables.Clear();
        damageables.Add(_armor);
        damageables.Add(_hp);
    }
}
