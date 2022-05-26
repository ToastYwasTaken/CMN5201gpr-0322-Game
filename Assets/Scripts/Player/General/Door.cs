using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    [SerializeField] GameObject DoorGO;
    [SerializeField] bool _isOpenByTrigger;

    private void Awake()
    {
        GameObject door = GetComponentInChildren<DoorTrigger>().gameObject;
        door.SetActive(_isOpenByTrigger);
    }
    public void OpenDoor() 
    {
        DoorGO.SetActive(false);
    }
    public void CloseDoor()
    {
        DoorGO.SetActive(true);
    }
}

public interface IDoor
{
    public void OpenDoor();
    public void CloseDoor();
}
