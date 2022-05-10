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
        #region position / size / offsets
        public int X, Y, Width, Height;
        protected int WidthOffset, HeightOffset;
        #endregion
        protected GameObject Wall;
        protected GameObject Border;
        protected GameObject Ground;
        private System.Random _rdm = new((int)(System.DateTime.Now.Ticks));

        /// <summary>
        /// Offsets roomsize randomly
        /// </summary>
        protected void RandomlyOffsetRoomSize()
        {
            Width = _rdm.Next(WidthOffset, Width-1);
            Height = _rdm.Next(HeightOffset, Height-1);
        }

        /// <summary>
        /// Offsets rotation randomly
        /// </summary>
        /// <returns>rotation</returns>
        protected Quaternion RandomlyOffsetRotation()
        {
            Quaternion rotation;
            int rdmInt = _rdm.Next(1, 8);
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

        public Vector3 CalculatePlayerSpawnPositionInRoom()
        {
            return new Vector3(_rdm.Next(WidthOffset, Width-1),_rdm.Next(HeightOffset, Height-1), 0);
        }

        protected abstract void InitRoom();
    }
}
