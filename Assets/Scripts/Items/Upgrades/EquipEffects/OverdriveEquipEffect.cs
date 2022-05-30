using UnityEngine;

public abstract class OverdriveEquipEffect : ScriptableObject
{
    public abstract void ActivateEffects(PlayerInformation playerInformation);
    public abstract void DeactivateEffects(PlayerInformation playerInformation);
}
