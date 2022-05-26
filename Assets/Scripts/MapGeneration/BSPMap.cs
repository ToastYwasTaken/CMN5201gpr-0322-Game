using System;
using System.Collections.Generic;
using UnityEngine;

//The general idea bases on: https://gamedevelopment.tutsplus.com/de/tutorials/how-to-use-bsp-trees-to-generate-game-maps--gamedev-12268
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
        public void CreateRooms(GameObject ground, GameObject obstacle1, GameObject obstacle2, GameObject wall, GameObject corner, GameObject door)
        {
            if (FirstMap != null || SecondMap != null)
            {
                //Map has been split, checking lower partitions
                if (FirstMap != null)
                {
                    FirstMap.CreateRooms(ground, obstacle1, obstacle2, wall, corner, door);
                }
                if (SecondMap != null)
                {
                    SecondMap.CreateRooms(ground, obstacle1, obstacle2, wall, corner, door);
                }
                if (FirstMap != null && SecondMap != null)
                {
                    CreateHallWay(FirstMap.GetRoom(), SecondMap.GetRoom(), ground, wall, corner, door); //replace with normal ground once done
                }
            }
            else
            //Lowest partition -> Create room here -> assign room to this partition
            {
                currentRoom = new NormalRoom(ground, obstacle1, obstacle2, wall, corner, X, Y, Width, Height);
                s_allRooms.Add(currentRoom);
            }
        }

        /// <summary>
        /// Hallway creation algorithm
        /// </summary>
        /// <param name="room1">Hallway connects to room2</param>
        /// <param name="room2">Hallway connects to room1</param>
        /// <param name="ground"></param>
        /// <param name="wall"></param>
        private void CreateHallWay(Room room1, Room room2, GameObject ground, GameObject wall, GameObject corner, GameObject door)
        {
            //Calculate possible connection location
            System.Random rdm = new System.Random();
            int rdmInt = rdm.Next(0, 2);
            Room hallway1 = null;
            Room hallway2 = null;

            #region
            //Offsets to ignore borders
            int room1X = room1.X + 1;
            int room1Y = room1.Y + 1;
            int room2X = room2.X + 1;
            int room2Y = room2.Y + 1;
            int rightRoom1 = room1.Width + room1X-3;
            int rightRoom2 = room2.Width + room2X-3;
            int topRoom1 = room1.Height + room1Y-3;
            int topRoom2 = room2.Height + room2Y-3;

            //Debug also ignores borders
            //Debug.Log($"Room1 at [{room1X} | {room1Y}] x | Width - x+Width : [{room1X}|{room1.Width-2} - {rightRoom1}] y | Height - y+Height : [{room1Y}|{room1.Height-2} - {topRoom1}] \nRoom2 at [{room2X} | {room2Y}] x | Width - x+Width : [{room2X}|{room2.Width-2} - {rightRoom2}] y | Height - y+Height : [{room2Y}|{room2.Height-2} - {topRoom2}]");
            //Check if the ranges are overlapping, e.g. Set1(x:2 | x+width:10) and Set2(x:9 |  x+width:12) -> true, 9,10 in both ranges
            bool splitHorizontal = Mathf.Abs(room1Y - room2Y) <= 2; 
            bool splitVertical = Mathf.Abs(room1X - room2X) <= 2;
            //Debug.Log($"{room1Y} {room2Y} {topRoom1} {topRoom2}");
            int randomIntOnRangeHorizontal;
            int randomIntOnRangeVertical;

            Vector2 splitPointRoom1, splitPointRoom2;
            if (splitHorizontal)
            {
                //Connect rooms horizontally
                if (!splitVertical)
                {
                    //Calculating splitPoints
                    randomIntOnRangeHorizontal = rdm.Next(Mathf.Max(room1Y, room2Y), Mathf.Min(topRoom1, topRoom2));
                    splitPointRoom1 = new Vector2(rightRoom1+1, randomIntOnRangeHorizontal); //point on the right of the room
                    splitPointRoom2 = new Vector2(room2X-1, randomIntOnRangeHorizontal);
                    //Debug.Log($"Connect rooms horizontally at: {splitPointRoom1} and {splitPointRoom2}");
                    hallway1 = new HallWay(ground, wall, corner, (int)splitPointRoom1.x, (int)splitPointRoom1.y, (int)(splitPointRoom2.x-splitPointRoom1.x + 1), 3);
                }
                else
                //both overlapping -> Room 'inside' the other room -> shouldn't happen
                {
                    Debug.LogError("Edge case, rooms overlapping");
                    return;
                }
            }
            else
            //horizontally not overlapping
            {
                //Connect rooms vertically
                if (splitVertical)
                {
                    //Calculating splitPoints
                    randomIntOnRangeVertical = rdm.Next(Mathf.Max(room1X, room2X), Mathf.Min(rightRoom1, rightRoom2));
                    splitPointRoom1 = new Vector2(randomIntOnRangeVertical, topRoom1+1);
                    splitPointRoom2 = new Vector2(randomIntOnRangeVertical, room2Y-1);
                    //Debug.Log($"Connect rooms vertically at: {splitPointRoom1} and {splitPointRoom2}");
                    hallway1 = new HallWay(ground, wall, corner, (int)splitPointRoom1.x, (int)splitPointRoom1.y, 3, (int)(splitPointRoom2.y-splitPointRoom1.y + 1));
                }
                else
                //Rooms diagonal
                {
                    Debug.LogError("Edge case, rooms diagonal");
                    return;
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
