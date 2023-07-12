public interface IAmmoTracker
{
    int GetAmmoCount(AmmoType type);
    void AddAmmo(int amount, AmmoType type);
}


public enum AmmoType
{
    All,
    Pistol,
    Shotgun,
    Rifle,
    Rocket,
}