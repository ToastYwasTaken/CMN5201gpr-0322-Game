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
            //Half the size or higher than original partition of BSPMap
            HeightOffset = Height / 2;
            WidthOffset = Width / 2;
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Wall);
            NormalizePrefabSize(Border);
            RandomlyOffsetRooms();
            InitRoom();
            Debug.Log($"Created new room : [X : {X} | Y : {Y} | Width: {Width} | Height : {Height} ]");
        }

        /// <summary>
        /// Assigns the tiles of the room
        /// </summary>
        protected override void InitRoom()
        {
            int posX = X;
            int posY = Y;
            float perlinNoise;
            Tiles = new Tile[Width, Height];
            Quaternion rotation;
            //create a random values between 'lower' and 'upper' bounds
            float perlinOffset = PerlinNoiseGenerator.RandomFloat(0.2f, 0.8f);
            float perlinScale = PerlinNoiseGenerator.RandomFloat(0.8f, 1.5f);
            float perlinIntensity = PerlinNoiseGenerator.RandomFloat(0.8f, 1.5f);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    perlinNoise = PerlinNoiseGenerator.GeneratePerlinNoiseAtCoordinates(posX, posY, perlinOffset, perlinOffset, perlinScale, perlinIntensity);
                    rotation = RandomlyOffsetRotation();
                    Tiles[j, i] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                }
                posX = X;
                posY++;
            }
            //Debug.Log("Created Normal Room, assigned Tiles");
        }


    }
}
