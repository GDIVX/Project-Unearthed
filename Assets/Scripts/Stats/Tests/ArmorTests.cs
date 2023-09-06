using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ArmorTests : MonoBehaviour
{
    private Armor _armor;
    private GameObject _character;
    private Health _hp;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _character = new GameObject();
        _armor = _character.AddComponent<Armor>();
        _hp = _character.AddComponent<Health>();
        _hp.Armor = _armor;
        _armor.MaxValue = 10;
        _armor.Value = _armor.MaxValue;
        _hp.MaxValue = 10;
        _hp.Value = _armor.MaxValue;
        
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(_character);
        GameObject.Destroy(_armor);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_ArmorPointsExceedingDamageTaken()
    {
        int currentAP = _hp.Armor.Value;
        _hp.TakeDamage(1);
        Assert.IsTrue(_hp.Armor.Value < currentAP);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_ArmorPointsLessThanDamageTaken()
    {
        int currentHP = _hp.Value;
        _hp.TakeDamage(11);
        Assert.IsTrue(_hp.Armor.Value == 0);
        Assert.IsTrue(_hp.Value < currentHP);
        yield return null;
    } 
    
    [UnityTest]
    public IEnumerator Armor_ArmorPointsZeroAndTakingDamage()
    {
        int currentHP = _hp.Value;
        _hp.Armor.Value = 0;
        _hp.TakeDamage(1);
        Assert.IsTrue(_hp.Armor.Value == 0);
        Assert.IsTrue(_hp.Value < currentHP);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_NegativeArmorPoints()
    {
        _hp.Armor.Value = -1;
        Assert.IsTrue(_hp.Armor.Value == 0);
        yield return null;
    } 
    
    [UnityTest]
    public IEnumerator Armor_RegeneratingArmorFromZero()
    {
        _hp.TakeDamage(_hp.Armor.Value);
        yield return new WaitForSeconds(4.0f);
        Assert.IsTrue(_hp.Armor.Value > 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_RegeneratingAllArmorNotAboveMax()
    {
        _hp.TakeDamage(_hp.Armor.MaxValue);
        Assert.IsTrue(_hp.Armor.Value == 0);
        yield return new WaitForSeconds(_hp.Armor.MaxValue * 2);
        Assert.IsTrue(_hp.Armor.Value == _hp.Armor.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_TakingDamageMultipleTimes()
    {
        int currentAP = _hp.Armor.Value;
        _hp.TakeDamage(1);
        yield return new WaitForSeconds(1.0f);
        _hp.TakeDamage(1);
        yield return new WaitForSeconds(1.0f);
        _hp.TakeDamage(1);
        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(_hp.Armor.Value == currentAP - 3);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_RegeneratingArmorFromMoreThanZero()
    {
        _hp.TakeDamage(_hp.Armor.Value - 1);
        yield return new WaitForSeconds(4.0f);
        Assert.IsTrue(_hp.Armor.Value > 1); 
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_TakingDamageWhenInvincible()
    {
        int seconds = 5;
        int currentAP = _hp.Armor.Value;
        _hp.SetInvincibilityForSeconds(seconds);
        _hp.TakeDamage(3);
        Assert.IsTrue(_hp.Armor.Value == currentAP);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_TakingNegativeDamage()
    {
        int currentAP = _hp.Armor.Value;
        _hp.TakeDamage(-3);
        Assert.IsTrue(_hp.Armor.Value == currentAP); 
        yield return null;
    }
}
