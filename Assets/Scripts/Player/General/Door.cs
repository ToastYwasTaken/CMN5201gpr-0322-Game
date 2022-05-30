using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    [SerializeField] GameObject _doorGO;
    [SerializeField] DoorTrigger _doorTrigger;
    [SerializeField] bool _isOpenByTrigger;

    DmgFlash _flash;
    Collider2D _doorCollider;

    private void Awake()
    {
        if (_doorTrigger == null) return;
        _flash = _doorGO.GetComponent<DmgFlash>();
        _doorCollider = _doorGO.GetComponent<Collider2D>();
        _doorTrigger.IsOpen = _isOpenByTrigger;
    }
    public void OpenDoor() 
    {
        _flash.StartFlash(false);
        _doorCollider.enabled = false;
    }
    public void CloseDoor()
    {
        _doorCollider.enabled = true;
        _flash.StartFlash(true);
    }
}

public interface IDoor
{
    public void OpenDoor();
    public void CloseDoor();
}
