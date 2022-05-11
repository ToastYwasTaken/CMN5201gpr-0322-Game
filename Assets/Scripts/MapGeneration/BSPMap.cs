using System;
using System.Collections.Generic;
using UnityEngine;

//Based on: https://gamedevelopment.tutsplus.com/de/tutorials/how-to-use-bsp-trees-to-generate-game-maps--gamedev-12268
namespace Assets.Scripts.MapGeneration
{
    public class BSPMap
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public BSPMap FirstMap, SecondMap;
        private static int s_MIN_PARTITION_WIDTH;
        private static int s_MIN_PARTITION_HEIGHT;
        private Room currentRoom;
        public static List<Room> s_allRooms = new List<Room>();
        private static List<Room> s_allHallWays = new List<Room>();
        public BSPMap(int x, int y, int maxWidth, int maxHeight)
        {
            X = x;
            Y = y;
            Width = maxWidth;
            Height = maxHeight;
            Debug.Log($"Creating new Map: X: [{X}] Y: [{Y}] Width: [{Width}] Height: [{Height}] ");
        }

        /// <summary>
        /// Assigning static values of 's_MIN_PARTITION_WIDTH' and 's_MIN_PARTITION_HEIGHT'
        /// </summary>
        public static void AssignMinValues(int minPartitionWidth, int minPartitionHeight)
        {
            s_MIN_PARTITION_WIDTH = minPartitionWidth;
            s_MIN_PARTITION_HEIGHT = minPartitionHeight;
        } 

        /// <summary>
        /// Split Map if splitable and desired partition side isn't too small
        /// </summary>
        /// <returns>true if map was split, else returns false </returns>
        public bool SplitMap()
        {
            System.Random rnd = new System.Random();
            bool splitHorizontal = false;
            int randomPositionOnHorizontal, randomPositionOnVertical;
            //already split
            if (FirstMap != null || SecondMap != null)
            {
                return false;
            }
            //Decide whether to split vertically or horizontally
            if (Width > Height && Width / Height >= 1.25f)
            {
                splitHorizontal = false;
            }
            else if (Height > Width && Height / Width >= 1.25f)
            {
                splitHorizontal = true;
            }
            else
            {
                int rndInt = rnd.Next(1, 3);
                if (rndInt == 1)
                {
                    splitHorizontal = false;
                }
                else splitHorizontal = true;
            }
            //Assigning max height / width
            int maxLength = splitHorizontal ? (Height - s_MIN_PARTITION_HEIGHT) : (Width - s_MIN_PARTITION_WIDTH);
            //Don't split if not enough space
            if (maxLength <= s_MIN_PARTITION_WIDTH ||maxLength <= s_MIN_PARTITION_HEIGHT)
            {
                return false;
            }
            //Splitting horizontaly
            if (splitHorizontal)
            {
                randomPositionOnHorizontal = rnd.Next(s_MIN_PARTITION_HEIGHT, maxLength);
                FirstMap = new BSPMap(X, Y, Width, randomPositionOnHorizontal);
                SecondMap = new BSPMap(X, Y + randomPositionOnHorizontal, Width, Height - randomPositionOnHorizontal);
            }
            //Splitting vertically
            else
            {
                randomPositionOnVertical = rnd.Next(s_MIN_PARTITION_WIDTH, maxLength);
                FirstMap = new BSPMap(X, Y, randomPositionOnVertical, Height);
                SecondMap = new BSPMap(X + randomPositionOnVertical, Y, Width - randomPositionOnVertical, Height);
            }
            return true;
        }

        /// <summary>
        /// Create new rooms from prefabs for all rooms lowest partitions
        /// </summary>
        public void CreateRooms(GameObject ground, GameObject wall, GameObject border)
        {
            if (FirstMap != null || SecondMap != null)
            {
                //Map has been split, checking lower partitions
                if (FirstMap != null)
                {
                    FirstMap.CreateRooms(ground, wall, border);
                }
                if (SecondMap != null)
                {
                    SecondMap.CreateRooms(ground, wall, border);
                }
                if(FirstMap != null && SecondMap != null)
                {
                    CreateHallWay(FirstMap.currentRoom, SecondMap.currentRoom, ground, wall, border);
                }
            }
            else
            //Lowest partition -> Create room here -> assign room to this partition
            {
                currentRoom = new NormalRoom(ground, wall, border, X, Y, Width, Height);
                s_allRooms.Add(currentRoom);
            }
        }

        private void CreateHallWay(Room currentRoom1, Room currentRoom2, GameObject ground, GameObject wall, GameObject border)
        {
            //Calculate possible connection location
            System.Random rdm = new System.Random();
            int rdmInt = rdm.Next(0, 2);
            Room hallway1 = null;
            Room hallway2= null;
            //TODO: FIX DAT SHIT
            int splitDifferenceHorizontalRoom1 = rdm.Next(currentRoom1.X + 1, currentRoom1.Width-2);
            int splitDifferenceHorizontalRoom2 = rdm.Next(currentRoom2.X + 1, currentRoom2.Width-2);            
            int splitDifferenceVerticalRoom1 = rdm.Next(currentRoom1.Y + 1, currentRoom1.Height-2);
            int splitDifferenceVerticalRoom2 = rdm.Next(currentRoom2.Y + 1, currentRoom2.Height-2);

            Vector2 splitPointRoom1 = new Vector2(splitDifferenceHorizontalRoom1, splitDifferenceVerticalRoom1);
            Vector2 splitPointRoom2 = new Vector2(splitDifferenceHorizontalRoom2, splitDifferenceVerticalRoom2);

            int splitPointX = (int)(splitPointRoom2.x - splitPointRoom1.x);
            int splitPointY = (int)(splitPointRoom2.y-splitPointRoom1.y);
            //Calculate connection direction
            if (splitPointX < 0)
            {
                if(splitPointY < 0)
                {
                    if (rdmInt == 0)
                    {
                        hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                    }
                    else 
                    {
                        hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                    }
                } else if(splitPointY > 0)
                {
                    if (rdmInt == 0)
                    {
                        hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                    }
                    else
                    {
                        hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                    }
                }
                //splitPointY == 0
                else
                {
                    hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                }
            }else if(splitPointX > 0)
            {
                if (splitPointY < 0)
                {
                    if (rdmInt == 0)
                    {
                        hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                    }
                    else
                    {
                        hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                    }
                }
                else if (splitPointY > 0)
                {
                    if (rdmInt == 0)
                    {
                        hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                    }
                    else
                    {
                        hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                    }
                }
                //splitPointY == 0
                else
                {
                    hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                }
            //splitPointX == 0
            } else 
            {
                if(splitPointY < 0)
                {
                    hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                }
                else if(splitPointY > 0)
                {
                    hallway1 = new HallWay(ground, wall, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                }
            }
            //Add hallways to list
            if (hallway2 != null && hallway1 != null)
            {
                s_allHallWays.Add(hallway1);
                s_allHallWays.Add(hallway2);
            } else s_allHallWays.Add(hallway1);
        }
    }
}
