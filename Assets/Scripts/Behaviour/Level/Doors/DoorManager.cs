using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MapGeneration;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] Transform _map;
    Transform _hallways;
    Dictionary<Vector2, IDoor> _doorsDic = new Dictionary<Vector2, IDoor>();
    List<IDoor>[] _doorsByRoom;

    private void Start()
    {
        if (_map == null) return;
        GetHallWays();
        GetDoors();
    }

    public void OpenDoors(int room)
    {
        foreach(IDoor door in _doorsByRoom[room])
            door.UnlockDoor();
    }
    void GetHallWays()
    {
        foreach (Transform child in _map.transform)
            if (child.transform.name.Contains("Hallways"))
                _hallways = child;
    }
    void GetDoors()
    {
        List<GameObject> doors = new List<GameObject>();
        foreach (Transform child in _hallways)
            if (child.transform.CompareTag("Door"))
                doors.Add(child.gameObject);


        foreach (RoomDoorConnection doorsR in BSPMap.s_doorsBetweenRooms)
        {
            string door1 = ((int)doorsR.DoorRoom1.x).ToString() + "|" +  ((int)doorsR.DoorRoom1.y).ToString();
            string door2 = ((int)doorsR.DoorRoom2.x).ToString() + "|" +  ((int)doorsR.DoorRoom2.y).ToString(); /////

            foreach (GameObject door in doors)
            {
                if (door.name.Contains(door1))
                    _doorsDic.Add(doorsR.DoorRoom1, door.GetComponent<IDoor>());
                else if (door.name.Contains(door2))
                    _doorsDic.Add(doorsR.DoorRoom2, door.GetComponent<IDoor>());
            }
        }
        List<Room> rooms = BSPMap.s_allRooms;
        _doorsByRoom = new List<IDoor>[rooms.Count];
        for (int i = 0; i < rooms.Count; i++)
            foreach (RoomDoorConnection doorsR in BSPMap.s_doorsBetweenRooms)
            {
                if (doorsR.Room1.X == rooms[i].X && doorsR.Room1.Y == rooms[i].Y)
                {
                    _doorsByRoom[i].Add(_doorsDic[doorsR.DoorRoom1]);
                    _doorsByRoom[i].Add(_doorsDic[doorsR.DoorRoom2]);
                }
                if (doorsR.Room2.X == rooms[i].X && doorsR.Room2.Y == rooms[i].Y)
                {
                    _doorsByRoom[i].Add(_doorsDic[doorsR.DoorRoom1]);
                    _doorsByRoom[i].Add(_doorsDic[doorsR.DoorRoom2]);
                }
            }
    }
}
