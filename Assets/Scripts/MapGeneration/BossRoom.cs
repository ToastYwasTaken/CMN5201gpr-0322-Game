using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates one boss room for the level
/// </summary>

namespace Assets.Scripts.MapGeneration
{
    public class BossRoom : Room
    {
        public BossRoom(GameObject ground, GameObject[] obstacles, GameObject wall, GameObject corner, int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Ground = ground;
            Obstacles = obstacles;
            Wall = wall;
            Corner = corner;
            Width = width;
            Height = height;
            NormalizePrefabSize(Ground);
            foreach (var item in obstacles)
            {
            NormalizePrefabSize(item);
            }
            NormalizePrefabSize(Wall);
            NormalizePrefabSize(Corner);
            //Create room
            InitRoom();
            //Debug.Log($"Created new BOSS room : [X : {X} | Y : {Y} | Width: {Width} | Height : {Height} ]");
        }


    }
}
