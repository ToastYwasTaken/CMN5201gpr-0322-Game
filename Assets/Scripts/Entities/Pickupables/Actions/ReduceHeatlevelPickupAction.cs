using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup Action", menuName = "Entities/Pickupables/Pickup Action/Reduce Heatlevel", order = 100)]
public class ReduceHeatlevelPickupAction : PickpupAction
{
    [SerializeField] private float _reduceHeatlevelAmount;

    public override bool PerformAction(Collider2D collision)
    {
        IReduceHeatlevel reduceHeatlevel = collision.GetComponent<IReduceHeatlevel>();
        if (reduceHeatlevel != null)
        {
            reduceHeatlevel.ReduceHeatlevel(_reduceHeatlevelAmount);
            return true;
        }
        else return false;
    }
}
