using Assets.Scripts.CharacterAbilities;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Weapon
{
    public class RangeWeapon : Weapon
    {

        [SerializeField, BoxGroup("Ammo")] float _reloadTime;
        [SerializeField, BoxGroup("Ammo")] int _ammoPerClip;
        [SerializeField, BoxGroup("Ammo"), ReadOnly] int _currentAmmoInClip;

        [SerializeField, BoxGroup("Accuracy")] Vector2 _accuracyRange;
        [SerializeField, BoxGroup("Accuracy")] AnimationCurve _accuracyCurve; // Curve to control the accuracy distribution

        [SerializeField] Recoil _recoil;
        [SerializeField, ReadOnly] List<ProjectileSpawner> _projectileSpawners = new List<ProjectileSpawner>();

        bool _canFire = true;
        IAmmoTracker _ammoTracker;

        public int CurrentAmmoInClip { get => _currentAmmoInClip; private set => _currentAmmoInClip = value; }


        #region Setup
        private void Awake()
        {
            _projectileSpawners = new List<ProjectileSpawner>(GetComponentsInChildren<ProjectileSpawner>());

        }

        private void Start()
        {
            //Fast reload
            StartCoroutine(Reload(0));
        }

        public override void SetOwner(Controller controller)
        {
            base.SetOwner(controller);

            _controller.onFire += Fire;
            _controller.onReload += () => StartCoroutine(Reload(_reloadTime));

            //Set Recoil target
            _recoil.RecoilTarget = controller.transform;

            // Set Ammo Tracker
            if (_ammoTracker == null)
            {
                _ammoTracker = controller.GetComponentInChildren<IAmmoTracker>();
            }
        }

        #endregion

        #region FIRE_AND_RELOAD
        protected override void Fire()
        {
            //Ensure we are not calling this method while the object is not active
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            if (!_canFire)
            {
                return;
            }

            if (CurrentAmmoInClip <= 0)
            {
                StartCoroutine(Reload(_reloadTime));
                return;
            }

            foreach (ProjectileSpawner spawner in _projectileSpawners)
            {
                FireProjectiles(spawner);
            }
            StartCoroutine(StartFireCooldown());

            //handle recoil
            _recoil.ApplyRecoil(transform.forward);

            //handle shake
            Shake();

        }

        private void FireProjectiles(ProjectileSpawner spawner)
        {
            if (spawner is null)
            {
                throw new ArgumentNullException(nameof(spawner));
            }
            // Do not spawn a projectile if the clip is empty
            if (CurrentAmmoInClip <= 0)
            {
                return;
            }

            // Generate a random accuracy value within the specified range based on the accuracy curve
            float randomValue = Random.value;
            float accuracy = Mathf.Lerp(_accuracyRange.x, _accuracyRange.y, _accuracyCurve.Evaluate(randomValue));

            // Generate offset based on the accuracy
            Vector3 offset = Vector3.zero;
            offset.y = Random.Range(-accuracy, accuracy);

            spawner.Spawn(offset);
            CurrentAmmoInClip--;
        }

        private IEnumerator StartFireCooldown()
        {


            _canFire = false;
            yield return new WaitForSeconds(1f / _fireRate);
            _canFire = true;
        }
        private IEnumerator Reload(float time)
        {
            _canFire = false;
            yield return new WaitForSeconds(time);

            int ammoToAddToClip = _ammoPerClip - CurrentAmmoInClip;
            int ammoToDeductFromTotal = Mathf.Min(ammoToAddToClip, _ammoTracker.GetAmmoCount());

            CurrentAmmoInClip += ammoToDeductFromTotal;
            _ammoTracker.AddAmmo(-ammoToDeductFromTotal);

            _canFire = true;
        }
        #endregion


        private void Shake()
        {
            //get the direction of the outgoing projectile based on the rotation of the weapon
            Vector3 direction = _controller.GetAimPoint();

            //start the shake
            CameraShake.Instance.Shake(_shakeTime, _shakeIntensity, _shakeCurve, direction);
        }



    }
}