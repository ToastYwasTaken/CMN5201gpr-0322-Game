using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup Check", menuName = "Entities/Pickupables/Pickup Check/Entity Check", order = 100)]
public class EntityPickupCheck : PickupCheck
{
    [SerializeField] private eEntityType _allowedToPickup;

    public override bool CheckPickup(Collider2D collision)
    {
        IReturnEntityType hittedObjectIType = collision.GetComponent<IReturnEntityType>();

        eEntityType hittedType;
        if (hittedObjectIType != null) hittedType = hittedObjectIType.GetEntityType();
        else return false;

        if (hittedType == _allowedToPickup) return true;
        else return false;
    }
}
