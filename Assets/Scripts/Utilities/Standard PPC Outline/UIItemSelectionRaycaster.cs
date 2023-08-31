using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PostProcessingOutline
{
    public class UIItemSelectionRaycaster : MonoBehaviour
    {
        public Camera MainCamera;
        public Camera RenderToTextureCamera;

        public RectTransform RawImageRectTrans;

        [Space]
        public GameObject SelectedObjectPosition;

        [Space]
        public UIEquipmentTooltip UISelectedTooltip;
        
        private ShopItemSelectable selectedItem;
        private ShopItemSelectable lastHoverItem;
        private void Start()
        {
            MainCamera = Camera.main;
        }

        private void Update()
        {
            HoverMouse();
            if (Input.GetMouseButtonUp(0))
            {
                if (selectedItem != null)
                    return;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(RawImageRectTrans, Input.mousePosition, null, out Vector2 localPoint);
                Vector2 normalizedPoint = Rect.PointToNormalized(RawImageRectTrans.rect, localPoint);

                RaycastHit hit;
                Ray ray = RenderToTextureCamera.ViewportPointToRay(normalizedPoint);
                //Ray ray = RenderToTextureCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;

                    objectHit.TryGetComponent(out ShopItemSelectable shopItem);
                    // Hit an item
                    if (shopItem != null)
                    {
                        shopItem.SelectObject();
                        shopItem.transform.parent.position = SelectedObjectPosition.transform.position;
                        selectedItem = shopItem;
                        lastHoverItem = null;
                    }
                }
            }
        }

        private void HoverMouse()
        {
            if (selectedItem != null)
                return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(RawImageRectTrans, Input.mousePosition, null, out Vector2 localPoint);
            Vector2 normalizedPoint = Rect.PointToNormalized(RawImageRectTrans.rect, localPoint);

            RaycastHit hit;
            Ray ray = RenderToTextureCamera.ViewportPointToRay(normalizedPoint);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                objectHit.TryGetComponent(out ShopItemSelectable shopItem);
                // Hit an item
                if (shopItem != null)
                {
                    if (shopItem == selectedItem)
                        return;

                    if (lastHoverItem == null)
                    {
                        shopItem.EnterObject();
                        lastHoverItem = shopItem;
                        return;
                    }

                    if (shopItem.gameObject == lastHoverItem.gameObject)
                    {
                        // Do Nothing
                    }
                }
            }
            else
            {
                if (lastHoverItem != null)
                {
                    lastHoverItem.ExitObject();
                    lastHoverItem = null;
                }
            }
        }

        public void DeSelectObject()
        {
            if (selectedItem != null)
                selectedItem.DeSelectObject();

            StartCoroutine(FixSelectBug());
        }

        IEnumerator FixSelectBug()
        {
            yield return new WaitForFixedUpdate();
            selectedItem = null;

            // When shop closes the selectedItem becomes null and causes in Update function to not return which causes it to re-enter to selected view in same frame
        }

        public void BuyItem()
        {
            if (selectedItem.ShopItem.Value <= PlayerCharacterManager.PlayerGold)
            {
                PlayerCharacterManager.PlayerGold -= selectedItem.ShopItem.Value;
            }

            BattleManager.Characters[Gladiator.Player].EquipmentManager.EquipItem(selectedItem.ShopItem);
        }
    }
}