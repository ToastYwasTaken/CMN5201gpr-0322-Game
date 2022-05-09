using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (UNITY_EDITOR)
/// <summary>
/// Unity Editor only script
/// Only to see the noise applied for room generation
/// </summary>

namespace MapGeneration
{
    public class PerlinNoiseGenerator : MonoBehaviour
    {
        [Tooltip("Resolution width")]
        [SerializeField, Range(1, 4000)]
        int _width = 256;
        [Tooltip("Resolution height")]
        [SerializeField, Range(1, 4000)]
        int _height = 256;
        //the higher the darker
        [SerializeField, Range(0.0f, 100.0f)]
        float _perlinNoiseIntensity = 1f;
        [SerializeField, Range(0.1f, 0.9f)]
        float _perlinNoiseOffsetX = 0.35f;
        [SerializeField, Range(0.1f, 0.9f)]
        float _perlinNoiseOffsetY = 0.35f;
        //the higher the bigger
        [SerializeField, Range(1.0f, 100f)]
        float _perlinNoiseScale = 5.0f;

        private Renderer _rend;

        private static PerlinNoiseGenerator _instance;


        void Start()
        {
            GetInstance();
            _rend = GetComponent<Renderer>();
        }

        void Update()
        {
            _rend.material.mainTexture = CreateTexture(_width, _height);
        }

        private static PerlinNoiseGenerator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PerlinNoiseGenerator();
            }
            return _instance;
        }

        private Texture2D CreateTexture(int width, int height)
        {
            Texture2D tex = new Texture2D(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = GenerateColor(x, y);
                    tex.SetPixel(x, y, color);
                }
            }
            tex.Apply();
            return tex;
        }

        private Color GenerateColor(int x, int y)
        {
            float perlinNoise = Mathf.PerlinNoise((x * _perlinNoiseOffsetX) / _perlinNoiseScale, (y * _perlinNoiseOffsetY) /_perlinNoiseScale) / _perlinNoiseIntensity;
            return new Color(perlinNoise, perlinNoise, perlinNoise);
        }
    }
}
#endif