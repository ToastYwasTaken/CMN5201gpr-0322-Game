using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _items;
    [SerializeField] int _spawnNum;
    List<Transform> _motherRooms = new List<Transform>();

    List<Transform> _spawnPoints = new List<Transform>();

    private void Start()
    {
        GetMotherRooms();
        GetSpawns();
        SpawnItems();
    }
    void GetMotherRooms()
    {
        Transform map = GameObject.Find("Map").transform;

        for (int i = 0; i < map.childCount; i++)
        {
            if(map.GetChild(i).name.Contains("room") && !map.GetChild(i).name.Contains("boss"))
                _motherRooms.Add(map.GetChild(i).transform);
        }
    }

    void GetSpawns()
    {
        for (int i = 0; i < _motherRooms.Count; i++)
        {
            int spawnNumTmp = 0;
            int tries = 50;
            while(spawnNumTmp < _spawnNum && tries > 0)
            {
                if(TryGetFloorTile(_motherRooms[i]))
                    spawnNumTmp++;
                tries--;
            }
        }
    }

    bool TryGetFloorTile(Transform room)
    {
        int r = Random.Range(0, room.childCount);
        if (room.GetChild(r).name.Contains("Floor"))
        {
            _spawnPoints.Add(room.GetChild(r).transform);
            return true;
        }
        return false;
    }

    void SpawnItems()
    {
        foreach(Transform pos in _spawnPoints)
        {
            GameObject newItem = Instantiate(_items[Random.Range(0, _items.Length)], pos);
            newItem.transform.SetParent(transform);
            newItem.transform.localScale = Vector3.one;
        }
    }
}
