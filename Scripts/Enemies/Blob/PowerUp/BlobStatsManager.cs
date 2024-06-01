using System.Collections.Generic;

public class BlobStatsManager: BaseStatsManager
{
    public EnemyController BlobController { get; private set; }

    public BlobStatsManager(EnemyController blobController)
        : base("BlobBaseStats", "BlobStat")
        {
            BlobController = blobController;

            InitializePowerUps();

            BlobController.HealthSystem.SetMaxHealth(BaseStats[StatsEnum.MaxHealth]);
            BlobController.HealthSystem.SetActualHealth(BaseStats[StatsEnum.ActualHealth]);

            BlobController.WeaponSystem.SetDamage(BaseStats[StatsEnum.MeleDamage]);

            BlobController.EnviromentDetection.SetDetectionRange(BaseStats[StatsEnum.DetectionRange]);
        }

    private void InitializePowerUps()
    {
        List<BasePowerUp> basePowerUps = new List<BasePowerUp> {
            new HealthQuantityPowerUp(this, BlobController.HealthSystem),
            new HealthPowerUp(this, BlobController.HealthSystem),
            new MeleDamagePowerUp(this, BlobController.WeaponSystem)
        };

        SetPowerUps(basePowerUps);
    }

    public void UpgradeEnemy()
    {
        UpgradeAllPowerUps();

        BlobController.HealthSystem.SetMaxHealth(BaseStats[StatsEnum.MaxHealth]);
        BlobController.HealthSystem.SetActualHealth(BaseStats[StatsEnum.ActualHealth]);

        BlobController.WeaponSystem.SetDamage(BaseStats[StatsEnum.MeleDamage]);
    }
}