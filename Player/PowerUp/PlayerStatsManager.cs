using System.Collections.Generic;

public class PlayerStatsManager : BaseStatsManager
{

    public PlayerController PlayerController { get; private set; }

    public PlayerStatsManager(PlayerController playerController) 
        : base("PlayerBaseStats", "PlayerStat")
        {
            PlayerController = playerController;

            InitializePowerUps();

            PlayerController.HealthSystem.SetMaxHealth(BaseStats[StatsEnum.MaxHealth]);
            PlayerController.HealthSystem.SetActualHealth(BaseStats[StatsEnum.ActualHealth]);

            PlayerController.WeaponSystem.SetDamage(BaseStats[StatsEnum.MeleDamage]);
        }

    private void InitializePowerUps()
    {
        List<BasePowerUp> basePowerUps = new List<BasePowerUp> {
            new HealthQuantityPowerUp(this, PlayerController.HealthSystem),
            new HealthPowerUp(this, PlayerController.HealthSystem),
            new SpeedPowerUp(this), 
            new MeleDamagePowerUp(this, PlayerController.WeaponSystem),
            new LongRangeAttackSpeedPowerUp(this)
        };

        SetPowerUps(basePowerUps);
    }
}
