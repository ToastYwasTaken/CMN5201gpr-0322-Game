using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a non-boss room
/// multiple are instantiated in Level.cs
/// </summary>

namespace Assets.Scripts.MapGeneration
{
    public class NormalRoom : Room
    {
        public NormalRoom(GameObject empty, GameObject ground, GameObject wall, GameObject border,
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
            //Half the size or higher than original partition of BSPMap
            HeightOffset = height / 2;
            WidthOffset = width / 2;
            //Sets new width / height
            RandomlyOffsetRoomSize();
            NormalizePrefabSize(Ground);
            NormalizePrefabSize(Wall);
            NormalizePrefabSize(Border);
            //Create room
            InitRoom();
            Debug.Log($"Created new room : [X : {X} | Y : {Y} | Width: {Width} | Height : {Height} ]");
        }


    }
}
