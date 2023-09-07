using UnityEngine;

public class CharacterStatus : MonoBehaviour, IDamageable
{
    private CharacterManager characterManager;

    public FlexibleStat Armour;
    public FlexibleDerivativeStat Health;
    public FlexibleDerivativeStat Morale;
    public FlexibleDerivativeStat Fatigue;

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    private void Start()
    {
        characterManager = GetComponent<CharacterManager>();

        Health.CurrentValueChanged += characterManager.StatusUI.UpdateHealthUI;
        Armour.CurrentValueChanged += characterManager.StatusUI.UpdateArmourUI;

        Health.DerivateFrom(characterManager.Attributes.PrimaryStatsDict[PrimaryAttributes.Vitality]);
        Health.CurrentValue = characterManager.Status.Health.GetValue();
        //characterManager.StatusUI.UpdateHealthUI((int)characterManager.Status.Health.GetValue(), (int)characterManager.Status.Health.CurrentValue);

        characterManager.Attributes.PrimaryStatsDict[PrimaryAttributes.Vitality].AttributeChanged += characterManager.StatusUI.UpdateHealthUI;
        Armour.AttributeChanged += characterManager.StatusUI.UpdateArmourUI;

        BattleStatus.Instance.AssignEvents(characterManager);
        BattleStatPanel.Instance.AssignEvents(characterManager);
    }

    public void TakeDamage(int Damage, HitRegion HitRegion, WeaponScriptableObject WeaponSO)
    {
        PlayTakeDamageSound(WeaponSO);
        //SpawnBlood();

        float damageTaken = Mathf.Clamp(Damage, 0, Health.CurrentValue);

        // Armour Damage
        if (Armour.CurrentValue != 0)
        {
            float newArmour = Armour.CurrentValue - Damage;

            Armour.CurrentValue = newArmour;

            // Armour Broke
            if (newArmour < 0)
            {
                // Takes half the damage after armour broke
                float newHealth = Mathf.Clamp(Health.CurrentValue - (newArmour * -1 / 2), 0, Health.CurrentValue);

                Health.CurrentValue = newHealth;
            }
            // Armour Takes All
            else
            {
                // Even if Armour takes damage, weapon still damages the character up to X % of HP
                float percToHp = Random.Range(0, 25) / 100f;
                float newHealth = Mathf.Clamp(Health.CurrentValue - (int)(percToHp * Damage), 0, Health.CurrentValue);  

                Health.CurrentValue = newHealth;
            }
        }
        // Health Damage
        else
        {
            float newHealth = Mathf.Clamp(Health.CurrentValue - Damage, 0, Health.CurrentValue);

            Health.CurrentValue = newHealth;
        }

        characterManager.StatusUI.UpdateHealthUI((int)Health.GetValue(), (int)Health.CurrentValue);
        //characterManager.StatusUI.UpdateArmourUI((int)Armour.GetValue(), (int)Armour.CurrentValue);

        if (Health.CurrentValue <= 0.9999f && damageTaken != 0)
        {
            OnDeath?.Invoke();
            characterManager.CharacterActionManager.BloodSpawner.RagdollHit();
            return;
        }

        if (Health.CurrentValue != 0)
        {
            OnTakeDamage?.Invoke(HitRegion);
            PlayHurtAnimation(HitRegion);
        }
    }

    private void PlayTakeDamageSound(WeaponScriptableObject WeaponSO)
    {
        if (Armour.CurrentValue >= 0)
            WeaponSO.WeaponAudioConfigSO.PlayHitSound(characterManager.AudioSource);
        else
        {
            // TODO : go through equipped armors to select a random piece and play the hit sound based on random equipped pieces
            ArmorScriptableObject ArmorSO = characterManager.EquipmentManager.EquippedItemsDict[EquipmentType.Breatplate] as ArmorScriptableObject;
            ArmorSO.ArmorAudioConfigSO.PlayArmorSound(characterManager.AudioSource);
        }
    }

    private void PlayHurtAnimation(HitRegion HitRegion)
    {
        switch (HitRegion)
        {
            case HitRegion.High:
                characterManager.Animator.SetTrigger("Hurt");
                break;
            case HitRegion.Mid:
                characterManager.Animator.SetTrigger("Hurt");
                break;
            case HitRegion.Low:
                characterManager.Animator.SetTrigger("Hurt");
                break;
        }
    }



    /*public void SpawnBlood()
    {
        // var randRotation = new Vector3(0, Random.value * 360f, 0);
        // var dir = CalculateAngle(Vector3.forward, hit.normal);
        float angle = Mathf.Atan2(hit.normal.x, hit.normal.z) * Mathf.Rad2Deg + 180;

        //var effectIdx = Random.Range(0, BloodFX.Length);
        if (effectIdx == BloodFX.Length) effectIdx = 0;

        var instance = Instantiate(BloodFX[effectIdx], hit.point, Quaternion.Euler(0, angle + 90, 0));
        effectIdx++;

        var settings = instance.GetComponent<BFX_BloodSettings>();
        //settings.FreezeDecalDisappearance = InfiniteDecal;
        settings.LightIntensityMultiplier = DirLight.intensity;

        var nearestBone = GetNearestObject(hit.transform.root, hit.point);
        if (nearestBone != null)
        {
            var attachBloodInstance = Instantiate(BloodAttach);
            var bloodT = attachBloodInstance.transform;
            bloodT.position = hit.point;
            bloodT.localRotation = Quaternion.identity;
            bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
            bloodT.LookAt(hit.point + hit.normal, direction);
            bloodT.Rotate(90, 0, 0);
            bloodT.transform.parent = nearestBone;
            //Destroy(attachBloodInstance, 20);
        }

        // if (!InfiniteDecal) Destroy(instance, 20);
    }*/
}