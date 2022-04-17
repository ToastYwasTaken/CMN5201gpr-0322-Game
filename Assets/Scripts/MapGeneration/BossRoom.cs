using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class BossRoom : Room
    {
        public BossRoom(GameObject ground, GameObject wall, ERoomSize roomSize)
        {
            _ground = ground;
            _wall = wall;
            _roomSize = roomSize;
            AssignRoomSize(roomSize);
            InitRoom();
        }

        /// <summary>
        /// Assigns the tiles of the room
        /// </summary>
        protected override void InitRoom()
        {
            throw new System.NotImplementedException();
        }
    }
}
