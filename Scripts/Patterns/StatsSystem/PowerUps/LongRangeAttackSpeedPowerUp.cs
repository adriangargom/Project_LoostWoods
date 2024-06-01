
public class LongRangeAttackSpeedPowerUp : BasePowerUp
{
    private const float Increment = 0.1f;

    public LongRangeAttackSpeedPowerUp(BaseStatsManager baseStatsManager)
        : base(baseStatsManager) {}

    public override void UpdatePowerUp()
    {
        float actualLongRangeProjectileForce = BaseStatsManager.ActualStats[StatsEnum.ProjectileForce];
        float baseLongRangeProjectileForce = BaseStatsManager.BaseStats[StatsEnum.ProjectileForce];
        float newActualLongRangeProjectileForce = actualLongRangeProjectileForce + baseLongRangeProjectileForce * Increment;

        BaseStatsManager.ActualStats[StatsEnum.ProjectileForce] = newActualLongRangeProjectileForce;
        BaseStatsManager.Notify();
    }
}