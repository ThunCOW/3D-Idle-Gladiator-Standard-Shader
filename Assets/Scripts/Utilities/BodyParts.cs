using System.Collections.Generic;
using UnityEngine;

namespace EquipItemEditor
{
    public class BodyParts : MonoBehaviour
    {
        // EquipItemEditor selects the target body parts from this list, which is listed in order of :
        /*
         * Helmet
         * Breatplate
         * Shoulder
         * Gauntlets
         * Pants
         * Shoes
         * PrimaryWeapon
         * SecondaryWeapon
         * */

        public GameObject Helmet;
        public GameObject Breatplate;
        public GameObject Shoulder;
        public GameObject Gauntlets;
        public GameObject Pants;
        public GameObject Shoes;
        public GameObject PrimaryWeapon;
        public GameObject SecondaryWeapon;

        [HideInInspector] public List<GameObject> Parts;

        public Dictionary<EquipmentType, GameObject> BodyPartsDict;

        void OnValidate()
        {
            Parts = new List<GameObject>();
            Parts.Add(Helmet);
            Parts.Add(Breatplate);
            Parts.Add(Shoulder);
            Parts.Add(Gauntlets);
            Parts.Add(Pants);
            Parts.Add(Shoes);
            Parts.Add(PrimaryWeapon);
            Parts.Add(SecondaryWeapon);
        }

        private void Awake()
        {
            BodyPartsDict = new Dictionary<EquipmentType, GameObject>();
            BodyPartsDict.Add(EquipmentType.Helmet, Helmet);
            BodyPartsDict.Add(EquipmentType.Breatplate, Breatplate);
            BodyPartsDict.Add(EquipmentType.Shoulder, Shoulder);
            BodyPartsDict.Add(EquipmentType.Gauntlets, Gauntlets);
            BodyPartsDict.Add(EquipmentType.Pants, Pants);
            BodyPartsDict.Add(EquipmentType.Shoes, Shoes);
            BodyPartsDict.Add(EquipmentType.PrimaryWeapon, PrimaryWeapon);
            BodyPartsDict.Add(EquipmentType.SecondaryWeapon, SecondaryWeapon);
        }
    }
}

