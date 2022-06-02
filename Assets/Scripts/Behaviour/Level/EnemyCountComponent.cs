using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCountComponent : MonoBehaviour
{
    int _roomNum;
    public int roomNum { set { _roomNum = value; } }

    [SerializeField] EntityStats _stats;

    private void Awake()
    {
        _stats.OnDeath += DecreaseCounter;
    }
    void DecreaseCounter()
    {
        RefLib.sEnemyCount.Count[_roomNum]--;
    }
}
