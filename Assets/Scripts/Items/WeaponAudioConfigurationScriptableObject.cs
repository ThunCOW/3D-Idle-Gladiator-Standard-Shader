using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Audio Config", menuName = "Audio Menu/New Weapon Audio Config")]
public class WeaponAudioConfigurationScriptableObject : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume = 1f;
    public AudioClip[] WeaponHitAudioVariations;
    public AudioClip[] WeaponMissAudioVariations;
    public AudioClip[] WeaponBlockAudioVariations;

    public void PlayHitSound(AudioSource AudioSource)
    {
        if (Random.Range(0, 10) == 0)
        {
            AudioSource.pitch = 1 + Random.Range(-0.15f, 0.15f);
        }
        else
            AudioSource.pitch = 1;

        AudioSource.PlayOneShot(WeaponHitAudioVariations[Random.Range(0, WeaponHitAudioVariations.Length)], volume);
    }

    public void PlayMissSound(AudioSource AudioSource)
    {
        if (Random.Range(0, 10) == 0)
        {
            AudioSource.pitch = 1 + Random.Range(-0.15f, 0.15f);
        }
        else
            AudioSource.pitch = 1;

        AudioSource.PlayOneShot(WeaponHitAudioVariations[Random.Range(0, WeaponHitAudioVariations.Length)], volume);
    }

    public void PlayBlockSound(AudioSource AudioSource)
    {
        if (Random.Range(0, 10) == 0)
        {
            AudioSource.pitch = 1 + Random.Range(-0.15f, 0.15f);
        }
        else
            AudioSource.pitch = 1;

        AudioSource.PlayOneShot(WeaponBlockAudioVariations[Random.Range(0, WeaponBlockAudioVariations.Length)], volume);
    }
}