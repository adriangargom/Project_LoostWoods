
public abstract class BasePowerUp
{
    protected BaseStatsManager BaseStatsManager;

    public BasePowerUp(BaseStatsManager baseStatsManager) {
        BaseStatsManager = baseStatsManager;
    }

    public abstract void UpdatePowerUp();
}