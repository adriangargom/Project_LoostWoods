using System.Collections.Generic;
using UnityEngine;

public class RoomExitDoor : MonoBehaviour, IObservable
{
    private readonly List<IObserver> ActualObservers = new();


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Notify();
        }
    }

    
    public void Attach(IObserver observer)
    {
        ActualObservers.Add(observer);
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