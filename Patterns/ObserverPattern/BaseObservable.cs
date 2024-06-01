using System.Collections.Generic;

public abstract class BaseObservable 
{
    protected readonly List<IObserver> ActualObservers = new();

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