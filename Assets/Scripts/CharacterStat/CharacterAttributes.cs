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

    public Dictionary<PrimaryAttributes, Stat> PrimaryStatsDict;
    
    private CharacterManager characterManager;

    private void Start()
    {
        characterManager = GetComponent<CharacterManager>();

        PrimaryStatsDict = new Dictionary<PrimaryAttributes, Stat>();
        PrimaryStatsDict.Add(PrimaryAttributes.Strength, Strength);
        PrimaryStatsDict.Add(PrimaryAttributes.Vitality, Vitality);
        PrimaryStatsDict.Add(PrimaryAttributes.Speed, Speed);
        PrimaryStatsDict.Add(PrimaryAttributes.Attack, Attack);
        PrimaryStatsDict.Add(PrimaryAttributes.Defense, Defense);
        PrimaryStatsDict.Add(PrimaryAttributes.Resolve, Resolve);

        LVL.OnLevelIncreased += IncreaseLevel;
        LVL.Init();
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
        private int _CurrentEXP;
        public int CurrentEXP
        {
            get { return _CurrentEXP; }
            set
            {
                _CurrentEXP = Mathf.Clamp(value, 0, ExperienceReqByLVL[LVL]);

                if (CurrentEXP == Required)
                {
                    IncreaseLevel();
                }

                try
                {
                    OnExperienceChanged(Required, CurrentEXP);         // Only player has this assigned
                }
                catch (Exception e)
                {
                    if (Required != 999)                             // AI Characters has 999 by default
                        Debug.LogError(e);
                }
            }
        }

        public void Init()
        {
            Required = ExperienceReqByLVL[LVL];
            CurrentEXP = 0;
        }

        private void IncreaseLevel()
        {
            if (ExperienceReqByLVL.Count > LVL)
            {
                LVL++;

                _CurrentEXP = 0;

                Required = ExperienceReqByLVL[LVL];

                OnLevelIncreased();
            }
            else
            {
                Debug.Log("Hit Last Level");
            }
        }
    }
}