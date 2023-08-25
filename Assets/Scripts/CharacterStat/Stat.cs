using System;
using UnityEngine;

[Serializable]
public class Stat
{
    // This is a root stat, if this stat changes other stats will be notified with an event
    internal event Action AttributeChanged;

    // Default value before any change
    [SerializeField]
    protected float baseValue;

    /// <summary>
    /// Returns base value
    /// </summary>
    /// <returns></returns>
    public virtual float GetValue()
    {
        return baseValue;
    }

    public void ChangeBaseStat(int amount)
    {
        if (amount == 0)
            return;

        baseValue += amount;

        if (AttributeChanged != null)
        {
            AttributeChanged();
        }
    }
}