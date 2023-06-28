using Assets.Scripts.CharacterAbilities;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Weapon
{
    public class RangeWeapon : Weapon
    {

        [SerializeField, BoxGroup("Ammo")] int _ammoPerClip;
        [SerializeField, BoxGroup("Ammo")] float _reloadTime;
        [SerializeField, BoxGroup("Ammo")] int _totalAmmo;
        [SerializeField, BoxGroup("Ammo"), ReadOnly] int _currentAmmoInClip;

        [SerializeField, BoxGroup("Accuracy")] Vector2 _accuracyRange;
        [SerializeField, BoxGroup("Accuracy")] AnimationCurve _accuracyCurve; // Curve to control the accuracy distribution

        [SerializeField] Recoil _recoil;
        [SerializeField, ReadOnly] List<ProjectileSpawner> _projectileSpawners = new List<ProjectileSpawner>();

        bool _canFire = true;

        public int TotalAmmo { get => _totalAmmo; set => _totalAmmo = value; }
        public int CurrentAmmoInClip { get => _currentAmmoInClip; set => _currentAmmoInClip = value; }

        #region Setup
        private void Awake()
        {
            _projectileSpawners = new List<ProjectileSpawner>(GetComponentsInChildren<ProjectileSpawner>());

            if (_controller == null)
            {
                _controller = GetComponentInParent<Controller>();
            }

            _controller.onFire += Fire;
            _controller.onReload += () => StartCoroutine(Reload());
        }

        private void Start()
        {
            CurrentAmmoInClip = _ammoPerClip;

        }

        public override void SetOwner(Controller controller)
        {
            base.SetOwner(controller);

            //Set Recoil target
            _recoil.RecoilTarget = controller.transform;
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
                StartCoroutine(Reload());
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
            _currentAmmoInClip--;
        }

        private IEnumerator StartFireCooldown()
        {


            _canFire = false;
            yield return new WaitForSeconds(1f / _fireRate);
            _canFire = true;
        }
        private IEnumerator Reload()
        {
            _canFire = false;
            yield return new WaitForSeconds(_reloadTime);

            int ammoToAddToClip = _ammoPerClip - _currentAmmoInClip;
            int ammoToDeductFromTotal = Mathf.Min(ammoToAddToClip, _totalAmmo);

            CurrentAmmoInClip += ammoToDeductFromTotal;
            _totalAmmo -= ammoToDeductFromTotal;

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