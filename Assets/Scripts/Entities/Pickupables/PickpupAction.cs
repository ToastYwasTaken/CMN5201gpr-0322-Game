using UnityEngine;

public abstract class PickpupAction : ScriptableObject
{
    public abstract bool PerformAction(Collider2D collision);
}
