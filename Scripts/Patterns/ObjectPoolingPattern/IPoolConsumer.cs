
public interface IPoolConsumer<T>
{
    public T GetItem();
    public void StoreItem(T item);
}