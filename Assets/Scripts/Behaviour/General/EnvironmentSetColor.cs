using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSetColor : MonoBehaviour
{
    [SerializeField] int _index;
    [SerializeField] eEnvType _envType;
    [SerializeField] SpriteRenderer[] _spriteRenderer;
    LevelSettings _settings;
    private void Awake()
    {
    }
    private void Start()
    {
        _settings = RefLib.sLevelSettings;
        SetColor();
    }

    void SetColor()
    {
        Color[] colors = new Color[0];
        if (_settings == null) return;
        switch (_envType)
        {
            case eEnvType.FLOOR: 
                colors = _settings.Palette.ColorsFloor;
                break;
            case eEnvType.OBSTACLE:
                colors = _settings.Palette.ColorsObstacle;
                break;
            case eEnvType.WALL:
                colors = _settings.Palette.ColorsWall;
                break;
            case eEnvType.DOOR:
                colors = _settings.Palette.ColorsDoor;
                break;
            case eEnvType.ENEMY:
                colors = _settings.Palette.ColorsEnemy;
                break;
        }
        foreach (SpriteRenderer renderer in _spriteRenderer)
        {
            Color tmpColor = colors[_index];
            tmpColor.a = renderer.color.a;
            renderer.color = tmpColor;
        }
    }
}
enum eEnvType
    {
        FLOOR, OBSTACLE, WALL, DOOR, ENEMY
    }
