using TMPro;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    public TMP_Text Health_Text;
    public Color Health_ColorLow;
    private Color Health_ColorDef;

    public TMP_Text Armour_Text;
    public Color Armour_ColorLow;
    private Color Armour_ColorDef;


    public UIStatusMeshMask HealthMask;
    public UIStatusMeshMask ArmourMask;

    [HideInInspector] 
    public CharacterManager characterManager;

    private void Awake()
    {
        Health_ColorDef = Health_Text.color;

        Armour_ColorDef = Armour_Text.color;
    }

    private void Start()
    {
        
    }

    public void UpdateHealthUI()
    {
        Health_Text.text = characterManager.Status.Health.CurrentValue.ToString() + " / " + characterManager.Status.Health.GetValue().ToString();

        Health_Text.color = Color.Lerp(Health_ColorLow, Health_ColorDef, characterManager.Status.Health.CurrentValue / (float)characterManager.Status.Health.GetValue());

        HealthMask.UpdateMask(1, characterManager.Status.Health.CurrentValue / (float)characterManager.Status.Health.GetValue());
    }
    public void UpdateArmourUI()
    {
        Armour_Text.text = characterManager.Status.Armour.CurrentValue.ToString() + " / " + characterManager.Status.Armour.GetValue().ToString();

        ArmourMask.UpdateMask(1, characterManager.Status.Armour.CurrentValue / (float)characterManager.Status.Armour.GetValue());
    }

    public void UpdateHealthUI(int BaseValue, int CurrentValue)
    {
        Health_Text.text = CurrentValue.ToString() + " / " + BaseValue.ToString();

        Health_Text.color = Color.Lerp(Health_ColorLow, Health_ColorDef, CurrentValue / (float)BaseValue);

        HealthMask.UpdateMask(1, CurrentValue / (float)BaseValue);
    }

    public void UpdateArmourUI(int BaseValue, int CurrentValue)
    {
        Armour_Text.text = CurrentValue.ToString() + " / " + BaseValue.ToString();

        ArmourMask.UpdateMask(1, CurrentValue / (float)BaseValue);
    }
}
