using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a new Level by creating new rooms accordingly
/// </summary>

namespace MapGeneration
{
    public class Level
    {
        protected int RoomCount;
        private Room[] _rooms;

        public Room[] PRooms => _rooms;

        public Level(int roomCount, GameObject groundPrefab, GameObject wallPrefab, GameObject borderPrefab, ERoomSize roomSize)
        {
            RoomCount = roomCount;
            InitAllRooms(roomCount, groundPrefab, wallPrefab, borderPrefab, roomSize);
        }

        private void InitAllRooms(int roomCount, GameObject groundPrefab, GameObject wallPrefab, GameObject borderPrefab, ERoomSize roomSize)
        {
            _rooms = new Room[roomCount];
            //Init roomCount-1 normal rooms 
            InitNormalRooms(groundPrefab, wallPrefab, borderPrefab, roomSize);
            //Init 1 boss room
            InitBossRoom(groundPrefab, wallPrefab, borderPrefab, roomSize);
        }

        private void InitNormalRooms(GameObject groundPrefab, GameObject wallPrefab, GameObject borderPrefab, ERoomSize roomSize)
        {
            for (int i = 0; i < RoomCount-1; i++)
            {
                _rooms[i] = new NormalRoom(groundPrefab, wallPrefab, borderPrefab, roomSize);
            }
        }

        private void InitBossRoom(GameObject groundPrefab, GameObject wallPrefab, GameObject borderPrefab, ERoomSize roomSize)
        {
            _rooms[RoomCount] = new BossRoom(groundPrefab, wallPrefab, borderPrefab, roomSize);
        }

    }
}
