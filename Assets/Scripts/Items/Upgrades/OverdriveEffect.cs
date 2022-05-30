using UnityEngine;

public abstract class OverdriveEffect : ScriptableObject
{
    public abstract void ActivateEffects(PlayerInformation playerInformation);
    public abstract void DeactivateEffects(PlayerInformation playerInformation);
}
