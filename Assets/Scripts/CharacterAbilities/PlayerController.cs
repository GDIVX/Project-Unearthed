using System.Collections;
using UnityEngine;

namespace Assets.Scripts.CharacterAbilities
{
    public class PlayerController : Controller
    {
        [SerializeField] LayerMask _mousePositionLayerMask;
        private bool isFiring;
        public override Vector2 GetMovementVector()
        {
            // Read input axes
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Create the input vector
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

            // Define the rotation angle for isometric perspective (45 degrees)
            float rotationAngle = 45f;

            // Convert the rotation angle to radians
            float rotationAngleRad = rotationAngle * Mathf.Deg2Rad;

            // Create a rotation matrix using the rotation angle
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, 0f, -rotationAngle));

            // Rotate the input vector using the rotation matrix
            Vector2 rotatedInputVector = rotationMatrix * inputVector;

            return rotatedInputVector;
        }



        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isFiring = true;
                StartCoroutine(FireCoroutine());
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                isFiring = false;
            }

            if (Input.GetButtonDown("Reload"))
            {
                onReload?.Invoke();
            }
        }

        private IEnumerator FireCoroutine()
        {
            while (isFiring)
            {
                onFire?.Invoke();
                yield return null;
            }
        }

        public override Vector3 GetAimPoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, _mousePositionLayerMask))
            {

                Vector3 aimDirection = hit.point - transform.position;
                aimDirection.y = 0f; // Optional: Set the y-coordinate to 0 if you want to ignore height differences
                aimDirection.Normalize();
                return aimDirection;
            }

            return Vector3.zero; // Default aim direction if the raycast doesn't hit anything
        }


    }
}