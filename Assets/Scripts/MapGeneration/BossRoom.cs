using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates one boss room for the level
/// </summary>

namespace MapGeneration
{
    public class BossRoom : Room
    {
        public BossRoom(GameObject ground, GameObject wall, GameObject border,
            int x, int y, int width, int height)
        {
            Ground = ground;
            Wall = wall;
            Border = border;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            RandomlyOffsetRooms();
            InitRoom();
        }

        /// <summary>
        /// Assigns the tiles of the room
        /// </summary>
        protected override void InitRoom()
        {
            int posX = X;
            int posY = Y;
            Tiles = new Tile[Width, Height];
            Quaternion rotation = Quaternion.identity;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    //Ground Prefab //TODO: Add conditions
                    if (true)
                    {
                        Tiles[i, j] = new Tile(Ground, new Vector3(posX++, posY++, 0), rotation);
                    }
                    //Border Prefab 
                    else if (true)
                    {
                        Tiles[i, j] = new Tile(Border, new Vector3(posX++, posY++, 0), rotation);
                    }
                    //Wall Prefab
                    //else if (true)
                    //{
                    //    Tiles[i, j] = new Tile(Wall, new Vector3(posX++, posY++, 0), rotation);
                    //}
                }
            }
            Debug.Log("Created BossRoom, assigned Tiles");
        }
    }
}
