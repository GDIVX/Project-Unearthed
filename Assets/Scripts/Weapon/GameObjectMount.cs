using Assets.Scripts.CharacterAbilities;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

/// <summary>
/// This class is used to mount a GameObject to a pivot point and rotate it around a target.
/// </summary>
public class GameObjectMount : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Controller _controller;
    [SerializeField] private AnimationCurve _rotationSpeedCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
    [SerializeField] private float _speedScalar = 1f;

    [SerializeField] private GameObject _mountedObject;

    public Vector3 Offset
    {
        get => _offset;
        set
        {
            _offset = value;
        }
    }


    private void Awake()
    {
        if (_pivot == null)
        {
            _pivot = transform;
        }
    }

    private void Start()
    {
        if (_mountedObject != null)
        {
            Mount(_mountedObject);
        }
    }

    [Button]
    public void Mount(GameObject other)
    {
        if (_mountedObject != null)
            Unmount(_mountedObject);

        _mountedObject = other;
        other.transform.SetParent(_pivot);
        other.transform.localPosition = Offset;

    }

    private void Unmount(GameObject other)
    {
        _mountedObject = null;
        other.transform.SetParent(null);
    }

    private void Update()
    {

        // Perform weapon rotation based on input or target
        // You can implement your own logic here based on your game's requirements
        // For example, you might use input from the player's mouse or joystick to rotate the weapon

        Vector3 aimDirection = _controller.GetAimPoint();

        if (aimDirection.sqrMagnitude <= 0.001f)
        {
            return;
        }

        Vector3 direction = aimDirection.normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Get the rotation speed based on the animation curve and multiply it by the speed scalar
        float rotationSpeed = _rotationSpeedCurve.Evaluate(Time.time) * _speedScalar;

        // Rotate towards the target rotation with the specified rotation speed
        _pivot.rotation = Quaternion.Lerp(_pivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
