using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MapGeneration
{
    public class HallWay : Room
    {
        public HallWay(GameObject ground, GameObject border,
    int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Ground = ground;
            Border = border;
            Width = width;
            Height = height;
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Border);
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
                    rotation = RandomlyOffsetRotation();
                    Tiles[x, y] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                }
                posX = X;
                posY++;
            }
        }
    }
}
