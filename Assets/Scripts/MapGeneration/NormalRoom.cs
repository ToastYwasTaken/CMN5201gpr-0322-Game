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
        public NormalRoom(GameObject empty, GameObject ground, GameObject wall, GameObject border,
            int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Empty = empty;
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
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    rotation = RandomlyOffsetRotation();
                    //Create borders, override rotation, filter out corners
                    if (y == 0)
                    {
                        if (x != 0 && x != Width-1)
                        {
                            rotation = Quaternion.Euler(0, 0, 180);
                            Tiles[x, y] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                        }
                        else
                        //in corner
                        {
                            Tiles[x, y] = new Tile(Empty, new Vector3(posX++, posY, 0), rotation);
                        }
                    }
                    else if (x == 0)
                    {
                        if (y != 0 && y != Height-1)
                        {
                            rotation = Quaternion.Euler(0, 0, 90);
                            Tiles[x, y] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                        }
                        else
                        //in corner
                        {
                            Tiles[x, y] = new Tile(Empty, new Vector3(posX++, posY, 0), rotation);
                        }
                    }
                    else if (x == Width-1)
                    {
                        if (y != Height-1 && y != 0)
                        {
                            rotation = Quaternion.Euler(0, 0, 270);
                            Tiles[x, y] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                        }
                        else
                        //in corner
                        {
                            Tiles[x, y] = new Tile(Empty, new Vector3(posX++, posY, 0), rotation);
                        }
                    }
                    else if (y == Height-1)
                    {
                        if (x != Width-1 && x != 0)
                        {
                            rotation = Quaternion.Euler(0, 0, 0);
                            Tiles[x, y] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                        }
                        else
                        //in corner
                        {
                            Tiles[x, y] = new Tile(Empty, new Vector3(posX++, posY, 0), rotation);
                        }
                    }
                    else
                    {
                        perlinNoise = PerlinNoiseGenerator.GeneratePerlinNoiseAtCoordinates(posX, posY, perlinOffset, perlinOffset, perlinScale, perlinIntensity);
                        //Create walls inside bounds from perlinNoise
                        if (perlinNoise < 0.7f)
                        {
                            Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                        }
                        else Tiles[x, y] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
                    }
                }
                posX = X;
                posY++;
            }
        }


    }
}
