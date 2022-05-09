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

namespace Assets.Scripts.MapGeneration
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
        protected PerlinNoiseGenerator PerlinNoiseGenerator = new PerlinNoiseGenerator();
        protected int RoomCount;
        protected System.Random Rdm = new((int)(System.DateTime.Now.Ticks));

        /// <summary>
        /// Offsets Positions and Size randomly
        /// </summary>
        protected void RandomlyOffsetRooms()
        {
            X = Rdm.Next(X-PositionOffset, X + PositionOffset);
            Y = Rdm.Next(Y-PositionOffset, Y + PositionOffset);
            Width = Rdm.Next(Width-SizeOffset, Width + SizeOffset);
            Height = Rdm.Next(Height-SizeOffset, Height + SizeOffset);
        }

        protected Quaternion RandomlyOffsetRotation()
        {
            Quaternion rotation;
            int rdmInt = Rdm.Next(1, 8);
            if (rdmInt == 1)
            {
                rotation = Quaternion.Euler(0, 0, 90);
            }
            else rotation = Quaternion.identity;
            return rotation;
        }

        protected void NormalizePrefabSize(GameObject prefab)
        {
            prefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        protected abstract void InitRoom();
    }
}
