
public class HealthQuantityPowerUp : BasePowerUp
{
    private readonly HealthSystem _healthSystem;
    private const int Increment = 50;

    public HealthQuantityPowerUp(BaseStatsManager baseStatsManager, HealthSystem healthSystem)
        : base(baseStatsManager) 
        {
            _healthSystem = healthSystem;
        }

    public override void UpdatePowerUp()
    {
        _healthSystem.IncreaseMaxHealth(Increment);
        BaseStatsManager.ActualStats[StatsEnum.MaxHealth] = _healthSystem.MaxHealthQuantity;
    }
}