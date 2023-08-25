using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopIconSelector : MonoBehaviour
{
    private List<ShopItemRef> ShopItems = new List<ShopItemRef>();

    [Header("******** Icons *******")]
    public float ScaleBy;

    public RectTransform[] Icons;
    private List<Vector3> originalPos = new List<Vector3>();
    private List<Vector3> originalScale = new List<Vector3>();

    private RectTransform lastSelected;

    [SerializeField]
    private GameObject SelectedItemClose;
    private EventTrigger SelectedItemCloseET;

    private void Start()
    {
        SelectedItemCloseET = SelectedItemClose.GetComponent<EventTrigger>();

        for (int i = 0; i < Icons.Length; i++)
        {
            originalPos.Add(Icons[i].anchoredPosition3D);
            originalScale.Add(Icons[i].localScale);
        }

        ShopItems.AddRange(GetComponentsInChildren<ShopItemRef>());
    }

    public void SelectIcon(RectTransform SelectedIcon)
    {
        SelectedItemCloseET.OnPointerUp(null);

        for (int i = 0; i < Icons.Length; i++)
        {
            Icons[i].anchoredPosition3D = originalPos[i];
            Icons[i].localScale = originalScale[i];
            ShopItems[i].DeSelect();
        }

        int order = System.Array.IndexOf(Icons, SelectedIcon);

        for (int i = 0; i < Icons.Length; i++)
        {
            if (i < order)
            {
                Icons[i].anchoredPosition3D = new Vector3(
                    Icons[i].anchoredPosition3D.x,
                    Icons[i].anchoredPosition3D.y + (Icons[i].rect.height * ScaleBy) / 2,
                    Icons[i].anchoredPosition3D.z
                    );
            }
            else if (i > order)
            {
                Icons[i].anchoredPosition3D = new Vector3(
                    Icons[i].anchoredPosition3D.x,
                    Icons[i].anchoredPosition3D.y - (Icons[i].rect.height * ScaleBy) / 2,
                    Icons[i].anchoredPosition3D.z
                    );
            }
            else if (i == order)
            {
                Icons[i].localScale = new Vector3(
                    Icons[i].localScale.x * (1 + ScaleBy),
                    Icons[i].localScale.y * (1 + ScaleBy),
                    Icons[i].localScale.z
                    );

                lastSelected = SelectedIcon;
                ShopItems[i].Select();
            }
        }
    }
}
