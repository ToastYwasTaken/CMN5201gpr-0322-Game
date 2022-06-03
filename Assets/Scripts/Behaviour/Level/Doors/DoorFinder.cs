using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFinder : MonoBehaviour
{
    public int Room1 { private get; set; }
    public int Room2 { private get; set; }
    public bool IsActive { private get; set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collis");
        if (collision == null || !IsActive) return;
        RefLib.sDoorManager.AddDoor(Room1, collision.transform.parent.GetComponent<IDoor>());
        print(collision.transform.parent.name);
        IsActive = false;
    }
}
