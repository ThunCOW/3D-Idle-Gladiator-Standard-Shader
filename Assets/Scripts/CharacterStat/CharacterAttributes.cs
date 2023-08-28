using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PrimaryAttributes
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
    [Header("*********** LVL **********")]
    public CharacterLeveL LVL;

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

    public Dictionary<PrimaryAttributes, Stat> PrimaryStatsDict;

    private void Start()
    {
        PrimaryStatsDict = new Dictionary<PrimaryAttributes, Stat>();
        PrimaryStatsDict.Add(PrimaryAttributes.Strength, Strength);
        PrimaryStatsDict.Add(PrimaryAttributes.Vitality, Vitality);
        PrimaryStatsDict.Add(PrimaryAttributes.Speed, Speed);
        PrimaryStatsDict.Add(PrimaryAttributes.Attack, Attack);
        PrimaryStatsDict.Add(PrimaryAttributes.Defense, Defense);
        PrimaryStatsDict.Add(PrimaryAttributes.Resolve, Resolve);

        LVL.OnLevelIncreased += IncreaseLevel;
    }

    private void IncreaseLevel()
    {
        // Activate options to increase stats
    }

    private void IncreasePerks()
    {

    }


    [System.Serializable]
    public class CharacterLeveL
    {
        #region *********** Events and Actions ***************
        public event Action OnLevelIncreased;

        public delegate void ExperienceChanged(int Required, int Current);
        public event ExperienceChanged OnExperienceChanged;
        #endregion

        
        public int LVL;
        
        public List<int> ExperienceReqByLVL;

        public int Required;

        [SerializeField]
        private int _Current;
        public int Current
        {
            get { return _Current; }
            set
            {
                _Current = value;

                if (Current == Required)
                {
                    IncreaseLevel();
                    _Current = 0;
                }

                OnExperienceChanged(Required, Current);
            }
        }

        private void IncreaseLevel()
        {
            LVL++;

            Required = ExperienceReqByLVL[LVL];

            OnLevelIncreased();
        }
    }
}