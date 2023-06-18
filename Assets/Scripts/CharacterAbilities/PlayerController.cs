using System.Collections;
using UnityEngine;

namespace CharacterAbilities.Assets.Scripts.CharacterAbilities
{
    public class PlayerController : Controller
    {
        public override Vector2 GetMovementVector()
        {
            // Read input axes
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Create the input vector
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

            return inputVector;
        }
    }
}