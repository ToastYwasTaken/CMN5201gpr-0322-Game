using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup Action", menuName = "Entities/Pickupables/Pickup Action/Restore Armor", order = 100)]
public class RestoreArmorPickupAction : PickpupAction
{
    [SerializeField] private float _restoreArmorAmount;

    public override bool PerformAction(Collider2D collision)
    {
        IRestoreArmor restoreArmor = collision.GetComponent<IRestoreArmor>();
        if (restoreArmor != null)
        {
            restoreArmor.RestoreArmor(_restoreArmorAmount);
            return true;
        }
        else return false;
    }
}
