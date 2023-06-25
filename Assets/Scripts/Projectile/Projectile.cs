using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] float _startingVelocity = 0f; // Initial speed of the projectile
        [SerializeField] float _maxVelocity = 10f; // Maximum velocity of the projectile
        [SerializeField] AnimationCurve _accelerationCurve; // AnimationCurve to control acceleration over time
        [SerializeField] float _accelerationScalar = 1f; // Scalar to control the rate of acceleration
        [SerializeField] float _lifetime = 5f; // Lifetime of the projectile in seconds

        private Rigidbody _rigidbody;

        private float currentSpeed; // Current speed of the projectile
        private float currentAcceleration; // Current acceleration value

        ProjectileSpawner spawner;

        public GameObject GameObject => gameObject;

        public ProjectileSpawner Spawner
        {
            get => spawner;
            set
            {
                spawner = value;
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {

            //Start counting to despawn
            StartCoroutine(DespawnTimer());
        }

        public void OnGet()
        {
            currentSpeed = _startingVelocity;
            currentAcceleration = 0f;

        }


        public void OnReturn()
        {
            // Reset the projectile's properties
            currentSpeed = 0f;
            currentAcceleration = 0f;
            _rigidbody.velocity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            // Calculate the new acceleration based on the animation curve and scalar
            float time = Mathf.Clamp01(Time.time / _startingVelocity);
            currentAcceleration = _accelerationCurve.Evaluate(time) * _accelerationScalar;

            // Calculate the new speed based on the acceleration
            currentSpeed += currentAcceleration * Time.fixedDeltaTime;

            // Limit the speed to the maximum velocity
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, _maxVelocity);

            // Apply the velocity to the Rigidbody2D component
            _rigidbody.velocity = transform.up * currentSpeed;


        }

        public void Despawn()
        {
            if (spawner == null)
            {
                Debug.LogError("No spawner assigned to projectile");
                //Destroy the object
                Destroy(gameObject);
                return;
            }
            // Return the projectile to the pool

            Spawner.Return(this);
        }
        private IEnumerator DespawnTimer()
        {
            yield return new WaitForSeconds(_lifetime);

            Despawn();
        }
    }
}
