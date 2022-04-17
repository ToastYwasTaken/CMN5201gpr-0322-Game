using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public abstract class Room
    {
        protected Tile[] _tiles;    //all tiles + data of one room
        public Tile[] Tiles
        {
            get => _tiles;
        }
        protected GameObject _wall;
        protected GameObject _ground;
        protected ERoomSize _roomSize;
        protected int _width;
        protected int _height;
        protected const int _LOWERRANDOMBOUND = 10;
        protected const int _UPPERRANDOMBOUND = 100;
        protected const int _NORMALWIDTH = 20;
        protected const int _NORMALHEIGHT = 20;
        protected System.Random _random = new System.Random((int)(System.DateTime.Now.Ticks));

        /// <summary>
        /// Assigns _width and _height accordingly
        /// </summary>
        /// <param name="roomSize">Generates size either randomly or from constants</param>
        protected void AssignRoomSize(ERoomSize roomSize)
        {
            switch (roomSize)
            {
                //Assign a random width and height within given bounds
                case ERoomSize.Random:
                    _width = _random.Next(_LOWERRANDOMBOUND, _UPPERRANDOMBOUND);
                    _height = _random.Next(_LOWERRANDOMBOUND, _UPPERRANDOMBOUND);
                    break;
                //Assign the room with normal given sizes
                case ERoomSize.Normal:
                    _width = _NORMALWIDTH;
                    _height = _NORMALHEIGHT;
                    break;
                default:
                    break;
            }
        }
        protected abstract void InitRoom();
    }
}
