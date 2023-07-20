using Assets.Scripts.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks the amount of ammo without using an inventory system.
/// </summary>
public class AmmoTracker : MonoBehaviour, IAmmoTracker
{
    [SerializeField] int _ammoCount;


    public void AddAmmo(int amount, Item type)
    {
        _ammoCount += amount;
    }



    public int GetAmmoCount(Item type)
    {
        return _ammoCount;
    }
}
