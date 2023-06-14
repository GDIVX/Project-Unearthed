using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Projectile
{
    public class ProjectileCollisionHandler : DamageOnCollision
    {
        [SerializeField] Projectile projectile;
        [SerializeField] string[] collisionTags;

        public UnityEvent<Projectile> OnProjectileHit;

        public override void OnCollision(Collider2D other)
        {
            //can we collide with this object?
            if (collisionTags.Length == 0)
            {
                return;
            }
            foreach (string tag in collisionTags)
            {
                if (other.CompareTag(tag))
                {
                    //Invoke any events that are subscribed to OnProjectileHit
                    OnProjectileHit?.Invoke(projectile);

                    //Deal damage to the object we hit if possible
                    base.OnCollision(other);

                    //Return the projectile to the pool
                    projectile.Despawn();

                    break;
                }
            }




        }
    }
}