using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Item Menu/Armor/New Armor", order = 1)]
public class ArmorScriptableObject : ItemScriptableObject
{
    [Header("***************** Armor SO ***************")]
    public int Armour;
    public float StaminaDamageReductionPerc;
    [Space]

    public ArmorAudioConfigurationScriptableObject ArmorAudioConfigSO;

    public override void Equip(Transform Parent, CharacterManager CharacterManager, SkinnedMeshRenderer TargetSkinnedMesh)
    {
        base.Equip(Parent, CharacterManager, TargetSkinnedMesh);

        character.Status.Armour.ChangeBaseStat(Armour);
        character.Status.Armour.CurrentValue = character.Status.Armour.GetValue();
        character.StatusUI.UpdateArmourUI((int)character.Status.Armour.GetValue(), (int)character.Status.Armour.CurrentValue);
    }
    public override void Unequip()
    {
        base.Unequip();

        character.Status.Armour.ChangeBaseStat(Armour);
        character.Status.Armour.CurrentValue = character.Status.Armour.GetValue();
        character.StatusUI.UpdateArmourUI((int)character.Status.Armour.GetValue(), (int)character.Status.Armour.CurrentValue);
    }

    public override string GetDescriptionComparison(ItemScriptableObject EquippedItem)
    {
        ArmorScriptableObject equippedArmor = EquippedItem as ArmorScriptableObject;

        StringBuild.Length = 0;

        #region ************* Armour Comparison *************
        StringBuild.Append("Increase Armour Point by " + Armour);

        if (Armour >= equippedArmor.Armour)
        {
            StringBuild.Append("<color=#F1E23C>");
            
            StringBuild.Append(" (+" + Mathf.Abs(Armour - equippedArmor.Armour) + ")");
            
            StringBuild.Append("</color>");
        }
        else
        {
            StringBuild.Append("<color=#E44F4B>");

            StringBuild.Append(" (-" + Mathf.Abs(Armour - equippedArmor.Armour) + ")");

            StringBuild.Append("</color>");
        }
        #endregion
        
        StringBuild.Append("\n");

        #region ************* Weight Comparison ****************
        StringBuild.Append("Increase Weight by " + Weight);

        if (Weight >= equippedArmor.Weight)
        {
            StringBuild.Append("<color=#E44F4B>");

            StringBuild.Append(" (+" + Mathf.Abs(Weight - equippedArmor.Weight) + ")");

            StringBuild.Append("</color>");
        }
        else
        {
            StringBuild.Append("<color=#F1E23C>");

            StringBuild.Append(" (-" + Mathf.Abs(Weight - equippedArmor.Weight) + ")");

            StringBuild.Append("</color>");
        }
        #endregion

        StringBuild.Append("\n");

        #region ************* Stamina Damage Reduction *************
        StringBuild.Append("Reduce Stamina Damage by " + StaminaDamageReductionPerc + "%");

        if (StaminaDamageReductionPerc >= equippedArmor.StaminaDamageReductionPerc)
        {
            StringBuild.Append("<color=#F1E23C>");

            StringBuild.Append(" (+" + Mathf.Abs(StaminaDamageReductionPerc - equippedArmor.StaminaDamageReductionPerc) + "%)");

            StringBuild.Append("</color>");
        }
        else
        {
            StringBuild.Append("<color=#E44F4B>");

            StringBuild.Append(" (-" + Mathf.Abs(StaminaDamageReductionPerc - equippedArmor.StaminaDamageReductionPerc) + "%)");

            StringBuild.Append("</color>");
        }
        #endregion

        return StringBuild.ToString();
    }

    public override object Clone()
    {
        ArmorScriptableObject clone = CreateInstance<ArmorScriptableObject>();

        clone.Name = Name;
        clone._id = _id;
        clone.Name = Name;
        clone.Type = Type;
        clone.Weight = Weight;
        clone.Value = Value;
        clone.RequiredLevel = RequiredLevel;
        clone.ModelPrefab = ModelPrefab;
        clone.ModelSprite = ModelSprite;
        clone.Armour = Armour;
        clone.StaminaDamageReductionPerc = StaminaDamageReductionPerc;
        clone.ArmorAudioConfigSO = ArmorAudioConfigSO;

        return clone;
    }
}