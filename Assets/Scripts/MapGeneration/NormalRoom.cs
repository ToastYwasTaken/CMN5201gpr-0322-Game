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
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Wall);
            NormalizePrefabSize(Border);
            //RandomlyOffsetRooms();
            InitRoom();
        }

        /// <summary>
        /// Assigns the tiles of the room
        /// </summary>
        protected override void InitRoom()
        {
<<<<<<< HEAD
            float perlinNoise;
=======
>>>>>>> parent of 2970b1ec (updated MapGen)
            Tiles = new Tile[Width, Height];
            Quaternion rotation;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    perlinNoise = Mathf.PerlinNoise(0, 1);
                    rotation = RandomlyOffsetRotation();
<<<<<<< HEAD
                    //Randomly add walls by perlinNoise
                    if (perlinNoise > 0.5f)
                    {
                        Tiles[j, i] = new Tile(Ground, new Vector3(i + X, j + Y, 0), rotation);
                    }else
                    {
                        Tiles[j, i] = new Tile(Wall, new Vector3(i + X, j + Y, 0), rotation);
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
=======
                    Tiles[x, y] = new Tile(Ground, new Vector3(X + x, Y + y, 0), rotation);
>>>>>>> parent of 2970b1ec (updated MapGen)
                }
            }
            Debug.Log("Created Normal Room, assigned Tiles");
        }


    }
}
