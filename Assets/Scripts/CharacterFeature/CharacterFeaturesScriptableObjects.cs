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
            CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Hair, Instantiate(HairPrefabList[Random.Range(0, HairPrefabList.Count)]));
            CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Eyebrow, Instantiate(EyebrowPrefabList[Random.Range(0, EyebrowPrefabList.Count)]));
            CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.EyeLash, Instantiate(EyeLashesPrefabList[Random.Range(0, EyeLashesPrefabList.Count)]));
            CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Eye, Instantiate(EyePrefabList[Random.Range(0, EyePrefabList.Count)]));
            if (Random.Range(0, 10) < 8) CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Moustache, Instantiate(MoustachePrefabList[Random.Range(0, MoustachePrefabList.Count)]));
            if (Random.Range(0, 10) < 7) CharacterManager.CharacterFeatureManager.ChangeHeadFeature(CharacterHeadFeatures.Beard, Instantiate(BeardPrefabList[Random.Range(0, BeardPrefabList.Count)]));
            CharacterManager.CharacterFeatureManager.UpdateHeadFeatures();

            CharacterManager.CharacterFeatureManager.ChangeColor(CharacterColorFeatures.BodyColor, BodyColorList[Random.Range(0, BodyColorList.Count)]);
            CharacterManager.CharacterFeatureManager.ChangeColor(CharacterColorFeatures.HairColor, HairColor[Random.Range(0, HairColor.Count)]);
            CharacterManager.CharacterFeatureManager.ChangeColor(CharacterColorFeatures.EyeColor, EyeColor[Random.Range(0, EyeColor.Count)]);
        }
    }
}