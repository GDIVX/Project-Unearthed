using Assets.Scripts.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CharacterAbilities
{
    /// <summary>
    /// This class is used to pull items towards the player
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class PickupPull : MonoBehaviour
    {
        [SerializeField] private float pullForce = 1f;
        [SerializeField] Inventory inventory;

        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Pickup"))
            {
                return;
            }
            if (!other.GetComponent<IPickup>().CanPickup(inventory))
            {
                return;
            }

            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, pullForce);
        }
    }
}
