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
        public static List<Room> s_allHallWays = new List<Room>();
        public BSPMap(int x, int y, int maxWidth, int maxHeight)
        {
            X = x;
            Y = y;
            Width = maxWidth;
            Height = maxHeight;
            //Debug.Log($"Creating new Map: X: [{X}] Y: [{Y}] Width: [{Width}] Height: [{Height}] ");
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
        /// Calculates player spawn position
        /// </summary>
        /// <returns></returns>
        public Vector3 CalculatePlayerSpawnPositionInRoom()
        {
            Vector3 playerSpawnPosition = new Vector3(0, 0, 0);
            if (s_allRooms[0] != null)
            {
                Room spawnRoom = s_allRooms[0];
                System.Random rand = new System.Random();
                playerSpawnPosition = new Vector3(rand.Next(spawnRoom.WidthOffset, Width-1), rand.Next(spawnRoom.HeightOffset, Height-1), 0);
            }
            return playerSpawnPosition;
        }

        /// <summary>
        /// Returns current room if lowest partition
        /// </summary>
        /// <returns></returns>
        private Room GetRoom()
        {
            if (currentRoom != null)
            {
                return currentRoom;
            }
            else
            {
                Room leftRoom = null;
                Room rightRoom = null;
                if (FirstMap != null)
                {
                    leftRoom = FirstMap.GetRoom();
                }
                if (SecondMap != null)
                {
                    rightRoom = SecondMap.GetRoom();
                }
                if (leftRoom == null && rightRoom == null)
                {
                    return null;
                }
                else if (leftRoom == null)
                {
                    return rightRoom;
                }
                else if (rightRoom == null)
                {
                    return leftRoom;
                }
                else return leftRoom;
            }
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
        public void CreateRooms(GameObject empty, GameObject ground, GameObject wall, GameObject border, GameObject corner, GameObject debugGround)
        {
            if (FirstMap != null || SecondMap != null)
            {
                //Map has been split, checking lower partitions
                if (FirstMap != null)
                {
                    FirstMap.CreateRooms(empty, ground, wall, border, corner, debugGround);
                }
                if (SecondMap != null)
                {
                    SecondMap.CreateRooms(empty, ground, wall, border, corner, debugGround);
                }
                if (FirstMap != null && SecondMap != null)
                {
                    CreateHallWay(FirstMap.GetRoom(), SecondMap.GetRoom(), debugGround, border); //replace with normal ground once done
                }
            }
            else
            //Lowest partition -> Create room here -> assign room to this partition
            {
                currentRoom = new NormalRoom(empty, ground, wall, border, corner, X, Y, Width, Height);
                s_allRooms.Add(currentRoom);
            }
        }

        /// <summary>
        /// Hallway creation algorithm
        /// </summary>
        /// <param name="room1"></param>
        /// <param name="room2"></param>
        /// <param name="ground"></param>
        /// <param name="border"></param>
        private void CreateHallWay(Room room1, Room room2, GameObject ground, GameObject border)
        {
            //Calculate possible connection location
            System.Random rdm = new System.Random();
            int rdmInt = rdm.Next(0, 2);
            Room hallway1 = null;
            Room hallway2 = null;

            if (room1 == null || room2 == null)
            {
                Debug.Log("a room was null");
                return;
            }
            #region
            //int splitVerticalRoom1 = room1.X + room1.Width;
            //int splitVerticalRoom2 = room2.X + room2.Width;
            //int splitHorizontalRoom1 = room1.Y + room1.Height;
            //int splitHorizontalRoom2 = room2.Y + room2.Height;

            //Debug.Log($"Room1: {room1.X} {room1.Y} | Room 2: {room2.X} {room2.Y}");

            ////Room 1 left of Room 2
            //if (room1.X < room2.X)
            //{
            //    //Room 1 left and below Room 2
            //    if (room1.Y < room2.Y)
            //    {
            //        Debug.Log("Room 1 left and below room 2");

            //    }
            //    //Room 1 left and above Room 2
            //    else if (room1.Y > room2.Y)
            //    {
            //        Debug.Log("Room 1 left and above room 2");
            //    }
            //    else
            //    //Room1 and at equal height of room 2
            //    {
            //        Debug.Log("Room 1 left and at equal height of room 2");

            //    }
            //}
            ////Room 1 right of Room 2
            //else if (room1.X > room2.X)
            //{
            //    //Room 1 right and below Room 2
            //    if (room1.Y < room2.Y)
            //    {
            //        Debug.Log("Room 1 right and below room 2");

            //    }
            //    //Room 1 right and above Room 2
            //    else if (room1.Y > room2.Y)
            //    {
            //        Debug.Log("Room 1 right and above room 2");

            //    }
            //    //Room 1 right and at equal height of Room 2
            //    else
            //    {
            //        Debug.Log("Room 1 right and at equal height of room 2");
            //    }
            //}
            //// equal width
            //else
            //{
            //    //Room 1 at equal width and below Room 2
            //    if (room1.Y < room2.Y)
            //    {
            //        Debug.Log("Room 1 at equal width and below room 2");

            //    }
            //    //Room 1 at equal width and above Room 2
            //    else if (room1.Y > room2.Y)
            //    {
            //        Debug.Log("Room 1 at equal width and above room 2");

            //    }
            //}
            #endregion
            #region
            int rightRoom1 = currentRoom1.Width + currentRoom1.X;
            int rightRoom2 = currentRoom2.Width + currentRoom2.X;
            int topRoom1 = currentRoom1.Height + currentRoom1.Y;
            int topRoom2 = currentRoom2.Height + currentRoom2.Y;
            int splitDifferenceHorizontalRoom1 = rdm.Next(currentRoom1.X+1, rightRoom1-2);
            int splitDifferenceHorizontalRoom2 = rdm.Next(currentRoom2.X+1, rightRoom2-2);
            int splitDifferenceVerticalRoom1 = rdm.Next(currentRoom1.Y+1, topRoom1-2);
            int splitDifferenceVerticalRoom2 = rdm.Next(currentRoom2.Y+1, topRoom2-2);

            Vector2 splitPointRoom1 = new Vector2(splitDifferenceHorizontalRoom1, splitDifferenceVerticalRoom1);
            Vector2 splitPointRoom2 = new Vector2(splitDifferenceHorizontalRoom2, splitDifferenceVerticalRoom2);

            //Points in range of both rooms
            int splitPointX = (int)(splitPointRoom2.x - splitPointRoom1.x);
            int splitPointY = (int)(splitPointRoom2.y - splitPointRoom1.y);

            Debug.Log($"rightRoom1 : {rightRoom1} rightRoom2 : {rightRoom2} topRoom1 : {topRoom1} topRoom2 : {topRoom2} splitPointRoom1 : {splitPointRoom1} splitPointRoom2 : {splitPointRoom2} splitPointX : {splitPointX} splitPointY : {splitPointY}");
            //Calculate connection direction
            //
            if (splitPointX < 0)
            {
                if (splitPointY < 0)
                {
                    if (rdmInt == 0)
                    {
                        Debug.Log($"Case 1: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                        hallway1 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                    }
                    else
                    {
                        Debug.Log($"Case 2: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                        hallway1 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                    }
                }
                else if (splitPointY > 0)
                {
                    if (rdmInt == 0)
                    {
                        Debug.Log($"Case 3: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                        hallway1 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                    }
                    else
                    {
                        Debug.Log($"Case 4: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                        hallway1 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                    }
                }
                //splitPointY == 0
                else
                {
                    Debug.Log($"Case 5: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                    hallway1 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                }
            }
            else if (splitPointX > 0)
            {
                if (splitPointY < 0)
                {
                    if (rdmInt == 0)
                    {
                        Debug.Log($"Case 6: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                        hallway1 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                    }
                    else
                    {
                        Debug.Log($"Case 7: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                        hallway1 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                    }
                }
                else if (splitPointY > 0)
                {
                    if (rdmInt == 0)
                    {
                        Debug.Log($"Case 8: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                        hallway1 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                    }
                    else
                    {
                        Debug.Log($"Case 9: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                        hallway1 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom2.y, Math.Abs(splitPointX), 1);
                        hallway2 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                    }
                }
                //splitPointY == 0
                else
                {
                    Debug.Log($"Case 10: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                    hallway1 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, Math.Abs(splitPointX), 1);
                }
                //splitPointX == 0
            }
            else
            {
                if (splitPointY < 0)
                {
                    Debug.Log($"Case 11: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                    hallway1 = new HallWay(ground, border, (int)splitPointRoom2.x, (int)splitPointRoom2.y, 1, Math.Abs(splitPointY));
                }
                else if (splitPointY > 0)
                {
                    Debug.Log($"Case 12: splitPointX: {splitPointX} | splitPointY: {splitPointY} | splitPointRoom1.x : {splitPointRoom1.x} | splitPointRoom1.y : {splitPointRoom1.y} | splitPointRoom2.x : {splitPointRoom2.x}  | splitPointRoom2.y : {splitPointRoom2.y}");
                    hallway1 = new HallWay(ground, border, (int)splitPointRoom1.x, (int)splitPointRoom1.y, 1, Math.Abs(splitPointY));
                }
            }
            #endregion
            //Add hallways to list
            if (hallway2 != null && hallway1 != null)
            {
                s_allHallWays.Add(hallway1);
                s_allHallWays.Add(hallway2);
            }
            else s_allHallWays.Add(hallway1);
            //TODO: Remove wall at splitPoint
        }
    }
}
