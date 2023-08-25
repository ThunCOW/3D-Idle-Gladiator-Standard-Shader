using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemRef : MonoBehaviour, IPointerUpHandler
{
    public GameObject ItemsParent;

    private RectTransform rt;

    private ShopIconSelector shopIconSelector;

    private void Start()
    {
        rt = GetComponent<RectTransform>();

        shopIconSelector = GetComponentInParent<ShopIconSelector>();
    }

    public void Select()
    {
        ItemsParent.SetActive(true);
    }

    public void DeSelect()
    {
        ItemsParent.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        shopIconSelector.SelectIcon(rt);
    }
}
