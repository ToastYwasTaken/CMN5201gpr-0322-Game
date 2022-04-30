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
        public NormalRoom(GameObject ground, GameObject wall, GameObject border, ERoomSize roomSize)
        {
            Ground = ground;
            Wall = wall;
            Border = border;
            RoomSize = roomSize;
            AssignRoomSize(roomSize);
            InitRoom();
        }

        /// <summary>
        /// Assigns the tiles of the room
        /// </summary>
        protected override void InitRoom()
        {
            int posX = 0;
            int posY = 0;
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
                    else if (true)
                    {
                        Tiles[i, j] = new Tile(Wall, new Vector3(posX++, posY++, 0), rotation);
                    }
                }
            }
        }


    }
}
