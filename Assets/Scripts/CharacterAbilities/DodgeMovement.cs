using CharacterAbilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.CharacterAbilities
{
    public class DodgeMovement : MonoBehaviour
    {
        [SerializeField, BoxGroup("Movement")] float dodgeSpeed = 10f;       // Speed of the dodge
        [SerializeField, BoxGroup("Movement")] float dodgeDuration = 0.5f;  // Duration of the dodge
        [SerializeField, BoxGroup("Movement")] float dodgeCooldown = 2f;    // Cooldown time before the next dodge can be initiated

        [SerializeField, BoxGroup("Invisibility Frames")] Health health;
        [SerializeField, BoxGroup("Invisibility Frames")] float invisibilityDuration;

        public event Action OnDodgeStart, OnDodgeEnd;


        private bool isDodging = false;
        private float lastDodgeTime = 0f;

        public bool CanDodge => !isDodging && Time.time - lastDodgeTime > dodgeCooldown;


        public void Dodge(Vector3 direction)
        {
            if (!CanDodge) return;

            isDodging = true;
            lastDodgeTime = Time.time;

            // You may want to normalize the direction and multiply by dodgeSpeed
            Vector3 dodgeVector = direction.normalized * dodgeSpeed;

            //set invisibility frames
            health.SetInvisibilityForSeconds(invisibilityDuration);

            // You can initiate a Coroutine to handle the dodge movement over time
            StartCoroutine(DodgeCoroutine(dodgeVector));
        }

        private IEnumerator DodgeCoroutine(Vector3 dodgeVector)
        {
            float startTime = Time.time;
            OnDodgeStart?.Invoke();

            while (Time.time - startTime < dodgeDuration)
            {
                // Move the character in the dodge direction
                transform.position += dodgeVector * Time.deltaTime;
                yield return null;
            }

            OnDodgeEnd?.Invoke();
            isDodging = false; // End the dodge
        }
    }
}
