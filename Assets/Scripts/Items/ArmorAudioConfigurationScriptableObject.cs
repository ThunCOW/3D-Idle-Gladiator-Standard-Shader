using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Config", menuName = "Audio Menu/New Armor Audio Config")]
public class ArmorAudioConfigurationScriptableObject : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume = 1f;
    public AudioClip[] ArmorAudioVariations;

    public void PlayArmorSound(AudioSource AudioSource)
    {
        if (Random.Range(0, 10) == 0)
        {
            AudioSource.pitch = 1 + Random.Range(-0.15f, 0.15f);
        }
        else
            AudioSource.pitch = 1;

        AudioSource.PlayOneShot(ArmorAudioVariations[Random.Range(0, ArmorAudioVariations.Length)], volume);
    }
}
