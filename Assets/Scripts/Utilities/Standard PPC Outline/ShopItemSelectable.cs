using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PostProcessingOutline
{
    public class ShopItemSelectable : MonoBehaviour
    {
        public ItemScriptableObject ShopItem;

        private Vector3 defPos;

        private UIEquipmentTooltip uiSelectedTooltip;

        private MeshRenderer mr;

        private Color lockedColor = new Color(49f, 49f, 49f);
        private void Awake()
        {
            uiSelectedTooltip = GetComponentInParent<UIItemSelectionRaycaster>().UISelectedTooltip;

            defPos = transform.parent.position;

            mr = GetComponentInParent<MeshRenderer>();
        }

        private void Start()
        {
            if (BattleManager.Characters[Gladiator.Player].Attributes.LVL.LVL + 1 < ShopItem.RequiredLevel)
                LockedItemView();
            else
                UnlockedItemView();

            BattleManager.Characters[Gladiator.Player].Attributes.LVL.OnLevelIncreased += OnPlayerLVLChanged;    
        }

        private void OnPlayerLVLChanged()
        {
            if (BattleManager.Characters[Gladiator.Player].Attributes.LVL.LVL + 1 >= ShopItem.RequiredLevel)
                UnlockedItemView();
        }

        private void LockedItemView()
        {
            for (int i = 0; i < mr.materials.Length; i++)
            {
                mr.materials[i].color = lockedColor;
                mr.materials[i].SetFloat("_Metallic", 1);
                mr.materials[i].SetFloat("_Glossiness", 0);
            }
        }

        private void UnlockedItemView()
        {
            for (int i = 0; i < mr.materials.Length; i++)
            {
                mr.materials[i].color = ShopItem.ModelPrefab.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterials[i].color;
            }

            for (int i = 0; i < mr.materials.Length; i++)
            {
                mr.materials[i].SetFloat("_Metallic", 0);
                mr.materials[i].SetFloat("_Glossiness", 0.5f);
            }
        }

        public void EnterObject()
        {
            gameObject.layer = LayerMask.NameToLayer("OutlinePostProcessing");
        }

        public void ExitObject()
        {
            gameObject.layer = LayerMask.NameToLayer("UI");
        }

        public void SelectObject()
        {
            EnterObject();
            uiSelectedTooltip.ShowItemTooltip(ShopItem, BattleManager.Characters[Gladiator.Player].EquipmentManager.EquippedItemsDict[ShopItem.Type]);
        }

        public void DeSelectObject()
        {
            ExitObject();
            transform.parent.position = defPos;
        }
    }
}