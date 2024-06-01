using System.Collections.Generic;

public abstract class BaseObjectPool<T>
{
    public List<T> AvailableItems { get; private set;}
    public List<T> InUseItems { get; private set; }

    protected BaseObjectPool(List<T> availableItems)
    {
        AvailableItems = availableItems;
        InUseItems = new();
    }

    public abstract T GetObject();
    public abstract void ReleaseObject(T item);
}