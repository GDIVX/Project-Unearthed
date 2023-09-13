using UnityEngine;

public class HealthPack : InstantEffectPickup
{
    [SerializeField] string _playerTag;
    [SerializeField] int _healAmount;
    
    Health _playerHealth;

    private void DestroyHealthPack()
    {
        Destroy(gameObject);
    }

    protected override void CauseEffect(Collider playerCollision)
    {
        _playerHealth = playerCollision.GetComponentInParent<Health>();
        if (_playerHealth == null) return;
        _playerHealth.Heal(_healAmount);
        DestroyHealthPack();
    }
}
