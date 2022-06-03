using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour", menuName = "Items/Overdrive_Chips/Behaviour/Reduce Armor", order = 100)]
public class ReduceArmorODB : OverdriveBehaviour
{
    [SerializeField] private float amountToReduce;

    public override void UseOverdriveEffect(PlayerInformation playerInformation)
    {
        playerInformation.PlayerStats.Armor -= amountToReduce;
    }
}
