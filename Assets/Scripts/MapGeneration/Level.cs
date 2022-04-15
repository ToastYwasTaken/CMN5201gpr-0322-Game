using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class Level
    {
        private int _roomCount;
        private Room _room;

        public Level(int roomCount, GameObject wallPrefab, GameObject groundPrefab, ERoomSize _roomSize)
        {
            _roomCount = roomCount;
            InitRooms(roomCount, wallPrefab, groundPrefab, _roomSize);
        }

        public void InitRooms(int roomCount, GameObject wallPrefab, GameObject groundPrefab, ERoomSize _roomSize)
        {
            //Init roomCount-1 rooms + 1 boss room

        }
    }
}
