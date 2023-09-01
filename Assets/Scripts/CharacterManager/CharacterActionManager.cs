using UnityEngine;

public enum HitRegion
{
    High,
    Mid,
    Low
}
public class CharacterActionManager : MonoBehaviour
{
    private CharacterManager character;
    [HideInInspector] public WeaponCollider BloodSpawner;
    private AnimationEventController animEventController;

    private void Start()
    {
        character = GetComponentInParent<CharacterManager>();

        animEventController = GetComponentInChildren<AnimationEventController>();

        animEventController.Damage += DealDamage;

        //character.Status.OnTakeDamage += TakeDamage;
    }

    // ************************** Character Animation

    public HitRegion AttackAction(bool Success, HitRegion HitRegion)            // Returns which region the character is going to attack based on weapon limitations, such as how Axe has no stab 
    {
        if (Success)
        {
            character.Animator.SetTrigger("Attack");
            BloodSpawner.Activate(true);
        }
        else
        {
            character.Animator.SetTrigger("AttackF");
        }

        character.Animator.SetFloat("AttackType", (int)HitRegion);

        WeaponScriptableObject WeaponSO = (character.EquipmentManager.EquippedItemsDict[EquipmentType.PrimaryWeapon] as WeaponScriptableObject);
        switch (WeaponSO.WeaponTypeConfigSO.WeaponType)
        {
            case WeaponType.Axe:
                return HitRegion;
            case WeaponType.Mace:
                return HitRegion;
            case WeaponType.Sword:
                return HitRegion;
            default:
                return HitRegion;
        }
    }

    public void BlockAction(HitRegion HitRegion)
    {
        character.Animator.SetTrigger("Block");
        character.Animator.SetFloat("AttackType", (int)HitRegion);

        WeaponScriptableObject WeaponSO = (character.EquipmentManager.EquippedItemsDict[EquipmentType.PrimaryWeapon] as WeaponScriptableObject);
        WeaponSO.WeaponAudioConfigSO.PlayBlockSound(character.AudioSource);
    }

    // ************************* Deal Damage Animation Event
    public void DealDamage(HitRegion HitRegion)
    {
        WeaponScriptableObject WeaponSO = (character.EquipmentManager.EquippedItemsDict[EquipmentType.PrimaryWeapon] as WeaponScriptableObject);

        CharacterManager target = CompareTag("Player") ? BattleManager.Characters[Gladiator.Enemy] : BattleManager.Characters[Gladiator.Player];

        target.Status.TakeDamage((int)Random.Range(WeaponSO.Damage.MinDamage.GetValue(), WeaponSO.Damage.MaxDamage.GetValue()), HitRegion, WeaponSO);
    }
}