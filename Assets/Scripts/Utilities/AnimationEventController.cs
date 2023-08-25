using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    public delegate void OnDamage(HitRegion HitRegion);
    public event OnDamage Damage;

    public void DamageEvent(HitRegion HitRegion)
    {
        Damage?.Invoke(HitRegion);
    }
}
