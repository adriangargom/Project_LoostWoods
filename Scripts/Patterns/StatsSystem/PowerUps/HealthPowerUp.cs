
public class HealthPowerUp : BasePowerUp
{
    private readonly HealthSystem _healthSystem;
    private const int Increment = 50;

    public HealthPowerUp(BaseStatsManager baseStatsManager, HealthSystem healthSystem)
        : base(baseStatsManager)
        {
            _healthSystem = healthSystem;
        }

    public override void UpdatePowerUp()
    {
        _healthSystem.IncreaseActualHealth(Increment);
    }
}