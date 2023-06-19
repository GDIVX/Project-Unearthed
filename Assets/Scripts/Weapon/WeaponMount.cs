using Assets.Scripts.CharacterAbilities;
using UnityEngine;

public class WeaponMount : MonoBehaviour
{
    [SerializeField] private Transform _weaponPivot;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Controller _controller;
    [SerializeField] private AnimationCurve _rotationSpeedCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
    [SerializeField] private float _speedScalar = 1f;

    public Vector2 Offset
    {
        get => _offset;
        set
        {
            _offset = value;
            MountWeapon(Weapon);
        }
    }

    public Weapon Weapon
    {
        get => _weapon;
        set
        {
            MountWeapon(value);
        }
    }

    private void Awake()
    {
        if (_weaponPivot == null)
        {
            _weaponPivot = transform;
        }

        if (Weapon == null)
        {
            Weapon = GetComponent<Weapon>();
        }
    }

    private void Start()
    {
        if (Weapon != null)
        {
            MountWeapon(Weapon);
        }
    }

    public void MountWeapon(Weapon weapon)
    {
        _weapon = weapon;
        weapon.transform.parent = _weaponPivot;
        weapon.transform.localPosition = Offset;
        weapon.transform.localRotation = _weaponPivot.rotation;
    }

    private void Update()
    {
        // Perform weapon rotation based on input or target
        // You can implement your own logic here based on your game's requirements
        // For example, you might use input from the player's mouse or joystick to rotate the weapon

        Vector2 aimDirection = _controller.GetAimDirection();
        Vector2 direction = aimDirection - (Vector2)_weaponPivot.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        // Get the rotation speed based on the animation curve and multiply it by the speed scalar
        float rotationSpeed = _rotationSpeedCurve.Evaluate(Time.time) * _speedScalar;

        // Rotate towards the target rotation with the specified rotation speed
        _weaponPivot.rotation = Quaternion.Lerp(_weaponPivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
