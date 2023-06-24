using System.Collections;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    public interface IPickup
    {
        bool CanPickup(Inventory inventory);
    }
}