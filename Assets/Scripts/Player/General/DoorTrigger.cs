using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    IDoor _door;
    private void Awake()
    {
        _door = transform.root.GetComponent<IDoor>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _door.OpenDoor();
    }
}
