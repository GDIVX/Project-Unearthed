using Assets.Scripts.Projectile;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The Projectile Spawner component.
/// </summary>
public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] Projectile _prefab;
    [SerializeField] int _initialAmmount = 10;


    SpawnManager<Projectile> projectileSpawnManager;

    private void Awake()
    {
        projectileSpawnManager = new SpawnManager<Projectile>(new ProjectileFactory(_prefab), _initialAmmount);
    }

    /// <summary>
    /// Returns a Projectile object from the pool.
    /// </summary>
    /// <returns>A projectile object.</returns>
    public Projectile Get()
    {
        return projectileSpawnManager.Get();
    }

    /// <summary>
    /// Returns a Projectile object to the pool.
    /// </summary>
    /// <param name="projectile">The Projectile object to return.</param>
    public void Return(Projectile projectile)
    {
        projectileSpawnManager.Return(projectile);
    }
}
