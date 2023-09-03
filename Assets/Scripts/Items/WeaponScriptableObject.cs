using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item Menu/Weapon/New Weapon", order = 0)]
public class WeaponScriptableObject : ItemScriptableObject
{
    [Header("*************** Weapon SO *************")]
    public GameObject WeaponColliderPrefab;

    public WeaponDamage Damage;

    [Space]
    public WeaponTypeConfigurationScriptableObject WeaponTypeConfigSO;
    public WeaponAudioConfigurationScriptableObject WeaponAudioConfigSO;

    protected GameObject weaponColliderSceneRef;

    /// <summary>
    /// Instantiating equipments in a custom hierarcy
    /// </summary>
    /// <param name="Parent">Equipment is going to be be child of the Parent in the scene</param>
    /// <param name="CharacterManager">Character Manager</param>
    /// <param name="TargetSkinnedMesh">Since its custom instantiated, Skinned meshes need to retarget to the right bones which is contained in the main body</param>
    /// <param name="WeaponColliderSpawnParent">Weapons are instantiated as skinned mesh object and part of animation, so weapon collision is calculated by seperate weapon collider objects with no model</param>
    public void Equip(Transform Parent, CharacterManager CharacterManager, SkinnedMeshRenderer TargetSkinnedMesh, Transform WeaponColliderSpawnParent)
    {
        base.Equip(Parent, CharacterManager, TargetSkinnedMesh);
        weaponColliderSceneRef = Instantiate(WeaponColliderPrefab, WeaponColliderSpawnParent);

        //character.Attributes.HitBonus += WeaponTypeConfigSO.BonusHitChance;
    }

    public override void Unequip()
    {
        base.Unequip();
        Destroy(weaponColliderSceneRef);

        //character.Attributes.HitBonus -= WeaponTypeConfigSO.BonusHitChance;
    }

    public int GetDamage()
    {
        return Random.Range((int)Damage.MinDamage.GetValue(), (int)Damage.MaxDamage.GetValue());
    }

    public override string GetDescriptionComparison(ItemScriptableObject EquippedItem)
    {
        WeaponScriptableObject equippedWeapon = EquippedItem as WeaponScriptableObject;

        StringBuild.Length = 0;

        #region **************** Damage Comparison **************
        StringBuild.Append("" + Damage.MinDamage.GetValue() + " - " +  Damage.MaxDamage.GetValue() + " Weapon Damage");

        StringBuild.Append(" (");

        if(Damage.MinDamage.GetValue() >= equippedWeapon.Damage.MinDamage.GetValue())
        {
            StringBuild.Append("<color=#F1E23C>");

            StringBuild.Append(" +" + Mathf.Abs(Damage.MinDamage.GetValue() - equippedWeapon.Damage.MinDamage.GetValue()));

            StringBuild.Append("</color>");
        }
        else
        {
            StringBuild.Append("<color=#E44F4B>");

            StringBuild.Append(" -" + Mathf.Abs(Damage.MinDamage.GetValue() - equippedWeapon.Damage.MinDamage.GetValue()));

            StringBuild.Append("</color>");
        }

        StringBuild.Append(" -");

        if (Damage.MaxDamage.GetValue() >= equippedWeapon.Damage.MaxDamage.GetValue())
        {
            StringBuild.Append("<color=#F1E23C>");

            StringBuild.Append(" +" + Mathf.Abs(Damage.MaxDamage.GetValue() - equippedWeapon.Damage.MaxDamage.GetValue()));

            StringBuild.Append("</color>");
        }
        else
        {
            StringBuild.Append("<color=#E44F4B>");

            StringBuild.Append(" (-" + Mathf.Abs(Damage.MaxDamage.GetValue() - equippedWeapon.Damage.MaxDamage.GetValue()));

            StringBuild.Append("</color>");
        }

        StringBuild.Append(" )");

        #endregion

        StringBuild.Append("\n");

        #region ************* Weight Comparison ****************
        StringBuild.Append("Increase Weight by " + Weight);

        if (Weight >= equippedWeapon.Weight)
        {
            StringBuild.Append("<color=#E44F4B>");

            StringBuild.Append(" (+" + Mathf.Abs(Weight - equippedWeapon.Weight) + ")");

            StringBuild.Append("</color>");
        }
        else
        {
            StringBuild.Append("<color=#F1E23C>");

            StringBuild.Append(" (-" + Mathf.Abs(Weight - equippedWeapon.Weight) + ")");

            StringBuild.Append("</color>");
        }
        #endregion

        return StringBuild.ToString();
    }

    public override object Clone()
    {
        WeaponScriptableObject clone = CreateInstance<WeaponScriptableObject>();

        clone.Name = Name;
        clone._id = _id;
        clone.Name = Name;
        clone.Type = Type;
        clone.Weight = Weight;
        clone.Value = Value;
        clone.RequiredLevel = RequiredLevel;
        clone.ModelPrefab = ModelPrefab;
        clone.ModelSprite = ModelSprite;
        clone.WeaponColliderPrefab = WeaponColliderPrefab;
        clone.Damage = Damage;
        clone.WeaponTypeConfigSO = WeaponTypeConfigSO;
        clone.WeaponAudioConfigSO = WeaponAudioConfigSO;
        clone.weaponColliderSceneRef = weaponColliderSceneRef;

        return clone;
    }

    [System.Serializable]
    public class WeaponDamage
    {
        public DerivativeStat MinDamage;
        public DerivativeStat MaxDamage;
    }
}