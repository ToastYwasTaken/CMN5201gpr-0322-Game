using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a non-boss room
/// multiple are instantiated in Level.cs
/// </summary>

namespace MapGeneration
{
    public class NormalRoom : Room
    {
        public NormalRoom(GameObject ground, GameObject wall, GameObject border,
            int x, int y, int width, int height)
        {
            Ground = ground;
            Wall = wall;
            Border = border;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            PositionOffset = (X + Y) / 6 ;
            SizeOffset = (Width + Height) / 6 ;
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
                        Tiles[i, j] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                    }
                    //Border Prefab 
                    //else if (true)
                    //{
                    //    Tiles[i, j] = new Tile(Border, new Vector3(i, j, 0), rotation);
                    //} 
                    //Wall Prefab
                    //else if (true)
                    //{
                    //    Tiles[i, j] = new Tile(Wall, new Vector3(i, j, 0), rotation);
                    //}
                }
                posX = X;
                posY++;
            }
            Debug.Log("Created Normal Room, assigned Tiles");
        }


    }
}
