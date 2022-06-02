using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] ColorPalette[] _palette;
    [SerializeField] int _currentPalette = 0;
    public int CurrentPalette { get { return _currentPalette; } set { _currentPalette = value; } }
    public ColorPalette Palette { get { return GetCurrentPalette(); } }

    ColorPalette GetCurrentPalette()
    {
        return _palette[CurrentPalette];
    }

    private void Awake()
    {
        RefLib.sLevelSettings = this;
    }

    //item num / enemy num / enemy stats
}
