using Assets.Scripts.Projectile;
using ObjectPooling;
using Sirenix.OdinInspector;
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
    [SerializeField] Transform _spawnPoint;


    SpawnManager<Projectile> projectileSpawnManager;

    private void Awake()
    {
        projectileSpawnManager = new SpawnManager<Projectile>(new ProjectileFactory(_prefab), _initialAmmount);
    }

    [Button]
    public Projectile Spawn()
    {
        Projectile projectile = projectileSpawnManager.Get();

        //set up the transform of the projectile
        projectile.transform.position = _spawnPoint.position;
        projectile.transform.rotation = _spawnPoint.rotation;

        projectile.Spawner = this;

        return projectile;
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