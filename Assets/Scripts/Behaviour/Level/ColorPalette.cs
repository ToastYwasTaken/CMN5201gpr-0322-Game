using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ColorPalette", menuName = "ColorPalette", order = 100)]
public class ColorPalette : ScriptableObject
{
    [SerializeField] Color[] colorsFloor;
    [SerializeField] Color[] colorsObstacle;
    [SerializeField] Color[] colorsWall;
    [SerializeField] Color[] colorsDoor;
    [SerializeField] Color[] colorsEnemy;
    [SerializeField] Color[] colorsBarrel;
    [SerializeField] GameObject[] _floors;
    [SerializeField] GameObject[] _obstacles;

    public Color[] ColorsFloor { get { return colorsFloor; } }
    public Color[] ColorsObstacle { get { return colorsObstacle; } }
    public Color[] ColorsWall { get { return colorsWall; } }
    public Color[] ColorsDoor { get { return colorsDoor; } }
    public Color[] ColorsEnemy { get { return colorsEnemy; } }
    public Color[] ColorsBarrel { get { return colorsBarrel; } }
    public GameObject[] Floors { get { return _floors; } }
    public GameObject[] Obstacles { get { return _obstacles; } }
}
