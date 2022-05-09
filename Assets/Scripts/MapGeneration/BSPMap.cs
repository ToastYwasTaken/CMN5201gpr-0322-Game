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
        public static List<Room> s_roomsList = new List<Room>();
        private static int s_MIN_PARTITION_WIDTH;
        private static int s_MIN_PARTITION_HEIGHT;
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
            }
            else
            //Create room here
            {
                Room currentRoom = new NormalRoom(ground, wall, border, X, Y, Width, Height);
                s_roomsList.Add(currentRoom);
            }
        }
    }
}
