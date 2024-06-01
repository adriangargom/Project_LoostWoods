using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSystem : MonoBehaviour, IObservable, IObserver
{    
    [field: Header("Room Attributes")]
    [field: SerializeField] public RoomTypeEnum RoomType { get; private set; }
    [field: SerializeField] public ActualRoomManager RoomManager { get; private set; }
    [field: SerializeField] public Transform PlayerEntryDoor { get; private set; }
    [field: SerializeField] public RoomExitDoor PlayerExitDoor { get; private set; }

    [field: Header("Enemies Generation Management")]
    private EnemiesPoolManager EnemiesPoolManager;
    [field: SerializeField] public List<GameObject> ActualEnemies { get; private set; }
    [field: SerializeField] public List<Transform> EnemySpawnPoints { get; private set; }
    private List<Transform> PickedEnemySpawnPoints;
    public int _enemyQuantity { get; private set; } 

    [field: Header("Items Generation Management")]
    private ItemsPoolManager ItemsPoolManager;
    [field: SerializeField] public List<GameObject> ActualItems { get; private set; }
    [field: SerializeField] public List<Transform> ItemsSpawnPoints { get; private set; }
    private List<Transform> PickedItemSpawnPoints;
    public int _itemsQuantity { get; private set; }
    
    private readonly List<IObserver> _actualObservers = new();

    void Awake()
    {
        ActualEnemies = new();
        ActualItems = new();
    }

    void Start()
    {
        Attach(RoomManager);
        PlayerExitDoor.Attach(this);
    }

    
    public void EnableRoom()
    {
        RoomManager = FindObjectOfType<ActualRoomManager>();
        RoomManager.Player.position = PlayerEntryDoor.position;

        EnemiesPoolManager = EnemiesPoolManager.Instance;
        ItemsPoolManager = ItemsPoolManager.Instance;

        switch (RoomType)
        {
            case RoomTypeEnum.BossRoom:
                PickBossEnemy();
                break;

            case RoomTypeEnum.ShopRoom:
                HandleShopRoomActions();
                break;
            
            default:
                PickRoomEnemies();
                break;
        }
        
        PickRoomItems();
    }

    public void DisableRoom()
    {
        EnemiesPoolManager.EnemyObjectPool.ReleaseFullPool();

        ItemsPoolManager.DestructibleItemsObjectPool.ReleaseFullPool();
        ItemsPoolManager.GoldPool.ReleaseFullPool();
        ItemsPoolManager.HealthOrbPool.ReleaseFullPool();
    }


    // Picks the enemy quantity that is going to appear in the room
    // and instantiates all the enemies in the randomized spawn points
    private void PickRoomEnemies()
    {
        _enemyQuantity = Random.Range(2, EnemySpawnPoints.Count+1);
        PickedEnemySpawnPoints = new();

        for (int i = 0; i < _enemyQuantity; i++)
        {
            if(EnemySpawnPoints.Count == 0) return;

            int randomSpawnPointId = PickEnemySpawnPoint();

            GameObject enemy = EnemiesPoolManager.EnemyObjectPool.GetItem();
            enemy.transform.position = EnemySpawnPoints[randomSpawnPointId].position;

            ActualEnemies.Add(enemy);
        }
    }

    // Picks a random enemy from the boss enemies list and instantiates it
    // in a randomized spawn point
    private void PickBossEnemy()
    {
        int randomEnemySpawnPointId = Random.Range(0, EnemySpawnPoints.Count-1);

        GameObject enemy = EnemiesPoolManager.BossObjectPool.GetItem();
        enemy.transform.position = EnemySpawnPoints[randomEnemySpawnPointId].position;

        ActualEnemies.Add(enemy);
    }

    // Recursively picks a random non repeating spawn point for
    // the enemy to be spawned in
    public int PickEnemySpawnPoint()
    {
        int randomSpawnPointId = Random.Range(0, EnemySpawnPoints.Count-1);

        if(PickedEnemySpawnPoints.Contains(EnemySpawnPoints[randomSpawnPointId]))
            return PickEnemySpawnPoint();

        PickedEnemySpawnPoints.Add(EnemySpawnPoints[randomSpawnPointId]);
        return randomSpawnPointId;
    }
    
    // Method called once the enemy is defeated cleans the enemy 
    // from the pool and removes it from the remaining enemy list
    public void EnemyDefeated(GameObject enemy)
    {
        ActualEnemies.Remove(enemy);

        if(enemy.TryGetComponent(out EnemyController enemyController))
        {
            switch(enemyController.EnemyType)
            {
                case EnemyType.Spike:
                    EnemiesPoolManager.BossObjectPool.StoreItem(enemy);
                    break;

                default:
                    EnemiesPoolManager.EnemyObjectPool.StoreItem(enemy);
                    break;
            }
        }
    }


    // Picks the item quantity that is going to appear in the room
    // and instantiates all the items in the randomized spawn points
    private void PickRoomItems()
    {
        _itemsQuantity = Random.Range(4, 8);
        PickedItemSpawnPoints = new();

        for (int i = 0; i < _itemsQuantity; i++)
        {
            if(ItemsSpawnPoints.Count == 0) return;

            int randomSpawnPointId = PickItemSpawnPoint();
            
            GameObject item = ItemsPoolManager.DestructibleItemsObjectPool.GetItem();
            item.transform.position = ItemsSpawnPoints[randomSpawnPointId].position;

            ActualItems.Add(item);
        }
    }

    // Recursively picks a random non repeating spawn point for
    // the item to be spawned in
    private int PickItemSpawnPoint()
    {
        int randomSpawnPointId = Random.Range(0, ItemsSpawnPoints.Count-1);

        if(PickedItemSpawnPoints.Contains(ItemsSpawnPoints[randomSpawnPointId]))
            return PickItemSpawnPoint();

        PickedItemSpawnPoints.Add(ItemsSpawnPoints[randomSpawnPointId]);
        return randomSpawnPointId;
    }


    // Resets the shopping points if the next room is a Shop and 
    // upgrades all the enemy power ups
    private void HandleShopRoomActions()
    {
        ShopController shopController = FindObjectOfType<ShopController>();
        shopController.GenerateRandomPowerUps();
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

    public void Notify() {
        foreach (IObserver observer in _actualObservers.ToList())
        {
            observer.ObserverUpdate();
        }
    }


    // IObserver
    public void ObserverUpdate()
    {
        if(ActualEnemies.Count <= 0)
            Notify();
    }
}