using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class ShopController : MonoBehaviour, IObservable
{
    [field: Header("Shop Manager Attributes")]
    public StoreSellPoint SelectedSellPoint { get; private set; }

    [SerializeField] private List<PowerUpsEnum> _availablePowerUps;
    [SerializeField] private StoreSellPoint[] _storeSellPoints;
    [SerializeField] private List<PowerUpsEnum> _selectedPowerUps = new();

    private XmlNodeList _xmlNodeList;
    private List<IObserver> actualObservers = new();


    
    void Start() {
        _availablePowerUps = Enum.GetValues(typeof(PowerUpsEnum)).Cast<PowerUpsEnum>().ToList();
        _xmlNodeList = XmlLoader.LoadPlayerPowerUps();

        GenerateRandomPowerUps();
    }



    // Generates three random power ups that are going to be assigned 
    // to each one of the ShopSellPoints 
    public void GenerateRandomPowerUps() {
        _selectedPowerUps.Clear();

        for (int i = 0; i < _storeSellPoints.Length; i++) {
            int powerUpIndex = PickRandomPowerUp();
            PowerUpsEnum selectedPowerUp = _availablePowerUps[powerUpIndex];

            _selectedPowerUps.Add(selectedPowerUp);
            _storeSellPoints[i].SellPointData.SetSellPointAttributes(selectedPowerUp, _xmlNodeList[powerUpIndex]);
            _storeSellPoints[i].ShopController = this;
        }
    }

    // Method that picks a random non repeated power up from the player 
    // available power ups, in a recursive
    private int PickRandomPowerUp()
    {
        int randomIndex = UnityEngine.Random.Range(0, _availablePowerUps.Count);
        
        bool isPowerUpSelected = _selectedPowerUps.Contains(_availablePowerUps[randomIndex]);
        return isPowerUpSelected ? PickRandomPowerUp(): randomIndex;
    }


    // IObservable
    public void Attach(IObserver observer)
    {
        actualObservers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        actualObservers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver observer in actualObservers) {
            observer.ObserverUpdate();
        }
    }
}
