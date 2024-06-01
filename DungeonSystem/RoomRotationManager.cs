using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomRotationManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _battleRooms;
    [SerializeField] private GameObject[] _bossRooms;
    [SerializeField] private GameObject[] _shopRooms;
    [field: SerializeField] public Queue<GameObject> ActualRoomQueue { get; private set; }

    [SerializeField] [Range(1, 10)] private int RoundRoomsQuantity;
    [field: SerializeField] public int CompletedRooms { get; private set; }


    void Awake()
    {
        ActualRoomQueue = new();
        RoundRoomsQuantity = StorageSystem.Instance.SettingsProfile.RoundsQuantityValue;
    }


    // If the ActualRoomQueue is empty regenerates the rooms and returns 
    // the first new room from the queue
    public GameObject GetNextRoom()
    {
        CompletedRooms += 1;

        if(ActualRoomQueue.Count <= 0)
            GenerateRoomRotation();

        return ActualRoomQueue.Dequeue();
    }


    // Generates a new room random rotation
    public void GenerateRoomRotation()
    {
        ActualRoomQueue.Clear();

        for (int i = 1; i < RoundRoomsQuantity+1; i++)
        {
            ActualRoomQueue.Enqueue(PickRandomRoom(_battleRooms));

            if(i == RoundRoomsQuantity / 2 || i == RoundRoomsQuantity)
                ActualRoomQueue.Enqueue(PickRandomRoom(_shopRooms));

            if(i == RoundRoomsQuantity)
                ActualRoomQueue.Enqueue(PickRandomRoom(_bossRooms));
        }
    }

    // Picks a random room from the provided list recursively while the selected room
    // is equal to the last room in the ActualRoomRotationQueue
    private GameObject PickRandomRoom(GameObject[] rooms)
    {
        int newRoomId = Random.Range(0, rooms.Length);

        if(ActualRoomQueue.Count > 0)
        {
            if(rooms[newRoomId].Equals(ActualRoomQueue.Last()))
            {
                return PickRandomRoom(rooms);
            }
        }

        return rooms[newRoomId];
    }
}
