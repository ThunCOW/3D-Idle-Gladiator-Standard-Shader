using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("************ Editor Filled ************")]

    [SerializeField]
    private Transform EquipmentSpawnParent;

    [SerializeField]
    private EquipmentSkinByType EquipmentSkinTargets;

    [Space]
    [SerializeField]
    private Transform PrimaryWeaponColliderSpawnParent;

    [Header("*********** Runtime Filled ***************")]
    [SerializeField]
    private List<ItemScriptableObject> EquippedItemsList;

    [SerializeField]
    [HideInInspector]
    private CharacterManager characterManager;

    [Header("*************** Editor Interaction ***********")]
    [SerializeField] List<ItemScriptableObject> EditorItems;


    public Dictionary<EquipmentType, ItemScriptableObject> EquippedItemsDict;
    private Dictionary<EquipmentType, SkinnedMeshRenderer> EquipmentTargetSkinnedMeshRenderersDict;

    private void Start()
    {
        characterManager = GetComponent<CharacterManager>();

        EquippedItemsDict = new Dictionary<EquipmentType, ItemScriptableObject>();
        EquippedItemsDict.Add(EquipmentType.Helmet, null);
        EquippedItemsDict.Add(EquipmentType.Breatplate, null);
        EquippedItemsDict.Add(EquipmentType.Shoulder, null);
        EquippedItemsDict.Add(EquipmentType.Gauntlets, null);
        EquippedItemsDict.Add(EquipmentType.Pants, null);
        EquippedItemsDict.Add(EquipmentType.Shoes, null);
        EquippedItemsDict.Add(EquipmentType.PrimaryWeapon, null);
        EquippedItemsDict.Add(EquipmentType.SecondaryWeapon, null);

        EquipmentTargetSkinnedMeshRenderersDict = new Dictionary<EquipmentType, SkinnedMeshRenderer>();
        foreach (EquipmentSkinByType.EquipmentSkinTarget EquipmentParent in EquipmentSkinTargets.Target)
        {
            EquipmentTargetSkinnedMeshRenderersDict.Add(EquipmentParent.EquipmentType, EquipmentParent.TargetSkinnedMeshRenderer);
        }

        SetStartingItems();
    }

    private void SetStartingItems()
    {
        foreach (ItemScriptableObject item in EditorItems)
        {
            EquipItem(item.Clone() as ItemScriptableObject);
        }

        EditorItems.Clear();
    }

    public void EquipItem(ItemScriptableObject NewItem)
    {
        // If already something is equipped, unequip them
        if (EquippedItemsDict.TryGetValue(NewItem.Type, out ItemScriptableObject Item))
        {
            //if (Item is null)
            //Debug.LogWarning("dict can return null as value");
            if (Item != null)
            {
                EquippedItemsList.Remove(Item);
                Item.Unequip();
            }
        }

        EquippedItemsList.Add(NewItem);
        Item = NewItem;

        // Equip item
        if (Item is ArmorScriptableObject)
            Item.Equip(EquipmentSpawnParent, characterManager, EquipmentTargetSkinnedMeshRenderersDict[NewItem.Type]);
        else
        {
            (Item as WeaponScriptableObject).Equip(EquipmentSpawnParent, characterManager, EquipmentTargetSkinnedMeshRenderersDict[NewItem.Type], PrimaryWeaponColliderSpawnParent);
            characterManager.CharacterActionManager.BloodSpawner = PrimaryWeaponColliderSpawnParent.GetComponentInChildren<BloodSpawner>();
        }

        EquippedItemsDict[NewItem.Type] = Item;
    }

    [System.Serializable]
    public class EquipmentSkinByType
    {
        public List<EquipmentSkinTarget> Target;

        [System.Serializable]
        public class EquipmentSkinTarget
        {
            public EquipmentType EquipmentType;
            public SkinnedMeshRenderer TargetSkinnedMeshRenderer;
        }
    }
}