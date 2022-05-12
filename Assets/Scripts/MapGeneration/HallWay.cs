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
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    rotation = RandomlyOffsetRotation();
                    Tiles[j, i] = new Tile(Ground, new Vector3(posX++, posY, 0), rotation);
                }
                posX = X;
                posY++;
            }
        }
    }
}
