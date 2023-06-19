using System.Collections;
using UnityEngine;

namespace CharacterAbilities.Assets.Scripts.CharacterAbilities
{
    public class PlayerController : Controller
    {
        private bool isFiring;
        public override Vector2 GetMovementVector()
        {
            // Read input axes
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Create the input vector
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

            return inputVector;
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
    }
}