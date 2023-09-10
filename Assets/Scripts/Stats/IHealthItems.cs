using UnityEngine;

public interface IHealthItems 
{
    public Health PlayerHealth { get; set; }
    public int HealAmount { get; set; }
}
