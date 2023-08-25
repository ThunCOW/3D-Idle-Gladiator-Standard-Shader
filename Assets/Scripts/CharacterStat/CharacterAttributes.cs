using System.Collections.Generic;
using UnityEngine;

public enum PrimaryStats
{
    Strength,
    Vitality,
    Speed,
    Attack,
    Defense,
    Resolve,
}

public class CharacterAttributes : MonoBehaviour
{
    public int Level;

    [Header("********** Primary Stats **********")]
    public Stat Strength;                    // Increase Weight Cap
    public Stat Vitality;                    // Increase Health Point
    public Stat Speed;                       // Increase Initiative and Critical Chance
    public Stat Attack;
    public Stat Defense;
    public Stat Resolve;

    [Header("*********** Derivative ***********")]
    public DerivativeStat AttackSpeed;
    public FlexibleDerivativeStat Weight;

    [Header("*********** Bonuses **************")]
    public float HitBonus;

    public Dictionary<PrimaryStats, Stat> PrimaryStatsDict;

    private void Start()
    {
        PrimaryStatsDict = new Dictionary<PrimaryStats, Stat>();
        PrimaryStatsDict.Add(PrimaryStats.Strength, Strength);
        PrimaryStatsDict.Add(PrimaryStats.Vitality, Vitality);
        PrimaryStatsDict.Add(PrimaryStats.Speed, Speed);
        PrimaryStatsDict.Add(PrimaryStats.Attack, Attack);
        PrimaryStatsDict.Add(PrimaryStats.Defense, Defense);
        PrimaryStatsDict.Add(PrimaryStats.Resolve, Resolve);
    }
}