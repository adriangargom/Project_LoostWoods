using System;
using System.Collections.Generic;

public class SpikeStatsManager: BaseStatsManager
{
    public EnemyController SpikeController { get; private set; }


    public SpikeStatsManager(EnemyController spikeController)
        : base("SpikeBaseStats", "SpikeStat")
        {
            SpikeController = spikeController;

            InitializePowerUps();

            SpikeController.HealthSystem.SetMaxHealth(BaseStats[StatsEnum.MaxHealth]);
            SpikeController.HealthSystem.SetActualHealth(BaseStats[StatsEnum.ActualHealth]);

            SpikeController.WeaponSystem.SetDamage(BaseStats[StatsEnum.MeleDamage]);
            SpikeController.EnviromentDetection.SetDetectionRange(BaseStats[StatsEnum.DetectionRange]);
        }

    private void InitializePowerUps()
    {
        List<BasePowerUp> basePowerUps = new List<BasePowerUp> {
            new HealthQuantityPowerUp(this, SpikeController.HealthSystem),
            new HealthPowerUp(this, SpikeController.HealthSystem),
            new MeleDamagePowerUp(this, SpikeController.WeaponSystem),
            new LongRangeAttackSpeedPowerUp(this)
        };

        SetPowerUps(basePowerUps);
    }

    public void UpgradeEnemy()
    {
        UpgradeAllPowerUps();

        SpikeController.HealthSystem.SetMaxHealth(BaseStats[StatsEnum.MaxHealth]);
        SpikeController.HealthSystem.SetActualHealth(BaseStats[StatsEnum.ActualHealth]);

        SpikeController.WeaponSystem.SetDamage(BaseStats[StatsEnum.MeleDamage]);
    }
}