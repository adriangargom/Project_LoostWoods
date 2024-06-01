
public class SpeedPowerUp : BasePowerUp
{
    private const float Increment = 0.1f;

    public SpeedPowerUp(BaseStatsManager baseStatsManager)
        : base(baseStatsManager) {}

    public override void UpdatePowerUp()
    {
        float actualSpeed = BaseStatsManager.ActualStats[StatsEnum.Speed];
        float baseSpeed = BaseStatsManager.BaseStats[StatsEnum.Speed];
        float newActualSpeed = actualSpeed + baseSpeed * Increment;

        BaseStatsManager.ActualStats[StatsEnum.Speed] = (int)newActualSpeed;
    }
}