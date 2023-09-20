using UnityEngine;

public class HealthPack : MonoBehaviour, IHealingItem
{
    public int HealAmount { get; set; }

    public void ItemEffect(Health playerHealth)
    {
        if (playerHealth == null) return;
        StartCoroutine(playerHealth.Regenerate(HealAmount));
    }
}
