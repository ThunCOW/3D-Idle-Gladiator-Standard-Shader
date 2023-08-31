using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Random Equipment By Level", menuName = "Item Menu/Random Equipment List By LVL")]
public class RandomEquipmentByLevelScriptableObject : ScriptableObject
{
    public List<RandomEquipmentByLevel> RandomEquipmentByLevelList = new List<RandomEquipmentByLevel>();

    public Dictionary<int, RandomEquipmentByLevel> RandomEquipmentByLevelDict;

    public void Init()
    {
        RandomEquipmentByLevelDict = new Dictionary<int, RandomEquipmentByLevel>();

        foreach (RandomEquipmentByLevel itemList in RandomEquipmentByLevelList)
        {
            itemList.Init();
            
            RandomEquipmentByLevelDict.Add(itemList.LVL, itemList);
        }
    }

    public void GetEquipment(int LVL)
    {
        List<int> keys = new List<int>(RandomEquipmentByLevelDict.Keys);

        int selectedLVL;
        foreach (int key in keys)
        {
            if (key < LVL)
                selectedLVL = key;
            else
                break;
        }
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

        public Dictionary<EquipmentType, ItemScriptableObject> RandomItemDict;

        public void Init()
        {
            RandomItemDict = new Dictionary<EquipmentType, ItemScriptableObject>();
            PopulateDict(Helmet);
            PopulateDict(Breastplate);
            PopulateDict(Shooulders);
            PopulateDict(Gauntlets);
            PopulateDict(Pants);
            PopulateDict(Shoes); 
            PopulateDict(PrimaryWeapons);
            PopulateDict(SecondaryWeapons);
        }

        private void PopulateDict(List<ItemScriptableObject> ItemList)
        {
            foreach (ItemScriptableObject item in ItemList)
            {
                RandomItemDict.Add(item.Type, item);
            }
        }
    }
}
