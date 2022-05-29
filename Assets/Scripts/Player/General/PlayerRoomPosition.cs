using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomPosition : MonoBehaviour
{
    RoomPos[] _roomPos;
    Transform _player;
    [SerializeField] bool isActive = true;
    [SerializeField] int _showRoom;
    private void Awake()
    {
        GetRooms();
        _player = GameObject.Find("Player").transform;
        StartCoroutine(SlowUpdate(2.15f));
    }

    void GetRooms()
    {
        Transform[] rooms = gameObject.GetComponentsInChildren<Transform>();
        _roomPos = new RoomPos[rooms.Length];

        int i = 0;
        foreach (Transform room in rooms)
        {
            if (!room.gameObject.name.Contains("room")) return;
            Vector3 pos1 = room.GetChild(0).transform.position;
            Vector3 pos2 = room.GetChild(room.childCount-1).transform.position;

            _roomPos[i].Pos1 = new Vector2(pos1.x, pos1.y);
            _roomPos[i].Pos2 = new Vector2(pos2.x, pos2.y);

            i++;
        }
    }

    public int CheckRoom()
    {
        float posX = _player.transform.position.x;
        float posY = _player.transform.position.y;
        for (int i = 0; i < _roomPos.Length; i++)
        {
            if(_roomPos[i].Pos1.x >= posX && _roomPos[i].Pos2.x <= posX &&
               _roomPos[i].Pos1.y >= posY && _roomPos[i].Pos2.y <= posY)
            {
                _showRoom = i;
                return i;
            }
        }
        _showRoom = -1;
        return -1;
    }

    IEnumerator SlowUpdate(float seconds)
    {
        while(isActive)
        {
            GlobalValues.PlayerCurrRoom = CheckRoom();
            yield return new WaitForSeconds(seconds);
        }
    }
}

struct RoomPos
{
    public Vector2 Pos1 { get; set; }
    public Vector2 Pos2 { get; set; }
}
