using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MapGeneration;
using UnityEngine;

public class PlayerRoomPosition : MonoBehaviour
{
    Transform _player;
    [SerializeField] bool isActive = true;
    [SerializeField] int _showRoom;

    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
        StartCoroutine(SlowUpdate(2.15f));
    }

    int CheckRoom()
    {
        for (int i = 0; i < BSPMap.s_allRooms.Count; i++)
            if (_player.transform.position.x > BSPMap.s_allRooms[i].X &&
               _player.transform.position.y > BSPMap.s_allRooms[i].Y &&
               _player.transform.position.x < BSPMap.s_allRooms[i].X + BSPMap.s_allRooms[i].Width &&
               _player.transform.position.y < BSPMap.s_allRooms[i].Y + BSPMap.s_allRooms[i].Height)
                return i;
        return -1;
    }
    IEnumerator SlowUpdate(float seconds)
    {
        while(isActive)
        {
            int currRoom = CheckRoom();
            if (currRoom > -1)
            {
                _showRoom = currRoom;
                GlobalValues.PlayerCurrRoom = currRoom;
            }
            yield return new WaitForSeconds(seconds);
        }
    }
}
