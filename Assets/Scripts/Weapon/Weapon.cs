using Assets.Scripts.CharacterAbilities;
using CharacterAbilities.Assets.Scripts.CharacterAbilities;
using Cinemachine;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected IProjectileWeaponInput _controller;

        [SerializeField, BoxGroup("Camera Shake")] protected AnimationCurve _shakeCurve;
        [SerializeField, BoxGroup("Camera Shake")] protected float _shakeIntensity = 0.2f;
        [SerializeField, BoxGroup("Camera Shake")] protected float _shakeTime = 0.2f;
        [SerializeField, BoxGroup("Fire Rate")] protected float _fireRate = 2f; // Fire rate in attacks per second



        protected abstract void Fire();

        public virtual void SetOwner(IProjectileWeaponInput controller)
        {
            _controller = controller;
        }
    }
}