using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] ColorPalette[] _palette;
    public int CurrentPalette { get { return GlobalValues.sCurrentLevel % _palette.Length; }}
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
