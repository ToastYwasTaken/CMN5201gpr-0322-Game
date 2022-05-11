using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MapGeneration
{
    public class HallWay : Room
    {
        public HallWay(GameObject ground, GameObject wall, GameObject border,
    int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Ground = ground;
            Wall = wall;
            Border = border;
            Width = width;
            Height = height;
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Wall);
            NormalizePrefabSize(Border);
            //Create room
            InitRoom();
            Debug.Log($"Created new hallway : [X : {X} | Y : {Y} | Width: {Width} | Height : {Height} ]");
        }
        protected override void InitRoom()
        {

        }
    }
}
