using System.Collections.Generic;

public class StaticPool<T>: BaseObjectPool<T>
{
    public StaticPool(List<T> availableItems) 
        : base(availableItems) {}

    // Returns a available object from the pool
    public override T GetObject()  
    {
        if(AvailableItems.Count <= 0) 
            throw new EmptyPoolException("No available items left in the object pool");

        T item = AvailableItems[0];
        AvailableItems.Remove(item);
        InUseItems.Add(item);

        return item;
    }

    // Stores a active object again into the pool
    public override void ReleaseObject(T item)
    {
        if(!InUseItems.Contains(item))
            throw new InvalidPoolObjectException("The provided object dosn't belongs to the pool");

        AvailableItems.Add(item);
        InUseItems.Remove(item);
    }
}