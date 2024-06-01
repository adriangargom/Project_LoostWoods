
public class MeleDamagePowerUp : BasePowerUp
{
    private readonly WeaponSystem _weaponSystem;
    private const float _increment = .1f;

    public MeleDamagePowerUp(BaseStatsManager baseStatsManager, WeaponSystem weaponSystem)
        : base(baseStatsManager) {
            _weaponSystem = weaponSystem;
        }

    public override void UpdatePowerUp()
    {
        float actualMeleDamage = BaseStatsManager.ActualStats[StatsEnum.MeleDamage];
        float baseMeleDamage = BaseStatsManager.BaseStats[StatsEnum.MeleDamage];
        float newActualMeleDamage = actualMeleDamage + baseMeleDamage * _increment;

        BaseStatsManager.ActualStats[StatsEnum.MeleDamage] = newActualMeleDamage;
        _weaponSystem.SetDamage(newActualMeleDamage);
    }
}