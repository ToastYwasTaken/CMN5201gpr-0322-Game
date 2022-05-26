using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup Action", menuName = "Entities/Pickupables/Pickup Action/Restore Health", order = 100)]
public class RestoreHealthPickupAction : PickpupAction
{
    [SerializeField] private float _restoreHealthAmount;

    public override bool PerformAction(Collider2D collision) 
    {
       IRestoreHealth restoreHealth = collision.GetComponent<IRestoreHealth>();
        if (restoreHealth != null)
        {
            restoreHealth.RestoreHealth(_restoreHealthAmount);
            return true;
        }
        else return false;
    }
}
