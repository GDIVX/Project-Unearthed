using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Stats
{
    public class Stat : MonoBehaviour
    {
        [SerializeField, Min(0)] int _value;
        [SerializeField, Min(1)] int _maxValue;

        public UnityEvent<Stat> OnValueChangeEvent;

        public int Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp(value, 0, _maxValue);
                OnValueChange();
            }
        }

        public virtual void OnValueChange()
        {
            OnValueChangeEvent?.Invoke(this);
        }

        public int MaxValue { get => _maxValue; set => _maxValue = value; }
    }
}