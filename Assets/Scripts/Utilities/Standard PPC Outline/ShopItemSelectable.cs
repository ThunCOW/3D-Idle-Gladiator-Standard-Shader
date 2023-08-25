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

        private void Start()
        {
            defPos = transform.position;    
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
            UIEquipmentTooltip.Instance.ShowItemTooltip(ShopItem, BattleManager.Characters[Gladiator.Player].EquipmentManager.EquippedItemsDict[ShopItem.Type]);
        }

        public void DeSelectObject()
        {
            ExitObject();
            transform.position = defPos;
        }
    }
}