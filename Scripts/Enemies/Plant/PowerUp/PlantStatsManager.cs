using System.Collections.Generic;

public class PlantStatsManager: BaseStatsManager
{
    public EnemyController PlantController { get; private set; }

    public PlantStatsManager(EnemyController plantController)
        : base("PlantBaseStats", "PlantStat")
        {
            PlantController = plantController;

            InitializePowerUps();

            PlantController.HealthSystem.SetMaxHealth(BaseStats[StatsEnum.MaxHealth]);
            PlantController.HealthSystem.SetActualHealth(BaseStats[StatsEnum.ActualHealth]);

            PlantController.WeaponSystem.SetDamage(BaseStats[StatsEnum.MeleDamage]);
            PlantController.EnviromentDetection.SetDetectionRange(BaseStats[StatsEnum.DetectionRange]);
        }

    private void InitializePowerUps()
    {
        List<BasePowerUp> basePowerUps = new List<BasePowerUp> {
            new HealthQuantityPowerUp(this, PlantController.HealthSystem),
            new HealthPowerUp(this, PlantController.HealthSystem),
            new MeleDamagePowerUp(this, PlantController.WeaponSystem),
            new LongRangeAttackSpeedPowerUp(this)
        };

        SetPowerUps(basePowerUps);
    }

    public void UpgradeEnemy()
    {
        UpgradeAllPowerUps();

        PlantController.HealthSystem.SetMaxHealth(BaseStats[StatsEnum.MaxHealth]);
        PlantController.HealthSystem.SetActualHealth(BaseStats[StatsEnum.ActualHealth]);

        PlantController.WeaponSystem.SetDamage(BaseStats[StatsEnum.MeleDamage]);
    }
}