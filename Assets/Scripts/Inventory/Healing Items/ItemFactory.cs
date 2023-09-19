using ObjectPooling;
using UnityEngine;

public class ItemFactory : IObjectFactory<InstantEffectPickup>
{
    InstantEffectPickup _prefab;

    public ItemFactory(InstantEffectPickup prefab)
    {
        _prefab = prefab ?? throw new System.ArgumentNullException(nameof(prefab));
    }

    public InstantEffectPickup Create()
    {
        GameObject projectileObject = Object.Instantiate(_prefab.gameObject);
        InstantEffectPickup item = projectileObject.GetComponent<HealthDrop>();
        return item;
    }
}
