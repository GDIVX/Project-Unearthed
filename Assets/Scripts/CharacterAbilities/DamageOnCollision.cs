using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageOnCollision : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to deal on collision

    private void OnTriggerEnter(Collider other)
    {
        OnCollision(other);
    }

    public virtual void OnCollision(Collider other)
    {
        if (!other.TryGetComponent<DamageHandler>(out var damageHandler)) // Check if the collided object has a Health component
        {
            return;
        }
        damageHandler.HandleTakingDamage(damageAmount); // Call the TakeDamage method on the Health component to deal damage
    }
}
