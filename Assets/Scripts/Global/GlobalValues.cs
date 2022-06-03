using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GlobalValues
{
    static public int sDoorTriggerNum = 0;
    static public int sPlayerCurrRoom = 0;
    static public int sCurrentLevel = 0;
    static public bool sIsEnemyAggro = false;
    static public bool sIsPlayerActive = true;
    static public Dictionary<Vector2, GameObject> sDoorByPos;
}
