using Assets.Scripts.Stats;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Unearthed
{
    public class HUDBars : Stat
    {
        [SerializeField] Image _bar;
        [SerializeField] float _lerpSpeed = 3f;

        private float _currentFillAmount;

        private void Start()
        {
            _currentFillAmount = _bar.fillAmount;
        }
        void ChangeBarImage()
        {
            float targetFillAmount = (float)Value / (float)MaxValue;
            DOTween.To(() => _currentFillAmount, x => _currentFillAmount = x, targetFillAmount, _lerpSpeed)
                .OnUpdate(() => _bar.fillAmount = _currentFillAmount);
        }

        public virtual void ChangeValue(Stat playerStat)
        {
            Value = playerStat.Value;
            ChangeBarImage();
        }
    }
}
