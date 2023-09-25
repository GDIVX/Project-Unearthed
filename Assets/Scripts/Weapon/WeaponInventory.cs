using Assets.Scripts.Weapon;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField, Min(0)] private int _inventorySlots = 2;
    [SerializeField] List<Weapon> _weaponsInInventory = new List<Weapon>();

    public List<Weapon> WeaponsInInventory { get => _weaponsInInventory; private set => _weaponsInInventory = value; }

    public static WeaponInventory PlayerWeaponInventory;

    private void Awake()
    {
        if (PlayerWeaponInventory == null)
        {
            PlayerWeaponInventory = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddWeaponToList(Weapon weapon)
    {
        if (weapon == null) return;
        if (_inventorySlots >= WeaponsInInventory.Count)
        {
            Debug.Log("Exceeding weapon slots in inventory");
            return;
        }
        WeaponsInInventory.Add(weapon);
    }

    public void IncreaseWeaponSlots(int numberOfSlotsToAdd)
    {
        _inventorySlots += numberOfSlotsToAdd;
    }

    public void DecreaseWeaponSlots(int numberOfSlotsToRemove)
    {
        _inventorySlots -= numberOfSlotsToRemove;
    }

    public Weapon ReturnNextWeapon(Weapon currentWeapon)
    {
        Debug.Log(currentWeapon);
        if (currentWeapon == null || !WeaponsInInventory.Contains(currentWeapon)) return null;
        int currentWeaponIndex = WeaponsInInventory.IndexOf(currentWeapon);
        if (currentWeaponIndex >= WeaponsInInventory.Count - 1) return WeaponsInInventory[0]; //if it's the last weapon in list, wrap around to start of list
        Debug.Log(WeaponsInInventory[currentWeaponIndex]);
        return WeaponsInInventory[currentWeaponIndex + 1];
    }
}
