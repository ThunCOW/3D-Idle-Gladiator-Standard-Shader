﻿using System;
using UnityEngine;

[Serializable]
public class FlexibleDerivativeStat : DerivativeStat
{
    internal event Action CurrentValueChanged;

    [Space]
    [SerializeField] protected float _currentValue;
    public float CurrentValue
    {
        get { return _currentValue; }
        set
        {
            // if new value is in between 0 and calculated value
            if (value <= GetValue() && value > 0)
            {
                _currentValue = value;
            }
            // for example trying to overheal
            else if (value > GetValue())
            {
                _currentValue = GetValue();
            }
            // for example dead
            else if (value <= 0)
            {
                _currentValue = 0;
            }

            CurrentValueChanged();
        }
    }
}