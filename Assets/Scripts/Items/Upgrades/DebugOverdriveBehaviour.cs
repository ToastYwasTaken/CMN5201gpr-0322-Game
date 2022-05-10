using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour", menuName = "Items/Overdrive_Chips/Behaviour/Debug", order = 100)]
public class DebugOverdriveBehaviour : OverdriveBehaviour
{
    public override void UseOverdriveEffect(PlayerInformation playerInformation)
    {
        Debug.Log("Overdrive used");
    }
}