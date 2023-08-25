public interface IDamageable
{
    public delegate void TakeDamageEvent(HitRegion HitRegion);
    public event TakeDamageEvent OnTakeDamage;

    public delegate void DeathEvent();
    public event DeathEvent OnDeath;

    public void TakeDamage(int Damage, HitRegion HitRegion, WeaponScriptableObject WeaponSO);
}
