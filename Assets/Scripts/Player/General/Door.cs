using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    [SerializeField] GameObject _doorGO;
    [SerializeField] DoorTrigger _doorTrigger;
    [SerializeField] bool _isOpenByTrigger;

    private void Awake()
    {
        if (_doorTrigger == null) return;
        _doorTrigger.IsOpen = _isOpenByTrigger;
    }
    public void OpenDoor() 
    {
        _doorGO.SetActive(false);
    }
    public void CloseDoor()
    {
        _doorGO.SetActive(true);
    }
}

public interface IDoor
{
    public void OpenDoor();
    public void CloseDoor();
}
