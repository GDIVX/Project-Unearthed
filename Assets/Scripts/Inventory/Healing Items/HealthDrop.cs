using Assets.Scripts.InventorySystem;
using UnityEngine;

public class HealthDrop : InstantEffectPickup, IPickup
{
    [SerializeField] string _playerTag = "Player";
    [SerializeField] int _healingPercentage = 10;

    Health _playerHealth;
    int _healAmount;

    private void HealPlayer()
    {
        _playerHealth.Heal(_healAmount);
    }

    private void DestroyHealthDrop()
    {
        Destroy(gameObject);
    }

    protected override void CauseEffect(Collider playerCollision)
    {
        _playerHealth = playerCollision.GetComponentInParent<Health>();
        if (_playerHealth == null) return;
        _healAmount = _playerHealth.MaxValue / _healingPercentage;
        HealPlayer();
        DestroyHealthDrop();
    }

    public bool CanPickup(Inventory inventory)
    {
        throw new System.NotImplementedException();
        //return inventory.CanAdd(content.Item, content.Quantity);
    }
}
