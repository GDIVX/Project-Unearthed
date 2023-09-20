using ObjectPooling;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] InstantEffectPickup _prefab;
    [SerializeField] int _initialAmmount = 10;
    [SerializeField] Transform _spawnPoint;

    SpawnManager<InstantEffectPickup> _objectSpawnManager;

    private void Awake()
    {
        _objectSpawnManager = new SpawnManager<InstantEffectPickup>(new ItemFactory(_prefab), _initialAmmount);
    }

    public void Spawn()
    {
        InstantEffectPickup objectToSpawn = _objectSpawnManager.Get();

        //set up the transform of the projectile
        objectToSpawn.transform.position = _spawnPoint.position;

        objectToSpawn.Spawner = this;
    }

    /// <summary>
    /// Returns an object to the pool.
    /// </summary>
    /// <param name="item">The Projectile object to return.</param>
    public void Return(InstantEffectPickup item)
    {
        _objectSpawnManager.Return(item);
    }
}
