using Assets.Scripts.InventorySystem;

public interface IAmmoTracker
{
    int GetAmmoCount(Item ammoType);
    void AddAmmo(int amount, Item type);
}
