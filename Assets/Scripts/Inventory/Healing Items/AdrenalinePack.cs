using UnityEngine;

public class AdrenalinePack : InstantEffectPickup
{
    [SerializeField] string _playerTag;
    [SerializeField] int _tempHealthAmount;

    Health _playerHealth;

    private void DestroyHealthPack()
    {
        Destroy(_gameObject);
    }

    protected override void CauseEffect(Collider playerCollision)
    {
        _playerHealth = playerCollision.GetComponentInParent<Health>();
        if (_playerHealth == null) return;
        _playerHealth.AddTemporaryHealth(_tempHealthAmount);
        DestroyHealthPack();
    }
}
