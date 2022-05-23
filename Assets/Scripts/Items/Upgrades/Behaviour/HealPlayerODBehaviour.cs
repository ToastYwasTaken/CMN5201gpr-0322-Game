using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour", menuName = "Items/Overdrive_Chips/Behaviour/Heal_Player", order = 100)]
public class HealPlayerODBehaviour : OverdriveBehaviour
{
    [SerializeField] private float _amountToHeal = 0;

    public override void UseOverdriveEffect(PlayerInformation playerInformation)
    {
        playerInformation.PlayerStats.Health += _amountToHeal;
    }
}