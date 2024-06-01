using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IObservable
{
    [field: SerializeField] public HealthSystemStatesEnum CurrentState { get; private set; }
    [field: SerializeField] public float MaxHealthQuantity { get; private set; }
    [field: SerializeField] public float ActualHealth { get; private set; }
    [field: SerializeField] public bool IsInmune { get; set; }
    private readonly List<IObserver> _actualObservers = new();



    public void SetMaxHealth(float value)
    {
        MaxHealthQuantity = value;
        Notify();
    }

    public void IncreaseMaxHealth(float increment) {
        MaxHealthQuantity += increment;
        CurrentState = HealthSystemStatesEnum.MaxHealthIncrease;
        Notify();
    }

    public void DecreaseMaxHealth(float decrement)
    {
        MaxHealthQuantity -= decrement;
        CurrentState = HealthSystemStatesEnum.MaxHealthDecrease;
        Notify();
    }

    public void SetActualHealth(float value)
    {
        ActualHealth = Mathf.Clamp(value, 0, MaxHealthQuantity);
        Notify();
    }

    public void IncreaseActualHealth(float increment)
    {
        float totalHealth = ActualHealth + increment;
        ActualHealth = Mathf.Clamp(totalHealth, 0, MaxHealthQuantity);
        CurrentState = HealthSystemStatesEnum.ActualHealthIncrease;
        Notify();
    }

    public void DecreaseActualHealth(float decrement)
    {
        float totalHealth = ActualHealth - decrement;
        ActualHealth = Mathf.Clamp(totalHealth, 0, MaxHealthQuantity);
        CurrentState = HealthSystemStatesEnum.ActualHealthDecrease;
        Notify();
    }

    public void EnableInmunity()
    {
        IsInmune = true;
    }

    public void DisableInmunity()
    {
        IsInmune = false;
    }


    // IObservable
    public void Attach(IObserver observer)
    {
        _actualObservers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _actualObservers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver observer in _actualObservers)
        {
            observer.ObserverUpdate();
        }
    }
}