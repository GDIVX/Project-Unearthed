using Assets.Scripts.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Unearthed
{
    public class HUDBars : Stat
    {
        [SerializeField] Image _bar;
        [SerializeField] float _lerpSpeed = 3f;
        private float _currentFillAmount;

        private void Start()
        {
            _bar.fillAmount = (float)Value / (float)MaxValue;
        }
        void ChangeBarImage()
        {
            //_bar.fillAmount = Mathf.Lerp(_bar.fillAmount, Value / MaxValue, _lerpSpeed);

            float targetFillAmount = (float)Value / (float)MaxValue;
            _bar.fillAmount = targetFillAmount;
            //DOTween.To(() => _bar.fillAmount, x => _bar.fillAmount = x, targetFillAmount, _lerpSpeed)
            //    .SetUpdate(true);
            Debug.Log(_bar.fillAmount);
        }

        public virtual void ChangeValue(Stat playerStat)
        {
            Debug.Log("Change Value");
            Value = playerStat.Value;
            OnValueChange();
        }

        public override void OnValueChange()
        {
            Debug.Log("Change event");
            ChangeBarImage();
            base.OnValueChange();
        }
    }
}
