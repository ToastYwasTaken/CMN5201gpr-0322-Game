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
        public int X, Y, Width, Height, WidthOffset, HeightOffset;
        #endregion
        protected GameObject Ground;
        protected GameObject Obstacle1;
        protected GameObject Obstacle2;
        protected GameObject Wall;
        protected GameObject Corner;
        protected GameObject Door;
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
            int rdmInt = _rdm.Next(1, 3);
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

        /// <summary>
        /// Assigns the tiles of the room 
        /// </summary>
        protected virtual void InitRoom()
        {
            int posX = X;
            int posY = Y;
            float perlinNoise;
            Tiles = new Tile[Width, Height];
            //rotation is changed to make prefabs face the correct way
            Quaternion rotation = Quaternion.Euler(0,0,0);
            //create a random values between 'lower' and 'upper' bounds
            float perlinOffset = PerlinNoiseGenerator.RandomFloat(0.7f, 0.8f);
            float perlinScale = PerlinNoiseGenerator.RandomFloat(0.9f, 1.0f);
            float perlinIntensity = PerlinNoiseGenerator.RandomFloat(0.9f, 1.1f);
            //Debug.Log($"Offset: {perlinOffset} Scale: {perlinScale} Intensity: {perlinIntensity}");
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    //Create borders, override rotation, filter out corners
                    if (y == 0)
                    {
                        if (x != 0 && x != Width-1)
                        {
                            rotation = Quaternion.Euler(0, 0, 180);
                            Tiles[x, y] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
                        }
                        else
                        {
                            //in bot left corner
                            if (x == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 90);
                            }
                            //in bot right corner
                            else if (x == Width-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 180);
                            }
                            Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                        }
                    }
                    else if (x == 0)
                    {
                        if (y != 0 && y != Height-1)
                        {
                            rotation = Quaternion.Euler(0, 0, 90);
                            Tiles[x, y] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
                        }
                        else
                        //in top left corner
                        {
                            rotation = Quaternion.Euler(0, 0, 0);
                            Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                        }
                    }
                    else if (x == Width-1)
                    {
                        if (y != Height-1 && y != 0)
                        {
                            rotation = Quaternion.Euler(0, 0, 270);
                            Tiles[x, y] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
                        }
                        else
                        //in top right corner
                        {
                            rotation = Quaternion.Euler(0, 0, 270);
                            Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                        }
                    }
                    else if (y == Height-1)
                    {
                        if (x != Width-1 && x != 0)
                        {
                            rotation = Quaternion.Euler(0, 0, 0);
                            Tiles[x, y] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
                        }
                    }
                    //Next to wall, don't create inside bounds walls
                    else if (x <= 1 | y <= 1 | x >= Width - 2 | y >= Height - 2)
                    {
                        rotation = RandomlyOffsetRotation();
                        Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                    }
                    //Create inside bounds walls by perlin noise
                    else
                    {
                        rotation = RandomlyOffsetRotation();
                        perlinNoise = PerlinNoiseGenerator.GeneratePerlinNoiseAtCoordinates(posX, posY, perlinOffset, perlinOffset, perlinScale, perlinIntensity);

                        //Create walls inside bounds from perlinNoise
                        if (perlinNoise < 0.75f)
                        {
                            Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                        }
                        else
                        {
                            int rdmInt =_rdm.Next(1, 3);
                            if(rdmInt == 1)
                            {
                            Tiles[x, y] = new Tile(Obstacle1, new Vector3(posX++, posY, 0), rotation);
                            }
                            else Tiles[x, y] = new Tile(Obstacle2, new Vector3(posX++, posY, 0), rotation);
                        }
                    }
                }
                posX = X;
                posY++;
            }
        }
    }
}
