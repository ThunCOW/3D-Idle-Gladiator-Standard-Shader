using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGoldChange : MonoBehaviour
{
    [SerializeField] TMP_Text GoldText;

    private void Start()
    {
        PlayerCharacterManager.GoldChangeEvent += GoldChange;
    }

    private void GoldChange(int Amount)
    {
        GoldText.text = "Gold : " + Amount.ToString();
    }
}
