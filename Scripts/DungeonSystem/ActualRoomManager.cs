using System.Collections.Generic;
using UnityEngine;

public class ActualRoomManager : MonoBehaviour, IObserver
{
    [field: SerializeField] public ActualRoomManager Instance { get; private set; }
    [field: SerializeField] public RoomRotationManager RoomRotationManager { get; private set; }
    [field: SerializeField] public RoomSystem ActualRoom { get; private set; }
    protected readonly List<IObserver> ActualObservers = new();
    public Transform Player;


    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        RoomRotationManager = GetComponent<RoomRotationManager>();
        GetNextRoom();
        ShowRoom();
    }


    private void GetNextRoom()
    {
        GameObject newRoom = RoomRotationManager.GetNextRoom();

        if(newRoom.TryGetComponent(out RoomSystem roomSystem))
        {
            ActualRoom = roomSystem;
        }
    }

    private void SwitchRooms()
    {
        HideRoom();
        GetNextRoom();
        ShowRoom();
    }

    private void ShowRoom()
    {
        ActualRoom.gameObject.SetActive(true);
        Player.position = ActualRoom.PlayerEntryDoor.position;
        ActualRoom.EnableRoom();
    }

    private void HideRoom()
    {
        ActualRoom.DisableRoom();
        ActualRoom.gameObject.SetActive(false);
        Player.position = Vector3.zero;
    }

    // IObserver
    public void ObserverUpdate()
    {
        SwitchRooms();
        Debug.Log($"Actual Room => {ActualRoom}");
    }
}
