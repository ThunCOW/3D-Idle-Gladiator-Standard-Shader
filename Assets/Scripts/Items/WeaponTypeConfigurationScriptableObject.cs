using UnityEngine;

public enum WeaponType
{
    Axe,
    Sword,
    Mace
}

[CreateAssetMenu(fileName = "New Weapon Config", menuName = "Item Menu/Weapon/New Weapon Config")]
public class WeaponTypeConfigurationScriptableObject : ScriptableObject
{
    public WeaponType WeaponType;
    public float EffectivenessAgainstArmour;
    public float IgnoreArmourEffectiveness;
    public float BonusHitChance;
}