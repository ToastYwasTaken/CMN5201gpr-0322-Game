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
        public BossRoom(GameObject empty, GameObject ground, GameObject wall, GameObject border,
         int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Empty = empty;
            Ground = ground;
            Wall = wall;
            Border = border;
            Width = width;
            Height = height;
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Wall);
            NormalizePrefabSize(Border);
            RandomlyOffsetPrefabColorBossRoom(Ground);
            RandomlyOffsetPrefabColorBossRoom(Wall);
            RandomlyOffsetPrefabColorBossRoom(Border);
            //Create room
            InitRoom();
            Debug.Log($"Created new BOSS room : [X : {X} | Y : {Y} | Width: {Width} | Height : {Height} ]");
        }


        private void RandomlyOffsetPrefabColorBossRoom(GameObject prefab)
        {
            Renderer prefabRend = prefab.GetComponent<Renderer>();
            prefabRend.sharedMaterial.color = new Color32(220, 110, 210, 255);
        }
    }
}
