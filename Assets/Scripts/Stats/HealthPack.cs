using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] string _playerTag;
    Health _playerHealth;
    private void OnTriggerEnter(Collider playerCollision)
    {
        if (!playerCollision.CompareTag(_playerTag.ToString()))
            return;
    }
}
