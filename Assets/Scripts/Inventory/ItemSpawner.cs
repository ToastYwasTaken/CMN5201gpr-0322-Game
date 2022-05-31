using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _items;
    [SerializeField] int _spawnNum;
    [SerializeField] GameObject _player;
    List<Transform> _motherRooms = new List<Transform>();

    List<Transform> _spawnPoints = new List<Transform>();

    private void Start()
    {
        GetMotherRooms();
        GetSpawns();
        SpawnItems();

        _player.GetComponent<PlayerController2>().SpawnPlayer();
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
            int spawnNumTmp = 0 + Random.Range(0, _spawnNum);
            int tries = 50;
            while(spawnNumTmp < _spawnNum && tries > 0)
            {
                if(TryGetFloorTile(_motherRooms[i]))
                    spawnNumTmp++;
                tries--;
            }
        }
    }

    public Transform GetPlayerSpawn()
    {
        int tries = 50;

        while(tries-->0)
        {
            int r = Random.Range(0, _motherRooms[0].childCount);

            if (_motherRooms[0].GetChild(r).name.Contains("Floor"))
                return _motherRooms[0].GetChild(r).transform;
        }
        return null;
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
            newItem.transform.localScale = Vector3.one/4;
        }
    }
}
