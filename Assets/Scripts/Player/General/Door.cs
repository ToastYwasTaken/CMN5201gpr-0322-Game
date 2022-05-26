using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    [SerializeField] GameObject DoorGO;
    public void OpenDoor() 
    {
        DoorGO.SetActive(false);
    }
    public void CloseDoor()
    {
        DoorGO.SetActive(true);
    }
}

interface IDoor
{
    public void OpenDoor();
    public void CloseDoor();
}
