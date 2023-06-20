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
        [SerializeField] float _recoildDuration;
        [SerializeField] AnimationCurve _recoilEaseCurve;
        [SerializeField] Controller _controller;

        private float _timer;
        private float _timerTotal;
        private Vector2 _startingForce;

        Transform _target;



        // Update is called once per frame
        void Update()
        {
            if (_timer <= 0f)
            {
                return;
            }

            _timer -= Time.deltaTime;

            float curveTimeValue = 1 - (_timer / _timerTotal);

            float curveEvul = _recoilEaseCurve.Evaluate(curveTimeValue);

            //shake over time
            Vector2 currRecoilForce = curveEvul * _startingForce;

            _target.position = (Vector2)_target.position + currRecoilForce * Time.deltaTime;

        }

        public void ApplyRecoil(Transform target)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }


            // Calculate the recoil direction based on the firing direction or player's aim direction
            Vector2 aimDirection = _controller.GetAimDirection();
            Vector2 recoilDirection = ((Vector2)target.position - aimDirection).normalized;

            Vector3 recoilForce = new Vector3(recoilDirection.x, recoilDirection.y, 0f) * _recoilStrength;
            _startingForce = recoilForce;


            // Apply recoil 
            _target = target;
            _timer = _recoildDuration;
            _timerTotal = _recoildDuration;
        }
    }
}