using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MapGeneration
{
    public class HallWay : Room
    {
        private Vector3 _connectionPoint1 = new Vector3(0,0,0); 
        private Vector3 _connectionPoint2 = new Vector3(0,0,0);
        public static List<Vector3> s_connections = new List<Vector3>();
        public HallWay(GameObject ground, GameObject border, GameObject corner,
    int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Ground = ground;
            Border = border;
            Corner = corner;
            Width = width;
            Height = height;
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Border);
            NormalizePrefabSize(Corner);
            //Create room
            InitRoom();
            //Debug.Log($"Created new hallway : [X : {X} | Y : {Y} | Width: {Width} | Height : {Height} ]");
        }
        protected override void InitRoom()
        {
            int posX = X;
            int posY = Y;
            Quaternion rotation;
            Tiles = new Tile[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    //Hallway is horizontal
                    if (Height < Width)
                    {
                        //left side
                        if (x == 0)
                        {
                            //Bot left corner
                            if (y == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 180);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Top left corner
                            else if (y == Height-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 270);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Mid
                            else
                            {
                                rotation = RandomlyOffsetRotation();
                                _connectionPoint1 = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint1);
                                Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                            }
                        }
                        //right side
                        else if (x == Width - 1)
                        {
                            //Bot right corner
                            if (y == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 90);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Top right corner
                            else if (y == Height-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 0);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Mid
                            else
                            {
                                rotation = RandomlyOffsetRotation();
                                _connectionPoint2 = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint2);
                                Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                            }
                        }
                        //Mid
                        else
                        {
                            //Bot border
                            if (y == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 180);
                                Tiles[x, y] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Top border
                            else if (y == Height-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 0);
                                Tiles[x, y] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Mid
                            else
                            {
                                rotation = RandomlyOffsetRotation();
                                Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                            }
                        }
                    }
                    //Hallway is vertical
                    else
                    {
                        //Bot
                        if (y == 0)
                        {
                            //Bot left corner
                            if (x == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 0);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Bot right corner
                            else if (x == Width-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 270);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Mid
                            else
                            {
                                rotation = RandomlyOffsetRotation(); 
                                _connectionPoint1 = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint1);
                                Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                            }
                        }
                        //Top
                        else if (y == Height-1)
                        {
                            //Top left corner
                            if (x == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 90);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Top right corner
                            else if (x == Width-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 180);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Mid
                            else
                            {
                                rotation = RandomlyOffsetRotation();
                                _connectionPoint2 = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint2);
                                Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                            }
                        }
                        //Mid
                        else
                        {
                            //left border
                            if (x == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 90);
                                Tiles[x, y] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                            }
                            //right border
                            else if(x == Width-1)
                            {
                                rotation= Quaternion.Euler(0, 0, 270);
                                Tiles[x, y] = new Tile(Border, new Vector3(posX++, posY, 0), rotation);
                            }
                            else
                            {
                                rotation = RandomlyOffsetRotation();
                                Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                            }
                        }

                    }
                }
                posX = X;
                posY++;
            }
        }
    }
}
