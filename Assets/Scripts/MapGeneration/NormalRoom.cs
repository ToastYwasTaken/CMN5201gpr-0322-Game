using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a non-boss room
/// multiple are instantiated in Level.cs
/// </summary>

namespace Assets.Scripts.MapGeneration
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
        /// Assigns the tiles of the room by perlin noise
        /// Offset Noise after each Room
        /// </summary>
        //protected override void InitRoom()
        //{
        //    Tiles = new Tile[Width, Height];
        //    Quaternion rotation;
        //    float perlinNoise;
        //    //create a random float between 0.2 and 0.8 as perlinOffset each time creating a new room
        //    float perlinOffset = PerlinNoiseGenerator.RandomFloat(0.2f, 0.8f);
        //    float perlinScale = PerlinNoiseGenerator.RandomFloat(0.8f, 1.5f);
        //    float perlinIntensity = PerlinNoiseGenerator.RandomFloat(0.8f, 1.5f);
        //    for (int x = 0; x < Width; x++)
        //    {
        //        for (int y = 0; y < Height; y++)
        //        {
        //            rotation = RandomlyOffsetRotation();
        //            perlinNoise = PerlinNoiseGenerator.GeneratePerlinNoiseAtCoordinates(x, y, perlinOffset, perlinOffset, perlinScale, perlinIntensity);
        //            Debug.Log($"Noise: {perlinNoise} Offset: {perlinOffset} Scale: {perlinScale} Intensity: {perlinIntensity}");
        //            //Randomly add walls by perlinNoise
        //            if (perlinNoise > 0.2f)
        //            {
        //                Tiles[x, y] = new Tile(Ground, new Vector3(x + X, y + Y, 0), rotation);
        //            }else
        //            {
        //                Tiles[x, y] = new Tile(Wall, new Vector3(x + X, y + Y, 0), rotation);
        //            }
        //            //Border Prefab 
        //            //else if (true)
        //            //{
        //            //    Tiles[i, j] = new Tile(Border, new Vector3(i, j, 0), rotation);
        //            //} 
        //            //Wall Prefab
        //            //else if (true)
        //            //{
        //            //    Tiles[i, j] = new Tile(Wall, new Vector3(i, j, 0), rotation);
        //            //}
        //        }
        //    }
        //    Debug.Log("Created Normal Room, assigned Tiles");
        //}
        protected override void InitRoom()
        {
            int posX = X;
            int posY = Y;
            Tiles = new Tile[Width, Height];
            Quaternion rotation;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    rotation = RandomlyOffsetRotation();
                    Tiles[x, y] = new Tile(Ground, new Vector3(posX, posY++, 0), rotation); //increment position
                }
                //Reset position
                posY = Y;
                posX++;
            }
            //Debug.Log("Created Normal Room, assigned Tiles");
        }


    }
}
