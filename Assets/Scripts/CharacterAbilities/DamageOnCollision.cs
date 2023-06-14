using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageOnCollision : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to deal on collision

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Health>(out var health)) // Check if the collided object has a Health component
        {
            health.TakeDamage(damageAmount); // Call the TakeDamage method on the Health component to deal damage
        }
    }
}
