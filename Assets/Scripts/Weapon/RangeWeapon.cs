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

    [SerializeField] Recoil _recoil;

    private ProjectileSpawner _projectileSpawner;
    private bool _canFire = true;
    PerlineCurve perlineCurve;

    public int TotalAmmo { get => _totalAmmo; set => _totalAmmo = value; }
    public int CurrentAmmoInClip { get => _currentAmmoInClip; set => _currentAmmoInClip = value; }

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
        perlineCurve = new PerlineCurve(_shakeIntensity, 0.1f, Random.value, Random.value);

    }
    public override void SetMount(WeaponMount mount)
    {
        _mount = mount;

    }
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



        _projectileSpawner.Spawn();
        _currentAmmoInClip--;
        StartCoroutine(StartFireCooldown());

        //handle recoil
        _recoil.ApplyRecoil(_mount.transform);

        //handle shake
        Shake();

    }




    private void Shake()
    {
        //get the direction of the outgoing projectile based on the rotation of the weapon
        Vector2 direction = _controller.GetAimDirection();

        //start the shake
        CameraShake.Instance.Shake(_shakeTime, _shakeIntensity, _shakeCurve, direction);
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

}
