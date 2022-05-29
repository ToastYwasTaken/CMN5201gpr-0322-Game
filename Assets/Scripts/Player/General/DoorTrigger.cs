using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool IsOpen { private get; set; }
    IDoor _door;
    int _doorNum;

    private void Awake()
    {
        _door = transform.root.GetComponent<IDoor>();
        _doorNum = GlobalValues.doorTriggerNum;
        GlobalValues.doorTriggerNum++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            if (IsOpen)
                _door.OpenDoor();
        }
    }
}
