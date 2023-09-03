using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CharacterFeature
{
    public enum CharacterHeadFeatures
    {
        Hair,
        Eyebrow,
        EyeLash,
        Eye,
        Moustache,
        Beard,
    }

    public enum CharacterColorFeatures
    {
        BodyColor,
        HairColor,
        EyeColor,
    }

    public class CharacterFeatureManager : MonoBehaviour
    {
        public Transform BodyFeaturesParent;

        public Dictionary<CharacterHeadFeatures, GameObject> HeadFeaturesDict;
        public Dictionary<CharacterColorFeatures, List<GameObject>> CharacterColorablesDict;
        public Dictionary<CharacterColorFeatures, Color> ColorFeaturesDict;

        // ********* Private Fields *************
        CharacterManager characterManager;
        
        SkinnedMeshRenderer headSMR;

        private void Awake()
        {
            characterManager = GetComponent<CharacterManager>();

            headSMR = characterManager.BodyParts.Helmet.GetComponentInChildren<SkinnedMeshRenderer>();

            #region HeadFeaturesDict
            HeadFeaturesDict = new Dictionary<CharacterHeadFeatures, GameObject>();
            HeadFeaturesDict.Add(CharacterHeadFeatures.Hair, null);
            HeadFeaturesDict.Add(CharacterHeadFeatures.Eyebrow, null);
            HeadFeaturesDict.Add(CharacterHeadFeatures.EyeLash, null);
            HeadFeaturesDict.Add(CharacterHeadFeatures.Eye, null);
            HeadFeaturesDict.Add(CharacterHeadFeatures.Moustache, null);
            HeadFeaturesDict.Add(CharacterHeadFeatures.Beard, null);
            #endregion

            #region ColorFeaturesDict
            ColorFeaturesDict = new Dictionary<CharacterColorFeatures, Color>();
            ColorFeaturesDict.Add(CharacterColorFeatures.BodyColor, Color.white);
            ColorFeaturesDict.Add(CharacterColorFeatures.HairColor, Color.white);
            ColorFeaturesDict.Add(CharacterColorFeatures.EyeColor, Color.white);
            #endregion

            #region CharacterColorsDict
            CharacterColorablesDict = new Dictionary<CharacterColorFeatures, List<GameObject>>();
            
            CharacterColorablesDict.Add(CharacterColorFeatures.BodyColor, characterManager.BodyParts.BodyPartsDict.Values.ToList());

            List<GameObject> tempHairColor = new List<GameObject>();
            tempHairColor.Add(HeadFeaturesDict[CharacterHeadFeatures.Hair]);
            tempHairColor.Add(HeadFeaturesDict[CharacterHeadFeatures.Beard]);
            tempHairColor.Add(HeadFeaturesDict[CharacterHeadFeatures.Moustache]);
            tempHairColor.Add(HeadFeaturesDict[CharacterHeadFeatures.Eyebrow]);
            tempHairColor.Add(HeadFeaturesDict[CharacterHeadFeatures.EyeLash]);
            CharacterColorablesDict.Add(CharacterColorFeatures.HairColor, tempHairColor);

            List<GameObject> tempEyeColor = new List<GameObject>();
            tempEyeColor.Add(HeadFeaturesDict[CharacterHeadFeatures.Eye]);
            CharacterColorablesDict.Add(CharacterColorFeatures.EyeColor, tempEyeColor);

            #endregion
        }

        public void ChangeHeadFeature(CharacterHeadFeatures HeadFeature, GameObject NewFeature)
        {
            if (HeadFeaturesDict.TryGetValue(HeadFeature, out GameObject feature)) Destroy(feature);
            
            SkinnedMeshRenderer SMR = NewFeature.GetComponentInChildren<SkinnedMeshRenderer>();

            HeadFeaturesDict[HeadFeature] = SMR.gameObject;

            ItemScriptableObject.TransferSkinnedMeshes(SMR, headSMR, BodyFeaturesParent);
        }

        public void ChangeColor(CharacterColorFeatures CharacterColorFeature, Color NewColor)
        {
            foreach (GameObject go in CharacterColorablesDict[CharacterColorFeature])
            {
                go.GetComponent<SkinnedMeshRenderer>().material.color = NewColor;
            }
        }
    }
}