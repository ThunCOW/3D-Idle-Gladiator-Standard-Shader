using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatus : MonoBehaviour
{
    public static BattleStatus Instance;

    public BattleStatusInfo Player;
    public BattleStatusInfo Enemy;

    private void Awake()
    {
        Instance = this;    
    }

    public void AssignEvents(CharacterManager characterManager)
    {
        characterManager.EquipmentManager.WeaponChanged += PopulateBattleStats;
        
        characterManager.Attributes.Defense.AttributeChanged += PopulateBattleStats;
        
        characterManager.Attributes.Attack.AttributeChanged += PopulateBattleStats;
    }

    public void RemoveEvents(CharacterManager characterManager)
    {
        characterManager.EquipmentManager.WeaponChanged -= PopulateBattleStats;

        characterManager.Attributes.Defense.AttributeChanged -= PopulateBattleStats;

        characterManager.Attributes.Attack.AttributeChanged -= PopulateBattleStats;
    }

    private void PopulateBattleStats()
    {
        //PopulateBattleStats(BattleManager.Characters[Gladiator.Player], BattleManager.Characters[Gladiator.Enemy]);
    }

    private void PopulateBattleStats(CharacterManager playerCManager, CharacterManager enemyCManager)
    {
        Player.HitChance = (int)(playerCManager.Attributes.Attack.GetValue() - enemyCManager.Attributes.Defense.GetValue());
        Enemy.HitChance = (int)(enemyCManager.Attributes.Attack.GetValue() - playerCManager.Attributes.Defense.GetValue());

        Player.DefendChance = (int)(playerCManager.Attributes.Defense.GetValue() - enemyCManager.Attributes.Attack.GetValue());
        Enemy.DefendChance = (int)(enemyCManager.Attributes.Defense.GetValue() - playerCManager.Attributes.Attack.GetValue());


        Player.AttackSpeed = 1 + (int)((playerCManager.Attributes.Weight.GetValue() - playerCManager.Attributes.Weight.CurrentValue) -
                                        (enemyCManager.Attributes.Weight.GetValue() - enemyCManager.Attributes.Weight.CurrentValue));

        Enemy.AttackSpeed = 1 + (int)((enemyCManager.Attributes.Weight.GetValue() - enemyCManager.Attributes.Weight.CurrentValue) -
                                        (playerCManager.Attributes.Weight.GetValue() - playerCManager.Attributes.Weight.CurrentValue));
    }

    [System.Serializable]
    public class BattleStatusInfo
    {
        public int HitChance;
        public int DefendChance;
        public int AttackSpeed;
    }
}
