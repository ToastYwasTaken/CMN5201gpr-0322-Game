using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour", menuName = "Items/Overdrive_Chips/Behaviour/Reduce Heatlevel", order = 100)]
public class ReduceHeatlevelODB : OverdriveBehaviour
{
    [SerializeField] private float amountToReduce;

    public override void UseOverdriveEffect(PlayerInformation playerInformation) 
    {
        playerInformation.Heatmeter.CurrentHeatlevel -= amountToReduce;
    }
}
