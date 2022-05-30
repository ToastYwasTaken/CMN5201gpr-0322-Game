using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Generates perlin noise to spawn obstacles in the rooms
/// </summary>

namespace Assets.Scripts.MapGeneration
{
    public class PerlinNoiseGenerator : MonoBehaviour
    {
        private static System.Random _rdm = new System.Random((int)(System.DateTime.Now.Ticks));


        /// <summary>
        /// public accessibility to generate this noise for the rooms
        /// </summary>
        /// <param name="x">xPos</param>
        /// <param name="y">yPos</param>
        /// <returns>perlinNoise at those coordinates</returns>
        public static float GeneratePerlinNoiseAtCoordinates(int x, int y, float perlinNoiseOffsetX, float perlinNoiseOffsetY, float perlinNoiseScale, float perlinNoiseIntensity)
        {
            float perlinNoise = Mathf.PerlinNoise((x * perlinNoiseOffsetX) / perlinNoiseScale, y * perlinNoiseOffsetY /perlinNoiseScale) / perlinNoiseIntensity;
            return perlinNoise;
        }

        public static float RandomFloat(float min, float max)
        {
            return (((float)(_rdm.NextDouble()) * (max - min)) + min);
        }

    }
}