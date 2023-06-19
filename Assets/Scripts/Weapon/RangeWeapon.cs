using Assets.Scripts.CharacterAbilities;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileSpawner))]
public class RangeWeapon : Weapon
{
    [SerializeField] Controller _controller;
    [SerializeField] float _fireRate = 2f; // Fire rate in attacks per second

    [SerializeField, BoxGroup("Ammo")] int _ammoPerClip;
    [SerializeField, BoxGroup("Ammo")] float _reloadTime;
    [SerializeField, BoxGroup("Ammo")] int _totalAmmo;
    [SerializeField, BoxGroup("Ammo"), ReadOnly] int _currentAmmoInClip;


    private ProjectileSpawner _projectileSpawner;
    private bool _canFire = true;

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
