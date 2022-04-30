using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generalized implementation of a unspezified room
///  - passes all needed objects
///  - assigns room size
///  - stores information about each Tile of the room
///  - passes Initialization to spezified classes (BossRoom.cs, NormalRoom.cs)
///     TODO: Dungeon algorithm / connecting rooms
/// </summary>

namespace MapGeneration
{
    public abstract class Room
    {
        protected Tile[,] Tiles;    //all tiles + data of one room
        public Tile[,] PTiles => Tiles;

        protected GameObject Wall;
        protected GameObject Border;
        protected GameObject Ground;
        protected ERoomSize RoomSize;
        protected int Width;
        protected int Height;
        private const int LOWERRANDOMBOUND = 10;
        private const int UPPERRANDOMBOUND = 100;
        private const int NORMALWIDTH = 20;
        private const int NORMALHEIGHT = 20;
        private System.Random Random = new((int)(System.DateTime.Now.Ticks));

        /// <summary>
        /// Assigns width and height accordingly
        /// </summary>
        /// <param name="roomSize">Generates size either randomly or from constants</param>
        public void AssignRoomSize(ERoomSize roomSize)
        {
            switch (roomSize)
            {
                //Assign a random width and height within given bounds
                case ERoomSize.Random:
                    Width = Random.Next(LOWERRANDOMBOUND, UPPERRANDOMBOUND);
                    Height = Random.Next(LOWERRANDOMBOUND, UPPERRANDOMBOUND);
                    break;
                //Assign the room with normal given sizes
                case ERoomSize.Normal:
                    Width = NORMALWIDTH;
                    Height = NORMALHEIGHT;
                    break;
                default:
                    break;
            }
        }
        protected abstract void InitRoom();
    }
}
