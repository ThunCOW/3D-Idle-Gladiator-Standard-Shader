using TMPro;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    public TMP_Text Health_Text;
    public Color Health_ColorLow;
    private Color Health_ColorDef;

    public TMP_Text Armour_Text;
    private Color Armour_ColorDef;
    public Color Armour_ColorLow;


    public UIStatusMeshMask HealthMask;
    public UIStatusMeshMask ArmourMask;

    private void Awake()
    {
        Health_ColorDef = Health_Text.color;

        Armour_ColorDef = Armour_Text.color;
    }

    public void UpdateHealthUI(int BaseValue, int CurrentValue)
    {
        Health_Text.text = BaseValue.ToString() + " / " + CurrentValue.ToString();

        Health_Text.color = Color.Lerp(Health_ColorLow, Health_ColorDef, CurrentValue / (float)BaseValue);

        HealthMask.UpdateMask(1, CurrentValue / (float)BaseValue);
    }

    public void UpdateArmourUI(int BaseValue, int CurrentValue)
    {
        Armour_Text.text = BaseValue.ToString() + " / " + CurrentValue.ToString();

        ArmourMask.UpdateMask(1, CurrentValue / (float)BaseValue);
    }
}
