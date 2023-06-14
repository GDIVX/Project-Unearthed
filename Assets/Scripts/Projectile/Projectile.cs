using ObjectPooling;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] float _startingVelocity = 0f; // Initial speed of the projectile
        [SerializeField] float _maxVelocity = 10f; // Maximum velocity of the projectile
        [SerializeField] AnimationCurve _accelerationCurve; // AnimationCurve to control acceleration over time
        [SerializeField] float _accelerationScalar = 1f; // Scalar to control the rate of acceleration
        [SerializeField] float _lifetime = 5f; // Lifetime of the projectile in seconds

        private Rigidbody2D _rigidbody;

        private float currentSpeed; // Current speed of the projectile
        private float currentAcceleration; // Current acceleration value
        private float despawnTimer; // Timer to track the projectile's lifetime

        ProjectileSpawner spawner;

        public GameObject GameObject => gameObject;

        public ProjectileSpawner Spawner { get => spawner; set => spawner = value; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void OnGet()
        {
            currentSpeed = _startingVelocity;
            currentAcceleration = 0f;
            despawnTimer = 0f;
        }

        public void OnReturn()
        {
            // Reset the projectile's properties
            currentSpeed = 0f;
            currentAcceleration = 0f;
            despawnTimer = 0f;
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

            // Increment the despawn timer
            despawnTimer += Time.fixedDeltaTime;

            // Check if the despawn timer has exceeded the lifetime
            if (despawnTimer >= _lifetime)
            {
                // Despawn the projectile
                Despawn();
            }
        }

        public void Despawn()
        {
            // Return the projectile to the pool
            Spawner.Return(this);
        }
    }
}
