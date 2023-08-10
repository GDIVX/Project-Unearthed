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


        [ShowInInspector] bool isDodging = false;
        [ShowInInspector] float lastDodgeTime = 0f;

        public bool CanDodge => !isDodging && Time.time - lastDodgeTime > dodgeCooldown;


        private Vector3 dodgeDirection;

        private void Update()
        {
            if (!isDodging)
            {
                return;
            }
            transform.position += dodgeDirection * dodgeSpeed * Time.deltaTime;
            if (Time.time - lastDodgeTime >= dodgeDuration)
            {
                EndDodge();
            }
        }

        public void Dodge(Vector3 direction)
        {
            if (!CanDodge) return;

            //if the direction is not on the xy plane, convert it
            if (direction.y != 0)
            {
                direction = new Vector3(direction.x, 0, direction.y);
            }

            isDodging = true;
            lastDodgeTime = Time.time;
            dodgeDirection = direction.normalized;

            health.SetInvisibilityForSeconds(invisibilityDuration);
            OnDodgeStart?.Invoke();
        }

        private void EndDodge()
        {
            OnDodgeEnd?.Invoke();
            isDodging = false;
        }





    }
}
