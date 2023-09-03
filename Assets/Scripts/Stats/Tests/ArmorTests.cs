using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class ArmorTests : MonoBehaviour
{
    private Armor _armor;
    private GameObject _character;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _character = new GameObject();
        _armor = _character.AddComponent<Armor>();
        _armor.MaxValue = 10;
        _armor.Value = _armor.MaxValue;
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
    public IEnumerator Armor_ArmorPointsCanReduce()
    {
        yield return null;
    }
}
