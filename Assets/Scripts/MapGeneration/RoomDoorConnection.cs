using Unity;
using UnityEngine;

namespace Assets.Scripts.MapGeneration
{
    public class RoomDoorConnection
    {
        public Room Room1;
        public Room Room2;
        public Vector2 DoorRoom1;
        public Vector2 DoorRoom2;
        public RoomDoorConnection(Room room1, Room room2, Vector2 doorRoom1, Vector2 doorRoom2)
        {
            Room1 = room1;
            Room2 = room2;
            DoorRoom1 = doorRoom1;
            DoorRoom2 = doorRoom2;
        }
    }
}