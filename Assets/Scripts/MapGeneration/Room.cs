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
        #region position / size
        protected int X, Y, Width, Height;
        #endregion
        protected GameObject Wall;
        protected GameObject Border;
        protected GameObject Ground;
        protected int PositionOffset;
        protected int SizeOffset;
        private System.Random _rdm = new((int)(System.DateTime.Now.Ticks));

        /// <summary>
        /// Offsets Positions and Size randomly
        /// </summary>
        protected void RandomlyOffsetRooms()
        {
            X = _rdm.Next(X-PositionOffset, X + PositionOffset);
            Y = _rdm.Next(Y-PositionOffset, Y + PositionOffset);
            Width = _rdm.Next(Width-SizeOffset, Width + SizeOffset);
            Height = _rdm.Next(Height-SizeOffset, Height + SizeOffset);
        }

        protected abstract void InitRoom();
    }
}
