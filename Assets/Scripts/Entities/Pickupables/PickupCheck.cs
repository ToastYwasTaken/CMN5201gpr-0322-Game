using UnityEngine;

public abstract class PickupCheck : ScriptableObject
{
    public abstract bool CheckPickup(Collider2D collision);
}
