using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoreSellPoint : MonoBehaviour, IObservable
{
    public ShopController ShopController;
    public SellPointModel SellPointData = new();

    [SerializeField] private string _playerTag = "Player";
    protected readonly List<IObserver> ActualObservers = new();



    void Start() {
        SellPointData.Status = SellPointStatusEnum.NotInRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != _playerTag) return;

        SellPointData.Status = SellPointStatusEnum.Available;
        
        Notify();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag != _playerTag) return;

        InputManager inputManager;
        other.gameObject.TryGetComponent(out inputManager);

        PlayerController playerController;
        other.gameObject.TryGetComponent(out playerController);


        PlayerStatsManager playerStatsManager = playerController.BaseStatsManager as PlayerStatsManager;
        float actualPlayerCredits = playerStatsManager.ActualStats[StatsEnum.Credits];

        bool sufficientQuantity = SellPointData.Quantity > 0;
        bool sufficientCredits = actualPlayerCredits >= SellPointData.Price;

        if(inputManager.IsInteracting && sufficientQuantity && sufficientCredits) {
            UpgradePlayerPowerUp(playerController);
            Notify();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag != "Player") return;

        SellPointData.Status = SellPointStatusEnum.NotInRange;
        
        Notify();
    }


    // Method that upgrades the player power up based on thee sellPointPowerUp
    private void UpgradePlayerPowerUp(PlayerController playerController) {
        PlayerStatsManager playerStatsManager = playerController.BaseStatsManager as PlayerStatsManager;

        switch (SellPointData.PowerUp) {
            case PowerUpsEnum.HealthQuantity:
                playerStatsManager.UpgradePowerUp<HealthQuantityPowerUp>();
                break;

            case PowerUpsEnum.Health:
                playerStatsManager.UpgradePowerUp<HealthPowerUp>();
                break;

            case PowerUpsEnum.Speed:
                playerStatsManager.UpgradePowerUp<SpeedPowerUp>();
                break;

            case PowerUpsEnum.SwordDamage:
                playerStatsManager.UpgradePowerUp<MeleDamagePowerUp>();
                break;

            case PowerUpsEnum.BowDamage:
                playerStatsManager.UpgradePowerUp<LongRangeAttackSpeedPowerUp>();
                break;
        }

        SellPointData.Quantity -= 1;
        playerStatsManager.ActualStats[StatsEnum.Credits] -= SellPointData.Price;
        playerStatsManager.Notify();
    }



    public void Attach(IObserver observer)
    {
        ActualObservers.Add(observer);
        Notify();
    }

    public void Detach(IObserver observer)
    {
        ActualObservers.Remove(observer);
    }

    public void Notify() {
        foreach (IObserver observer in ActualObservers)
        {
            observer.ObserverUpdate();
        }
    }
}
