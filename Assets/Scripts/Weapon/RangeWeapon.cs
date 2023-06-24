using Assets.Scripts.CharacterAbilities;
using Assets.Scripts.Weapon;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ProjectileSpawner))]
public class RangeWeapon : Weapon
{
    [SerializeField] Controller _controller;

    [SerializeField, BoxGroup("Ammo")] int _ammoPerClip;
    [SerializeField, BoxGroup("Ammo")] float _reloadTime;
    [SerializeField, BoxGroup("Ammo")] int _totalAmmo;
    [SerializeField, BoxGroup("Ammo"), ReadOnly] int _currentAmmoInClip;

    [SerializeField, BoxGroup("Accuracy"), Range(0, 1)] float _accuracy;
    [SerializeField, BoxGroup("Accuracy"), Min(1)] float _spread;

    [SerializeField] Recoil _recoil;

    private ProjectileSpawner _projectileSpawner;
    private bool _canFire = true;
    PerlineCurve perlineCurve;

    public int TotalAmmo { get => _totalAmmo; set => _totalAmmo = value; }
    public int CurrentAmmoInClip { get => _currentAmmoInClip; set => _currentAmmoInClip = value; }

    #region AWAKE_AND_START
    private void Awake()
    {
        _projectileSpawner = GetComponent<ProjectileSpawner>();

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
        perlineCurve = new PerlineCurve(1 - _accuracy, 0.1f, Random.value, Random.value);

    }
    #endregion
    public override void SetMount(WeaponMount mount)
    {
        _mount = mount;

    }

    #region FIRE_AND_RELOAD
    protected override void Fire()
    {
        if (!_canFire)
        {
            return;
        }

        if (CurrentAmmoInClip <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        //Generate offset base on accuracy
        Vector3 offset = Vector3.zero;
        offset.y = perlineCurve.GetNextValue() * _spread;

        _projectileSpawner.Spawn(offset);
        _currentAmmoInClip--;
        StartCoroutine(StartFireCooldown());

        //handle recoil
        _recoil.ApplyRecoil(_mount.transform, -transform.forward);

        //handle shake
        Shake();

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
        Vector2 direction = _controller.GetAimPoint();

        //start the shake
        CameraShake.Instance.Shake(_shakeTime, _shakeIntensity, _shakeCurve, direction);
    }



}
