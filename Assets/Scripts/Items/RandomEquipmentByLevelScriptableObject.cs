using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Random Equipment By Level", menuName = "Item Menu/Random Equipment List By LVL")]
public class RandomEquipmentByLevelScriptableObject : ScriptableObject
{
    [SerializeField] 
    private List<RandomEquipmentByLevel> RandomEquipmentByLevelList = new List<RandomEquipmentByLevel>();

    [SerializeField] 
    private Dictionary<int, RandomEquipmentByLevel> RandomEquipmentByLevelDict;

    public void Init()
    {
        RandomEquipmentByLevelDict = new Dictionary<int, RandomEquipmentByLevel>();

        foreach (RandomEquipmentByLevel itemList in RandomEquipmentByLevelList)
        {
            itemList.Init();
            
            RandomEquipmentByLevelDict.Add(itemList.LVL, itemList);
        }
    }

    public List<ItemScriptableObject> GetEquipment(int LVL)
    {
        List<int> keys = new List<int>(RandomEquipmentByLevelDict.Keys);

        int selectedLVL = 0;
        foreach (int key in keys)
        {
            if (key <= LVL)
                selectedLVL = key;
            else
                break;
        }

        List<ItemScriptableObject> itemList = new List<ItemScriptableObject>();

        // 25% chance to have a hat
        if (Random.Range(0, 12) < 3)
            RandomEquipmentByLevelDict[selectedLVL].GetRandomItem(EquipmentType.Helmet, itemList);

        // 75% chance to have a breastplate
        if (Random.Range(0, 12) < 9)
            RandomEquipmentByLevelDict[selectedLVL].GetRandomItem(EquipmentType.Breatplate, itemList);

        if (Random.Range(0, 12) < 7)
            RandomEquipmentByLevelDict[selectedLVL].GetRandomItem(EquipmentType.Shoulder, itemList);

        if (Random.Range(0, 12) < 11)
            RandomEquipmentByLevelDict[selectedLVL].GetRandomItem(EquipmentType.Gauntlets, itemList);

        if (Random.Range(0, 12) < 8)
            RandomEquipmentByLevelDict[selectedLVL].GetRandomItem(EquipmentType.Shoes, itemList);
        
        RandomEquipmentByLevelDict[selectedLVL].GetRandomItem(EquipmentType.Pants, itemList);
        RandomEquipmentByLevelDict[selectedLVL].GetRandomItem(EquipmentType.PrimaryWeapon, itemList);
        RandomEquipmentByLevelDict[selectedLVL].GetRandomItem(EquipmentType.SecondaryWeapon, itemList);

        return itemList;
    }

    [System.Serializable]
    public class RandomEquipmentByLevel
    {
        public int LVL;

        public List<ItemScriptableObject> Helmet;
        public List<ItemScriptableObject> Breastplate;
        public List<ItemScriptableObject> Shooulders;
        public List<ItemScriptableObject> Gauntlets;
        public List<ItemScriptableObject> Pants;
        public List<ItemScriptableObject> Shoes;
        public List<ItemScriptableObject> PrimaryWeapons;
        public List<ItemScriptableObject> SecondaryWeapons;

        public Dictionary<EquipmentType, List<ItemScriptableObject>> RandomItemDict;

        public void Init()
        {
            RandomItemDict = new Dictionary<EquipmentType, List<ItemScriptableObject>>();
            RandomItemDict.Add(EquipmentType.Helmet, Helmet);
            RandomItemDict.Add(EquipmentType.Breatplate, Breastplate);
            RandomItemDict.Add(EquipmentType.Shoulder, Shooulders);
            RandomItemDict.Add(EquipmentType.Gauntlets, Gauntlets);
            RandomItemDict.Add(EquipmentType.Pants, Pants);
            RandomItemDict.Add(EquipmentType.Shoes, Shoes);
            RandomItemDict.Add(EquipmentType.PrimaryWeapon, PrimaryWeapons);
            RandomItemDict.Add(EquipmentType.SecondaryWeapon, SecondaryWeapons);
            //PopulateDict(Helmet);
            //PopulateDict(Breastplate);
            //PopulateDict(Shooulders);
            //PopulateDict(Gauntlets);
            //PopulateDict(Pants);
            //PopulateDict(Shoes); 
            //PopulateDict(PrimaryWeapons);
            //PopulateDict(SecondaryWeapons);
        }

        private void PopulateDict(List<ItemScriptableObject> ItemList)
        {
            //foreach (ItemScriptableObject item in ItemList)
            //{
            //    RandomItemDict.Add(item.Type, item);
            //}
        }

        public void GetRandomItem(EquipmentType type, List<ItemScriptableObject> list)
        {
            if (RandomItemDict[type].Count == 0)
                return;
            
            list.Add(RandomItemDict[type][Random.Range(0, RandomItemDict[type].Count)]);
        }
    }
}
