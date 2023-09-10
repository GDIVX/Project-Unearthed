using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    [SerializeField] string _playerTag;
    [SerializeField] int _healingPercentage = 10;

    Health _playerHealth;
    int _healAmount;

    private void OnTriggerEnter(Collider playerCollision)
    {
        if (!playerCollision.CompareTag(_playerTag.ToString()))
            return;
        _playerHealth = playerCollision.GetComponentInParent<Health>();
        if (_playerHealth == null) return;
        _healAmount = _playerHealth.MaxValue / _healingPercentage;
        HealPlayer();
    }

    private void HealPlayer()
    {
        _playerHealth.Heal(_healAmount);
        DestroyHealthDrop();
    }

    private void DestroyHealthDrop()
    {
        Destroy(gameObject);
    }
}
