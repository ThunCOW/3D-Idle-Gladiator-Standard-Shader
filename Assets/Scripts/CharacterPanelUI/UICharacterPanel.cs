using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterPanel : MonoBehaviour
{
    [Header("************ EXP **************")]
    public TMP_Text Rank;
    public TMP_Text LVL;
    public TMP_Text EXP;
    public RectTransform EXP_BAR;

    [Header("*********** Attributes *********")]
    public TMP_Text STR;
    public TMP_Text VIT;
    public TMP_Text ATT;
    public TMP_Text DEF;
    public TMP_Text RES;
    public List<Button> PlusButtons;
    private Dictionary<PrimaryAttributes, TMP_Text> UIPrimaryStatDict;

    private Dictionary<PrimaryAttributes, int> StatWaitingConfirmation;
    //private Dictionary<PrimaryAttributes, >

    [Header("*********** Unspent Points ************")]
    public TMP_Text UnspentAttP_Text;
    [SerializeField]
    private int unspentAttTotalP;           // Total Unspent Point
    [SerializeField]
    private int _unspentAttP;               // Remaining Unspent Point
    public int UnspentAttP
    {
        get { return _unspentAttP; }
        set
        {
            _unspentAttP = value;

            if (_unspentAttP == unspentAttTotalP && _unspentPerkP == unspentPerkTotalP)     // means no point is spent
            {
                ActivateButton(false);                                                      // so turn off the button
            }
        }
    }

    [Space]
    public TMP_Text UnspentPerkP_Text;
    [SerializeField]
    private int unspentPerkTotalP;          // Total Unspent Point
    [SerializeField]
    private int _unspentPerkP;              // Remaining Unspent Point
    public int UnspentPerkP
    {
        get { return _unspentPerkP; }
        set
        {
            _unspentPerkP = value;

            if (_unspentAttP == unspentAttTotalP && _unspentPerkP == unspentPerkTotalP)     // means no point is spent
            {
                ActivateButton(false);                                                      // so turn off the button
            }
        }
    }

    [Space]
    public Button Btn_Confirm;
    public TMP_Text Btn_Confirm_Text;


    // Start is called before the first frame update
    void Start()
    {
        #region UIPrimaryStatDict Dictionary
        UIPrimaryStatDict = new Dictionary<PrimaryAttributes, TMP_Text>();
        UIPrimaryStatDict.Add(PrimaryAttributes.Strength, STR);
        UIPrimaryStatDict.Add(PrimaryAttributes.Vitality, VIT);
        //UIPrimaryStatDict.Add(PrimaryAttributes.Speed, SPD);
        UIPrimaryStatDict.Add(PrimaryAttributes.Attack, ATT);
        UIPrimaryStatDict.Add(PrimaryAttributes.Defense, DEF);
        UIPrimaryStatDict.Add(PrimaryAttributes.Resolve, RES);
        #endregion

        #region StatWaitingConfirmation Dictionary
        StatWaitingConfirmation = new Dictionary<PrimaryAttributes, int>();
        StatWaitingConfirmation.Add(PrimaryAttributes.Strength, 0);
        StatWaitingConfirmation.Add(PrimaryAttributes.Vitality, 0);
        //StatWaitingConfirmation.Add(PrimaryAttributes.Speed, 0);
        StatWaitingConfirmation.Add(PrimaryAttributes.Attack, 0);
        StatWaitingConfirmation.Add(PrimaryAttributes.Defense, 0);
        StatWaitingConfirmation.Add(PrimaryAttributes.Resolve, 0);
        #endregion

        BattleManager.Characters[Gladiator.Player].Attributes.LVL.OnLevelIncreased += LevelIncreased;
        BattleManager.Characters[Gladiator.Player].Attributes.LVL.OnExperienceChanged += UIUpdateExperience;
    }

    private void UIUpdateExperience(int Required, int Current)
    {
        EXP.text = Current + " / " + Required;
        EXP_BAR.localScale = new Vector3(Current / Required, 1, 1);
    }

    private void LevelIncreased()
    {
        LVL.text = BattleManager.Characters[Gladiator.Player].Attributes.LVL.LVL.ToString();
        
        foreach (Button button in PlusButtons)
        {
            button.interactable = true;
        }

        unspentAttTotalP += 5;
        unspentPerkTotalP += 4;
        UnspentAttP += 5;
        UnspentPerkP += 4;
        UnspentAttP_Text.text = UnspentAttP.ToString();
        UnspentPerkP_Text.text = UnspentPerkP.ToString();

        ActivateButton(true);
    }

    public void ConfirmButtonClick()
    {
        ActivateButton(false);

        foreach (KeyValuePair<PrimaryAttributes, int> instance in StatWaitingConfirmation)
        {
            BattleManager.Characters[Gladiator.Player].Attributes.PrimaryStatsDict[instance.Key].ChangeBaseStat(instance.Value);
            
            unspentAttTotalP -= instance.Value;
            
            StatWaitingConfirmation[instance.Key] = 0;
        }
    }

    private void ActivateButton(bool Activate)
    {
        Btn_Confirm.interactable = Activate;
        Color c = Color.white;
        c.a = Activate ? 1 : 0.3f;
        Btn_Confirm_Text.color = c;
    }

    #region ********** Increase / Decrease Attribute Button Logic (UI) **********
    public void AttributeIncrease(AttributeDecisionComponent ATC)
    {
        UnspentAttP--;

        PrimaryAttributes Attribute = ATC.Attribute;

        StatWaitingConfirmation[Attribute]++;

        UIPrimaryStatDict[Attribute].text = StatWaitingConfirmation[Attribute].ToString();

        ActivateButton(true);
    }

    public void AttributeDecreased(AttributeDecisionComponent ATC)
    {
        UnspentAttP++;

        PrimaryAttributes Attribute = ATC.Attribute;

        StatWaitingConfirmation[Attribute]--;

        UIPrimaryStatDict[Attribute].text = StatWaitingConfirmation[Attribute].ToString();
    }
    #endregion
}