using Assets.Scripts.InventorySystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAmmoTracker : MonoBehaviour, IAmmoTracker
{
    [SerializeField] Inventory _inventory;
    private void Start()
    {
        _inventory ??= GetComponent<Inventory>();
    }


    public int GetAmmoCount(AmmoType type)
    {
        throw new System.NotImplementedException();
    }

    public void AddAmmo(int amount, AmmoType type)
    {
        throw new System.NotImplementedException();
    }
}
