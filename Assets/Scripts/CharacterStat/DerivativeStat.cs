using System;
using UnityEngine;


[Serializable]
public class DerivativeStat : Stat
{
    //[SerializeField] private float multiplier = 1;  // multiplier for dependancy, 

    //public Stat StatToDependOn;    // class this stat dependant on

    [SerializeField]
    protected float DerivatedValue;

    [HideInInspector] public CharacterManager CharacterManager;

    public Derivate DerivateStat;

    public void DerivateFrom(Stat DerivateFrom)
    {
        DerivateStat.DerivateFrom = DerivateFrom;
        DerivateStat.DerivateFrom.AttributeChanged += DependantStatChanged;
        DependantStatChanged();
    }

    // Returns derivated value of base stat ( Max Health Point is calculated based on base health value + Vitality * multiplier value which would be called derivated value )
    /// <summary>
    /// Returns derivated value of base stat
    /// </summary>
    /// <returns></returns>
    public override float GetValue()
    {
        DerivatedValue = DerivateStat.DerivateFrom == null ? baseValue : baseValue + DerivateStat.DerivateFrom.GetValue() * DerivateStat.multiplier;
        return DerivatedValue;
    }

    private void DependantStatChanged()
    {
        DerivatedValue = DerivateStat.DerivateFrom == null ? baseValue : baseValue + DerivateStat.DerivateFrom.GetValue() * DerivateStat.multiplier;
    }

    [System.Serializable]
    public class Derivate
    {
        [NonSerialized] public Stat DerivateFrom;
        public float multiplier;
    }
}