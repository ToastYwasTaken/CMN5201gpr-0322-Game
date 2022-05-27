using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MapGeneration
{
    public class HallWay : Room
    {
        private Vector3 _connectionPoint = new Vector3(0,0,0); 
        public static List<Vector3> s_connections = new List<Vector3>();

        public HallWay(GameObject ground, GameObject border, GameObject corner, GameObject door,
    int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Ground = ground;
            Wall = border;
            Corner = corner;
            Door = door;
            Width = width;
            Height = height;
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Wall);
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
                                rotation = Quaternion.Euler(0, 0, 0);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Top left corner
                            else if (y == Height-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 90); 
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Mid left
                            else
                            {
                                rotation = Quaternion.Euler(0, 0, 90);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Door, new Vector3(posX++, posY, 0), rotation);
                            }
                        }
                        //right side
                        else if (x == Width - 1)
                        {
                            //Bot right corner
                            if (y == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 270); 
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Top right corner
                            else if (y == Height-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 180);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Mid right
                            else
                            {
                                rotation = Quaternion.Euler(0, 0, 90);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Door, new Vector3(posX++, posY, 0), rotation);
                            }
                        }
                        //Mid
                        else
                        {
                            //Bot border
                            if (y == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 180);
                                Tiles[x, y] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Top border
                            else if (y == Height-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 0);
                                Tiles[x, y] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
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
                                rotation = Quaternion.Euler(0, 0, 180);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Bot right corner
                            else if (x == Width-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 90);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Bot mid
                            else
                            {
                                rotation = Quaternion.Euler(0, 0, 0);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Door, new Vector3(posX++, posY, 0), rotation);
                            }
                        }
                        //Top
                        else if (y == Height-1)
                        {
                            //Top left corner
                            if (x == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 270);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Top right corner
                            else if (x == Width-1)
                            {
                                rotation = Quaternion.Euler(0, 0, 0);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Corner, new Vector3(posX++, posY, 0), rotation);
                            }
                            //Top mid
                            else
                            {
                                rotation = Quaternion.Euler(0, 0, 0);
                                _connectionPoint = new Vector3(posX, posY, 0);
                                s_connections.Add(_connectionPoint);
                                Tiles[x, y] = new Tile(Door, new Vector3(posX++, posY, 0), rotation);
                            }
                        }
                        //Mid
                        else
                        {
                            //left border
                            if (x == 0)
                            {
                                rotation = Quaternion.Euler(0, 0, 90);
                                Tiles[x, y] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
                            }
                            //right border
                            else if(x == Width-1)
                            {
                                rotation= Quaternion.Euler(0, 0, 270);
                                Tiles[x, y] = new Tile(Wall, new Vector3(posX++, posY, 0), rotation);
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
