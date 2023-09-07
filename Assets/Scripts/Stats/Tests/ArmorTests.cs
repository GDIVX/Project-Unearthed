using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ArmorTests : MonoBehaviour
{
    private Armor _armor;
    private GameObject _character;
    private Health _hp;
    private DamageHandler _damageHandler;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _character = new GameObject();
        _armor = _character.AddComponent<Armor>();
        _hp = _character.AddComponent<Health>();
        IDamageable[] damageables = { _armor, _hp };
        _damageHandler = new DamageHandler(damageables);
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
        GameObject.Destroy(_hp);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_ArmorPointsExceedingDamageTaken()
    {
        int currentAP = _armor.Value;
        _damageHandler.TakeDamage(1);
        Assert.IsTrue(_armor.Value < currentAP);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_ArmorPointsLessThanDamageTaken()
    {
        int currentHP = _hp.Value;
        _damageHandler.TakeDamage(11);
        Assert.IsTrue(_armor.Value == 0);
        Assert.IsTrue(_hp.Value < currentHP);
        yield return null;
    } 
    
    [UnityTest]
    public IEnumerator Armor_ArmorPointsZeroAndTakingDamage()
    {
        int currentHP = _hp.Value;
        _armor.Value = 0;
        _damageHandler.TakeDamage(1);
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
        _damageHandler.TakeDamage(_armor.Value);
        yield return new WaitForSeconds(4.0f);
        Assert.IsTrue(_armor.Value > 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_RegeneratingAllArmorNotAboveMax()
    {
        _damageHandler.TakeDamage(_armor.MaxValue);
        Assert.IsTrue(_armor.Value == 0);
        yield return new WaitForSeconds(_armor.MaxValue * 2);
        Assert.IsTrue(_armor.Value == _armor.MaxValue);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_TakingDamageMultipleTimes()
    {
        int currentAP = _armor.Value;
        _damageHandler.TakeDamage(1);
        yield return new WaitForSeconds(1.0f);
        _damageHandler.TakeDamage(1);
        yield return new WaitForSeconds(1.0f);
        _damageHandler.TakeDamage(1);
        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(_armor.Value == currentAP - 3);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_RegeneratingArmorFromMoreThanZero()
    {
        _damageHandler.TakeDamage(_armor.Value - 1);
        yield return new WaitForSeconds(4.0f);
        Assert.IsTrue(_armor.Value > 1); 
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_TakingDamageWhenInvincible()
    {
        int currentAP = _armor.Value;
        _armor.IsInvincible = true;
        _damageHandler.TakeDamage(1);
        Assert.IsTrue(_armor.Value == currentAP);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_TakingNegativeDamage()
    {
        int currentAP = _armor.Value;
        _damageHandler.TakeDamage(-1);
        Assert.IsTrue(_armor.Value == currentAP); 
        yield return null;
    }
}
