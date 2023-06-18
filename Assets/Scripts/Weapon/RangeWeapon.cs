using CharacterAbilities.Assets.Scripts.CharacterAbilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileSpawner))]
public class RangeWeapon : Weapon
{
    [SerializeField] private Controller _controller;
    [SerializeField] private float fireRate = 2f; // Fire rate in attacks per second

    private ProjectileSpawner _projectileSpawner;
    private bool _canFire = true;
    private void Awake()
    {
        _projectileSpawner = GetComponent<ProjectileSpawner>();

        if (_controller == null)
        {
            _controller = GetComponentInParent<Controller>();
        }

        _controller.onFire += Fire;
    }
    protected override void Fire()
    {
        if (_canFire)
        {
            _projectileSpawner.Spawn();
            StartCoroutine(StartFireCooldown());
        }
    }

    private IEnumerator StartFireCooldown()
    {
        _canFire = false;
        yield return new WaitForSeconds(1f / fireRate);
        _canFire = true;
    }

}
