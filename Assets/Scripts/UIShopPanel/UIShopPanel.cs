using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShopPanel : MonoBehaviour
{
    public GameObject Shop3DView;

    public void Shop_Show(TMP_Text ButtonText)
    {
        ButtonText.color = new Color(0.9882353f, 0.8666667f, 0.3568628f);
        transform.GetChild(0).gameObject.SetActive(true);
        Shop3DView.SetActive(true);
    }

    public void Shop_Hide(TMP_Text ButtonText)
    {
        ButtonText.color = Color.white;
        transform.GetChild(0).gameObject.SetActive(false);
        Shop3DView.SetActive(false);
    }
}
