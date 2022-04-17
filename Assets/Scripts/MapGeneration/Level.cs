using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class Level
    {
        private int _roomCount;
        private Room[] _rooms;

        public Room[] Rooms
        {
            get => _rooms;
        }

        public Level(int roomCount, GameObject wallPrefab, GameObject groundPrefab, ERoomSize roomSize)
        {
            _roomCount = roomCount;
            InitRooms(roomCount, wallPrefab, groundPrefab, roomSize);
        }

        public void InitRooms(int roomCount, GameObject wallPrefab, GameObject groundPrefab, ERoomSize roomSize)
        {
            _rooms = new Room[roomCount];
            //Init roomCount-1 normal rooms 
            for (int i = 0; i < roomCount-1; i++)
            {
                _rooms[i] = new NormalRoom(groundPrefab, wallPrefab, roomSize);
            }
            //Init 1 boss room
            _rooms[roomCount] = new BossRoom(groundPrefab, wallPrefab, roomSize);
        }
    }
}
