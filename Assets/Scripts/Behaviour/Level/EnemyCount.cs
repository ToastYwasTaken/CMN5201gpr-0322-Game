using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    int[] _count;
    public int[] Count { get { return _count; } set { _count = value; } }

    private void Awake()
    {
        RefLib.sEnemyCount = this;
    }
}
