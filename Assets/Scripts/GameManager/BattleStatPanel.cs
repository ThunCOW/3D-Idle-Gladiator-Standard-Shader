using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleStatPanel : MonoBehaviour
{
    public static BattleStatPanel Instance;

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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Battle_Stat_Text_DefaultColor = Color.white;
    }

    public void AssignEvents(CharacterManager characterManager)
    {
        characterManager.Status.Health.CurrentValueChanged += PopulateBattleStatInfo;                   // Current Health
        characterManager.Attributes.Vitality.AttributeChanged += PopulateBattleStatInfo;                // Max Health

        characterManager.Status.Armour.CurrentValueChanged += PopulateBattleStatInfo;                   // Current Armour
        characterManager.Status.Armour.AttributeChanged += PopulateBattleStatInfo;                      // Max Armour

        characterManager.Status.Fatigue.CurrentValueChanged += PopulateBattleStatInfo;                  // Current Stamina
        characterManager.Status.Fatigue.AttributeChanged += PopulateBattleStatInfo;                     // Max Stamina

        characterManager.EquipmentManager.WeaponChanged += PopulateBattleStatInfo;
        characterManager.Attributes.Defense.AttributeChanged += PopulateBattleStatInfo;
        characterManager.Attributes.Attack.AttributeChanged += PopulateBattleStatInfo;

        PopulateBattleStatInfo();
    }

    public void RemoveEvents(CharacterManager characterManager)
    {
        characterManager.Status.Health.CurrentValueChanged -= PopulateBattleStatInfo;                   // Current Health
        characterManager.Attributes.Vitality.AttributeChanged -= PopulateBattleStatInfo;                // Max Heal-h

        characterManager.Status.Armour.CurrentValueChanged -= PopulateBattleStatInfo;                   // Current Armour
        characterManager.Status.Armour.AttributeChanged -= PopulateBattleStatInfo;                      // Max Armo-r

        characterManager.Status.Fatigue.CurrentValueChanged -= PopulateBattleStatInfo;                  // Current Stamina
        characterManager.Status.Fatigue.AttributeChanged -= PopulateBattleStatInfo;                     // Max Stami-a

        characterManager.EquipmentManager.WeaponChanged -= PopulateBattleStatInfo;
        characterManager.Attributes.Defense.AttributeChanged -= PopulateBattleStatInfo;
        characterManager.Attributes.Attack.AttributeChanged -= PopulateBattleStatInfo;
    }

    private void PopulateBattleStatInfo()
    {
        if (BattleManager.Characters[Gladiator.Player] != null && BattleManager.Characters[Gladiator.Enemy] != null)
        {
            PopulateBattleStatInfo(BattleManager.Characters[Gladiator.Player]);
            PopulateBattleStatInfo(BattleManager.Characters[Gladiator.Enemy]);
        }
    }

    private void PopulateBattleStatInfo(CharacterManager Character)
    {
        BattleStatInfo bsi = null;
        if (Character is PlayerCharacterManager)
            bsi = Player;
        else
            bsi = Enemy;

        bsi.Hp.text = ((int)Character.Status.Health.CurrentValue).ToString() + " / " + ((int)Character.Status.Health.GetValue()).ToString();
        bsi.Armour.text = ((int)Character.Status.Armour.CurrentValue).ToString() + " / " + ((int)Character.Status.Armour.GetValue()).ToString();
        bsi.Stamina.text = ((int)Character.Status.Fatigue.CurrentValue).ToString() + " / " + ((int)Character.Status.Fatigue.GetValue()).ToString();

        if (Character.EquipmentManager.EquippedItemsDict[EquipmentType.PrimaryWeapon] != null)
        {
            WeaponScriptableObject.WeaponDamage weaponDamage = (Character.EquipmentManager.EquippedItemsDict[EquipmentType.PrimaryWeapon] as WeaponScriptableObject).Damage;

            bsi.Damage.text = weaponDamage.MinDamage.GetValue().ToString() + " - " + weaponDamage.MaxDamage.GetValue().ToString();
        }

        bsi.Hit_Chance.text = Character is PlayerCharacterManager ? 
                                BattleStatus.Instance.Player.HitChance.ToString() + "%" : 
                                BattleStatus.Instance.Enemy.HitChance.ToString() + "%";
        bsi.Attack_Speed.text = Character is PlayerCharacterManager ? 
                                (BattleStatus.Instance.Player.AttackSpeed * 100).ToString() + "%" : 
                                (BattleStatus.Instance.Enemy.AttackSpeed * 100).ToString() + "%";
    }

    public void Btn_Enable()
    {
        Battle_Stat_Text.color = Battle_Stat_Text_SelectedColor;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Btn_Disable()
    {
        Battle_Stat_Text.color = Battle_Stat_Text_DefaultColor;
        transform.GetChild(0).gameObject.SetActive(false);
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
