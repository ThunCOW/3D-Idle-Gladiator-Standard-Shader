using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleStatPanel : MonoBehaviour
{
    [Header("****** Battle Stat Top Bar Text ******")]
    [SerializeField]
    private TMP_Text Battle_Stat_Text;
    [SerializeField]
    private Color Battle_Stat_Text_SelectedColor;
    private Color Battle_Stat_Text_DefaultColor;

    [SerializeField]
    private BattleStatInfo Player;
    [SerializeField]
    private BattleStatInfo Enemy;


    private void Start()
    {
        Battle_Stat_Text_DefaultColor = Color.white;
    }

    private void PopulateBattleStatInfo(CharacterManager Character)
    {
        BattleStatInfo bsi = null;
        if (Character is PlayerCharacterManager)
            bsi = Player;
        else
            bsi = Enemy;

        //bsi.Hp.text = ((int)Character.Status.Health.CurrentValue).ToString() + " / " + ((int)Character.Status.Health.GetValue()).ToString();
        //bsi.Armour.text = ((int)Character.Status.Armour.CurrentValue).ToString() + " / " + ((int)Character.Status.Armour.GetValue()).ToString();
        //bsi.Stamina.text = ((int)Character.Status.Fatigue.CurrentValue).ToString() + " / " + ((int)Character.Status.Fatigue.GetValue()).ToString();
    }

    private void OnEnable()
    {
        Battle_Stat_Text.color = Battle_Stat_Text_SelectedColor;

        PopulateBattleStatInfo(BattleManager.Characters[Gladiator.Player]);
        PopulateBattleStatInfo(BattleManager.Characters[Gladiator.Enemy]);
    }

    private void OnDisable()
    {
        Battle_Stat_Text.color = Battle_Stat_Text_DefaultColor;
    }

    [System.Serializable]
    private class BattleStatInfo
    {
        public TMP_Text LVL;
        
        public TMP_Text Hp;
        public TMP_Text Armour;
        public TMP_Text Stamina;

        public TMP_Text Damage;
        public TMP_Text Hit_Chance;
        public TMP_Text Attack_Speed;
    }
}
