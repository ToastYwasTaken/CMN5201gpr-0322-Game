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
            X = x;
            Y = y;
            Ground = ground;
            Wall = wall;
            Border = border;
            Width = width;
            Height = height;
            //Half the size or higher than original partition of BSPMap
            HeightOffset = height / 2;
            WidthOffset = width / 2;
            //Sets new width / height
            RandomlyOffsetRoomSize();
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Wall);
            NormalizePrefabSize(Border);
            //Create room
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
            float perlinScale = PerlinNoiseGenerator.RandomFloat(0.7f, 1.1f);
            float perlinIntensity = PerlinNoiseGenerator.RandomFloat(0.8f, 1.1f);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    rotation = RandomlyOffsetRotation();
                    //Create borders, override rotation
                    //TODO: Remove overlapping walls
                    if (i == 0)
                    {
                        rotation = Quaternion.Euler(0, 0, 180);
                        Tiles[j, i] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                    }
                    else if (j == 0)
                    {
                        rotation = Quaternion.Euler(0, 0, 90);
                        Tiles[j, i] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                    }
                    else if (j == Width-1)
                    {
                        rotation = Quaternion.Euler(0, 0, 270); 
                        Tiles[j, i] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                    }
                    else if (i == Height-1)
                    {
                        rotation = Quaternion.Euler(0, 0, 0);
                        Tiles[j, i] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                    }

                    else
                    {
                        perlinNoise = PerlinNoiseGenerator.GeneratePerlinNoiseAtCoordinates(posX, posY, perlinOffset, perlinOffset, perlinScale, perlinIntensity);
                        //Create walls from perlinNoise
                        if (perlinNoise < 0.7f)
                        {
                            Tiles[j, i] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                        }
                        else Tiles[j, i] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
                    }
                }
                posX = X;
                posY++;
            }
        }


    }
}
