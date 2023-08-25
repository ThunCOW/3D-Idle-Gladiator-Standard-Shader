using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEquipmentTooltip : MonoBehaviour
{
    public static UIEquipmentTooltip Instance;

    private void Start()
    {
        Instance = this;
    }

    [SerializeField] TMP_Text itemNameText;
    [SerializeField] TMP_Text itemRequirementText;
    [SerializeField] TMP_Text itemDescriptionText;
    [SerializeField] TMP_Text itemValue;

    [SerializeField] GameObject MainView;
    [SerializeField] GameObject SelectedView;

    public void ShowItemTooltip(ItemScriptableObject SelectedItem, ItemScriptableObject EquippedItem)
    {
        MainView.SetActive(false);
        SelectedView.SetActive(true);

        itemNameText.text = SelectedItem.name;
        itemRequirementText.text = "- Required Gladiator Level " + SelectedItem.RequiredLevel + " -";
        itemDescriptionText.text = SelectedItem.GetDescriptionComparison(EquippedItem);
        itemValue.text = SelectedItem.Value + " gold";
    }

    public void HideItemTooltip()
    {
        MainView.SetActive(true);
        SelectedView.SetActive(false);
    }
}