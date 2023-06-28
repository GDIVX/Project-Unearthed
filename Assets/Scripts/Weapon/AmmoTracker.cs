using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTracker : MonoBehaviour, IAmmoTracker
{
    [SerializeField] int _ammoCount;
    public void AddAmmo(int amount)
    {
        _ammoCount += amount;
    }

    public int GetAmmoCount()
    {
        return _ammoCount;
    }
}
