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
        public BossRoom(GameObject empty, GameObject ground, GameObject wall, GameObject border, GameObject corner, int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Empty = empty;
            Ground = ground;
            Wall = wall;
            Border = border;
            Corner = corner;
            Width = width;
            Height = height;
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Wall);
            NormalizePrefabSize(Border);
            NormalizePrefabSize(Corner);
            RandomlyOffsetPrefabColorBossRoom(Ground);
            RandomlyOffsetPrefabColorBossRoom(Wall);
            RandomlyOffsetPrefabColorBossRoom(Border);
            //Create room
            InitRoom();
            //Debug.Log($"Created new BOSS room : [X : {X} | Y : {Y} | Width: {Width} | Height : {Height} ]");
        }

        /// <summary>
        /// Doesnt work
        /// </summary>
        /// <param name="prefab"></param>
        private void RandomlyOffsetPrefabColorBossRoom(GameObject prefab)
        {
            //Renderer prefabRend = prefab.GetComponent<Renderer>();
            Renderer prefabRend = prefab.GetComponentInChildren<Renderer>();
            prefabRend.sharedMaterial.color = new Color32(220, 110, 210, 255);
        }
    }
}
