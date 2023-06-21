using Assets.Scripts.CharacterAbilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public class Recoil : MonoBehaviour
    {
        [SerializeField] float _recoilStrength;
        [SerializeField] float _recoilDuration;
        [SerializeField] AnimationCurve _recoilEaseCurve;
        [SerializeField] Controller _controller;
        private float _timer;
        private float _timerTotal;
        private Transform _target;

        [SerializeField, ReadOnly] private Vector3 _startingForce;

        // Update is called once per frame
        void Update()
        {
            if (_timer <= 0f)
            {
                return;
            }

            _timer -= Time.deltaTime;

            float curveTimeValue = 1 - (_timer / _timerTotal);
            float curveEval = _recoilEaseCurve.Evaluate(curveTimeValue);

            // Shake over time
            Vector3 currRecoilForce = curveEval * _startingForce;
            _target.localPosition += currRecoilForce * Time.deltaTime;
        }

        public void ApplyRecoil(Transform target, Vector3 direction)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            ResetRecoil();

            // Calculate the recoil direction based on the firing direction or player's aim direction
            Vector3 recoilForce = direction * _recoilStrength;

            // Apply recoil
            _startingForce = recoilForce;
            _target = target;
            _timer = _recoilDuration;
            _timerTotal = _recoilDuration;

        }

        public void ResetRecoil()
        {
            _timer = 0f;
            _timerTotal = 0f;
            _startingForce = Vector3.zero;
        }

    }
}
