using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stat : MonoBehaviour
{
    [SerializeField] int _value;
    [SerializeField] int _maxValue;

    public UnityEvent<Stat> OnValueChangeEvent;

    public int Value
    {
        get => _value;
        set
        {
            this._value = Mathf.Clamp(value, 0, _maxValue);
            OnValueChange();
        }
    }

    public virtual void OnValueChange()
    {
        OnValueChangeEvent?.Invoke(this);
    }

    public int MaxValue { get => _maxValue; set => _maxValue = value; }
}
