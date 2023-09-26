using UnityEngine;

public class WeaponPickup : InstantEffectPickup
{
    [SerializeField] GameObject _weapon;

    protected override void CauseEffect(Collider playerCollision)
    {
        if(WeaponInventory.PlayerWeaponInventory.AddWeaponToList(_weapon))
            RemoveItemFromScene();
    }

    public override bool WillDrop()
    {
        throw new System.NotImplementedException();
    }
}
