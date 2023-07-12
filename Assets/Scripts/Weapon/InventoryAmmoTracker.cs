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

    public void AddAmmo(int amount, Item ammoType)
    {

        //Update the inventory
        _inventory.AddItem(ammoType, amount);
    }

    public int GetAmmoCount(Item ammoType)
    {

        return _inventory.Count(ammoType);
    }

}
