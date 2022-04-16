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

        protected override void AssignRoomSize(ERoomSize roomSize)
        {
            switch (roomSize)
            {
                case ERoomSize.Random:
                    _width = 0;
                    _height = 0;
                    break;
                case ERoomSize.Normal:
                    _width = 0;
                    _height = 0;
                    break;
                default:
                    break;
            }
        }


        protected override void InitRoom()
        {
            
        }
    }
}
