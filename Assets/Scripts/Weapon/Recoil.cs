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
        [SerializeField] Transform _recoilTarget;
        private float _timer;
        private float _timerTotal;

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
            _recoilTarget.localPosition += currRecoilForce * Time.deltaTime;
        }

        public void ApplyRecoil(Vector3 direction)
        {


            ResetRecoil();

            //project the direction to the xz plane
            direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;

            // Calculate the recoil direction based on the firing direction or player's aim direction
            Vector3 recoilForce = -direction * _recoilStrength;

            // Apply recoil
            _startingForce = recoilForce;
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
