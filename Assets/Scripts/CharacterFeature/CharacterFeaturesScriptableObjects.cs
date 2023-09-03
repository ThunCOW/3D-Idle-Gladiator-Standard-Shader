using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFeature
{
    [CreateAssetMenu(fileName = "Character Feature Randomizer", menuName = "Character Randomizer SO")]
    public class CharacterFeaturesScriptableObjects : ScriptableObject
    {
        [Header("*********** Head Features **********")]
        public List<GameObject> HairPrefabList;
        public List<GameObject> EyebrowPrefabList;
        public List<GameObject> EyeLashesPrefabList;
        public List<GameObject> EyePrefabList;
        public List<GameObject> MoustachePrefabList;
        public List<GameObject> BeardPrefabList;

        [Header("********** Color Features ***********")]
        public List<Color> BodyColorList;
        public List<Color> HairColor;
        public List<Color> EyeColor;

        // TODO: Can be simplified with directory and enums but not prio
        public void RandomizeCharacter(CharacterManager CharacterManager)
        {
            CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Hair, HairPrefabList[Random.Range(0, HairPrefabList.Count)]);
            CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Eyebrow, EyebrowPrefabList[Random.Range(0, EyebrowPrefabList.Count)]);
            CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Eye, EyePrefabList[Random.Range(0, EyePrefabList.Count)]);
            CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Moustache, MoustachePrefabList[Random.Range(0, MoustachePrefabList.Count)]);
            CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Beard, BeardPrefabList[Random.Range(0, BeardPrefabList.Count)]);

            CharacterManager.CharacterFeatureManager.ChangeColor(CharacterColorFeatures.BodyColor, BodyColorList[Random.Range(0, BodyColorList.Count)]);
            CharacterManager.CharacterFeatureManager.ChangeColor(CharacterColorFeatures.HairColor, HairColor[Random.Range(0, HairColor.Count)]);
            CharacterManager.CharacterFeatureManager.ChangeColor(CharacterColorFeatures.EyeColor, EyeColor[Random.Range(0, EyeColor.Count)]);
        }
    }
}