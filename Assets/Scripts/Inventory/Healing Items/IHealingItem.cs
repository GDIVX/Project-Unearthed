public interface IHealingItem 
{
    int HealAmount { get; set; }

    void ItemEffect(Health playerHealth);
}
