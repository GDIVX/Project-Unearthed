using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public class ProjectileFactory : IObjectFactory<Projectile>
    {
        Projectile _prefab;

        public ProjectileFactory(Projectile prefab)
        {
            _prefab = prefab ?? throw new System.ArgumentNullException(nameof(prefab));
        }

        public Projectile Create()
        {
            GameObject projectileObject = Object.Instantiate(_prefab.gameObject);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            
            return projectile;
        }
    }
}