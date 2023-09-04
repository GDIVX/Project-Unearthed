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
        int currentAP = _armor.Value;
        _armor.TakeDamage(1);
        Assert.IsTrue(_armor.Value < currentAP);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_ArmorPointsLessThanDamageTaken()
    {
        yield return null;
    } 
    
    [UnityTest]
    public IEnumerator Armor_ArmorPointsZeroAndTakingDamage()
    {
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Armor_NegativeArmorPoints()
    {
        yield return null;
    } 
    
    [UnityTest]
    public IEnumerator Armor_RegeneratingArmorFromZero()
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_RegeneratingArmorFromMoreThanZero()
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator Armor_TakingDamageWhenInvincible()
    {
        yield return null;
    }
}
