using System;
using UnityEngine;

//Based on: https://gamedevelopment.tutsplus.com/de/tutorials/how-to-use-bsp-trees-to-generate-game-maps--gamedev-12268

namespace Assets.Scripts.MapGeneration
{
    public class Map
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public Map FirstMap, SecondMap;
        //protected Room room;
        private static int s_MIN_MAP_WIDTH;
        private static int s_MIN_MAP_HEIGHT;
        private static int s_MAX_SIZE;
        public Map(int x, int y, int maxWidth, int maxHeight)
        {
            X = x;
            Y = y;
            Width = maxWidth;
            Height = maxHeight;
            Debug.Log($"Creating new Map: X: [{X}] Y: [{Y}] Width: [{Width}] Height: [{Height}] ");
        }

        public static void AssignMinAndMaxValues(int minMapWidth, int minMapHeight, int maxSize)
        {
            s_MIN_MAP_WIDTH = minMapWidth;
            s_MIN_MAP_HEIGHT = minMapHeight;
            s_MAX_SIZE = maxSize;
        } 

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
            int maxLength = splitHorizontal ? (Height - MIN_MAP_HEIGHT) : (Width - MIN_MAP_WIDTH);
            //Don't split if not enough space
            if (maxLength <= MIN_MAP_WIDTH ||maxLength <= MIN_MAP_HEIGHT)
            {
                return false;
            }
            //Splitting horizontaly
            if (splitHorizontal)
            {
                randomPositionOnHorizontal = rnd.Next(MIN_MAP_HEIGHT, maxLength);
                FirstMap = new Map(X, Y, Width, randomPositionOnHorizontal);
                SecondMap = new Map(X, Y + randomPositionOnHorizontal, Width, Height - randomPositionOnHorizontal);
            }
            //Splitting vertically
            else
            {
                randomPositionOnVertical = rnd.Next(MIN_MAP_WIDTH, maxLength);
                FirstMap = new Map(X, Y, randomPositionOnVertical, Height);
                SecondMap = new Map(X + randomPositionOnVertical, Y, Width - randomPositionOnVertical, Height);
            }
            return true;
        }
    }
}
