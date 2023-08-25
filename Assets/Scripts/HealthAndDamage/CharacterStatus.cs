using UnityEngine;

public class CharacterStatus : MonoBehaviour, IDamageable
{
    private CharacterManager character;

    public FlexibleStat Armour;
    public FlexibleDerivativeStat Health;
    public FlexibleDerivativeStat Morale;
    public FlexibleDerivativeStat Fatigue;

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    private void Start()
    {
        character = GetComponent<CharacterManager>();

        Health.DerivateFrom(character.Attributes.PrimaryStatsDict[PrimaryStats.Vitality]);
        character.Status.Health.CurrentValue = character.Status.Health.GetValue();
        character.StatusUI.UpdateHealthUI((int)character.Status.Health.GetValue(), (int)character.Status.Health.CurrentValue);
    }

    public void TakeDamage(int Damage, HitRegion HitRegion, WeaponScriptableObject WeaponSO)
    {
        PlayTakeDamageSound(WeaponSO);
        PlayHurtAnimation(HitRegion);
        //SpawnBlood();

        float damageTaken = Mathf.Clamp(Damage, 0, Health.CurrentValue);

        float newHealth = Mathf.Clamp(Health.CurrentValue - Damage, 0, Health.CurrentValue);

        Health.CurrentValue = newHealth;

        if (Health.CurrentValue == 0 && damageTaken != 0)
        {
            OnDeath?.Invoke();
            return;
        }

        character.StatusUI.UpdateHealthUI((int)Health.GetValue(), (int)Health.CurrentValue);
        character.StatusUI.UpdateArmourUI((int)Armour.GetValue(), (int)Armour.CurrentValue);

        if (Health.CurrentValue != 0)
        {
            OnTakeDamage?.Invoke(HitRegion);
        }
    }

    private void PlayTakeDamageSound(WeaponScriptableObject WeaponSO)
    {
        if (Armour.CurrentValue > 0)
            WeaponSO.WeaponAudioConfigSO.PlayHitSound(character.AudioSource);
        else
        {
            // TODO : go through equipped armors to select a random piece and play the hit sound based on random equipped pieces
            ArmorScriptableObject ArmorSO = character.EquipmentManager.EquippedItemsDict[EquipmentType.Breatplate] as ArmorScriptableObject;
            ArmorSO.ArmorAudioConfigSO.PlayArmorSound(character.AudioSource);
        }
    }

    private void PlayHurtAnimation(HitRegion HitRegion)
    {
        switch (HitRegion)
        {
            case HitRegion.High:
                character.Animator.SetTrigger("Hurt");
                break;
            case HitRegion.Mid:
                character.Animator.SetTrigger("Hurt");
                break;
            case HitRegion.Low:
                character.Animator.SetTrigger("Hurt");
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