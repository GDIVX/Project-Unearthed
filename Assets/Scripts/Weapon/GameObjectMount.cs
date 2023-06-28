using Assets.Scripts.CharacterAbilities;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapon
{

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

        [SerializeField, ReadOnly] private GameObject _mountedObject;

        public Vector3 Offset
        {
            get => _offset;
            set
            {
                _offset = value;
            }
        }

        public GameObject MountedObject { get => _mountedObject; private set => _mountedObject = value; }
        public Controller Controller { get => _controller; protected set => _controller = value; }

        private void Awake()
        {
            if (_pivot == null)
            {
                _pivot = transform;
            }
        }

        private void Start()
        {
            if (MountedObject != null)
            {
                Mount(MountedObject);
            }
        }

        [Button]
        public virtual void Mount(GameObject other)
        {
            if (MountedObject != null)
                Unmount();

            other.transform.SetParent(_pivot);
            other.transform.localRotation = transform.localRotation;
            other.transform.localPosition = Offset;

            MountedObject = other;

        }

        public virtual GameObject Unmount()
        {
            GameObject discardedObject = MountedObject;
            MountedObject.transform.SetParent(null);

            MountedObject = null;
            return discardedObject;
        }

        private void Update()
        {

            // Perform weapon rotation based on input or target
            // You can implement your own logic here based on your game's requirements
            // For example, you might use input from the player's mouse or joystick to rotate the weapon

            Vector3 aimDirection = Controller.GetAimPoint();

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
}