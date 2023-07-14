using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.CharacterAbilities
{
    public class Movement : MonoBehaviour
    {

        [SerializeField] float movementSpeed = 5f;                 // Adjust the movement speed as per your needs
        [SerializeField, Min(0.1f)] float accelerationScalar = 1f;            // Adjust the acceleration scalar as per your needs
        [SerializeField, Min(0.1f)] float decelerationScalar = 1f;            // Adjust the deceleration scalar as per your needs
        [SerializeField] AnimationCurve accelerationCurve;         // Animation curve for acceleration
        [SerializeField] AnimationCurve decelerationCurve;         // Animation curve for deceleration
        [SerializeField] float maxVelocity = 10f;                  // Adjust the maximum velocity as per your needs

        [SerializeField, ReadOnly] float currentSpeed = 0f;
        [SerializeField, ReadOnly] float accelerationTimer = 0f;
        [SerializeField, ReadOnly] float decelerationTimer = 0f;

        [Inject] IController controller;
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public float AccelerationScalar { get => accelerationScalar; set => accelerationScalar = value; }
        public float DecelerationScalar { get => decelerationScalar; set => decelerationScalar = value; }
        public AnimationCurve AccelerationCurve { get => accelerationCurve; set => accelerationCurve = value; }
        public AnimationCurve DecelerationCurve { get => decelerationCurve; set => decelerationCurve = value; }
        public float MaxVelocity { get => maxVelocity; set => maxVelocity = value; }
        public float CurrentSpeed { get => currentSpeed; private set => currentSpeed = value; }
        public IController Controller { get => controller; set => controller = value; }

        private void Update()
        {
            //if there is no controller throw error and return
            if (Controller == null)
            {
                Debug.LogError("No controller assigned to movement");
                return;
            }

            //Read movement input from controller
            Vector2 inputVector = Controller.GetMovementVector();

            // Check if input vector magnitude is greater than 1 and normalize it
            if (inputVector.magnitude > 1f)
            {
                inputVector.Normalize();
            }

            // Calculate the target speed based on input
            float targetSpeed = inputVector.magnitude * MovementSpeed;

            // Update the acceleration and deceleration timers based on input
            if (targetSpeed > 0f)
            {
                accelerationTimer += Time.deltaTime;
                decelerationTimer = 0f;
            }
            else
            {
                decelerationTimer += Time.deltaTime;
                accelerationTimer = 0f;
            }

            // Calculate the acceleration and deceleration values based on the animation curves and scalars
            float accelerationValue = AccelerationCurve.Evaluate(accelerationTimer) * AccelerationScalar;
            float decelerationValue = DecelerationCurve.Evaluate(decelerationTimer) * DecelerationScalar;

            // Smoothly adjust the current speed towards the target speed
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, targetSpeed, accelerationValue * Time.deltaTime / (accelerationValue + decelerationValue));

            // Limit the current speed to the maximum velocity
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0f, MaxVelocity);

            if (CurrentSpeed == float.NaN)
            {
                Debug.LogWarning("Current speed is Nan");
                return;
            }

            // Calculate the movement vector
            Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y) * CurrentSpeed;

            // Apply the movement to the player's position
            transform.localPosition += (Vector3)movement * Time.deltaTime;
        }
    }
}