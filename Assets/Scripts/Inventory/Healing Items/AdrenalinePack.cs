using UnityEngine;

public class AdrenalinePack : MonoBehaviour, IHealingItem
{
    public int HealAmount { get; set; }

    public void ItemEffect(Health playerHealth)
    {
        if (playerHealth == null) return;
        playerHealth.AddTemporaryHealth(HealAmount);
    }
}
