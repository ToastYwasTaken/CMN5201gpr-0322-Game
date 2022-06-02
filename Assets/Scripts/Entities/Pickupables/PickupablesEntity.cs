using UnityEngine;

public class PickupablesEntity : MonoBehaviour
{
    [SerializeField] private Pickupables _pickupables;

    private void OnTriggerEnter2D(Collider2D collision)
{
        if (_pickupables.PickpupAction == null) return;
        if (_pickupables.PickupCheck == null) return;

        if (_pickupables.PickupCheck.CheckPickup(collision))
            if (_pickupables.PickpupAction.PerformAction(collision)) DestroyPickupables();
    }

    private void DestroyPickupables()
    {
        OnDestroy();
    }

    public virtual void OnDestroy()
    {
        Destroy(gameObject);
    }
}
