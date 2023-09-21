using Assets.Scripts.InventorySystem;
using UnityEngine;

public class HealthDrop : InstantEffectPickup, IPickup
{
    [SerializeField] int _healingPercentage = 10;

    [SerializeField] Health _playerHealth;
    int _healAmount;

    private void HealPlayer()
    {
        _playerHealth.Heal(_healAmount);
    }

    private void DestroyHealthDrop()
    {
        Destroy(_gameObject);
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
    }

    public void OnGet()
    {
        throw new System.NotImplementedException();
    }

    public void OnReturn()
    {
        throw new System.NotImplementedException();
    }

    public override bool WillDrop()
    {
        float playerHealthNormalized = (float)_playerHealth.Value / (float)_playerHealth.MaxValue;
        if (playerHealthNormalized <= 0.2f) return true;
        if (_playerHealth.Value > _playerHealth.MaxValue * 0.6f) return false;
        float randomNumber = UnityEngine.Random.Range(0f, 1f);
        if (randomNumber <= playerHealthNormalized)
        {
            Debug.Log("Dropped item health drop by chance");
            return true;
        }
        Debug.Log("Didn't drop health drop by chance");
        return false;
    }
}
