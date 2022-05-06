using System;
<<<<<<< Updated upstream
using UnityEngine;

//Based on: https://gamedevelopment.tutsplus.com/de/tutorials/how-to-use-bsp-trees-to-generate-game-maps--gamedev-12268
=======
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.MapGeneration;
>>>>>>> Stashed changes

namespace Assets.Scripts.MapGeneration
{
    public class Map
    {
<<<<<<< Updated upstream
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
=======
        protected int Width;
        protected int Height;
        protected int HeightOffset;
        protected int WidthOffset;
        protected Vector3[,] MapPositions;
        public List<BSPPartition> Partitions = new List<BSPPartition>();
        public Map(int maxWidth, int maxHeight)
        {
            Width = maxWidth;
            Height = maxHeight;
            HeightOffset = (int)(Height * 0.2f);
            WidthOffset = (int)(Width * 0.2f);
            MapPositions = new Vector3[Width, Height];
            AssignPositions();
            int breakCount = 0;
            while(Width > 20 && Height > 20)
            {
                Debug.Log("Creating BSP map");
                CreateBSPMap();
                breakCount++;
                if(breakCount == 100)
                {
                    break;
                }
            }
            DebugLog();
        }

        private void AssignPositions()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    MapPositions[i, j] = new Vector3(i, j, 0);
                }
            }
        }

        private void DebugLog()
        {
            for (int i = 0; i < Partitions.Count; i++)
            {
                Debug.Log($"Partition {i}:");
                for (int j = 0; j < Partitions[i].MapPositions.Length; j++)
                {
                    for (int k = 0; k < Partitions[i].MapPositions.Length; k++)
                    {
                        Debug.Log($"Map[{j}|{k}]");
                    }
                }
            }

        }

        private void CreateBSPMap()
        {
            System.Random rnd = new System.Random();
            int randomPositionOnHorizontal, randomPositionOnVertical;
            int rndInt = rnd.Next(1, 3);
            BSPPartition partition;
            //Splitting horizontally at random
            if (rndInt == 0)
            {
                randomPositionOnHorizontal = rnd.Next(WidthOffset, Width - WidthOffset);
                partition = new BSPPartition(randomPositionOnHorizontal, Width, 0, Height);
                Partitions.Add(partition);
                partition = new BSPPartition(0, randomPositionOnHorizontal, 0, Height);
                Partitions.Add(partition);
            }
            //Splitting vertically
            else if(rndInt == 1)
            {
                randomPositionOnVertical = rnd.Next(HeightOffset, Height - HeightOffset);
                partition = new BSPPartition(0, Width, randomPositionOnVertical, Height);
                Partitions.Add(partition);
                partition = new BSPPartition(0, Width, 0, randomPositionOnVertical);
                Partitions.Add(partition);
            }
        }
    }

    public class BSPPartition
    {
        protected int Width;
        protected int Height;
        protected int MinWidth;
        protected int MinHeight;
        public Vector3[,] MapPositions;

        public BSPPartition(int minWidth, int maxWidth, int minHeight, int maxHeight)
        {
            Width = maxWidth;
            MinWidth = minWidth;
            Height = maxHeight;
            MinHeight = minHeight;
            MapPositions = new Vector3[Width, Height];
            AssignPositions();
        }

        private void AssignPositions()
        {
            if(MapPositions.LongLength > 0)
            {
            Array.Clear(MapPositions, 0, MapPositions.Length);
            }
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    MapPositions[i, j] = new Vector3(i+MinWidth, j+MinHeight, 0);
                }
            }
        }

    }


>>>>>>> Stashed changes
}
