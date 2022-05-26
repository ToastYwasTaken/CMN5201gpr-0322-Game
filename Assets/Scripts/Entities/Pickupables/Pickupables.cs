using UnityEngine;

public class Pickupables : MonoBehaviour
{
    [SerializeField] private PickpupAction _pickpupAction;
    [SerializeField] private PickupCheck _pickupCheck;

    private void Awake()
    {
        if(_pickupCheck == null) Debug.LogWarning("Pickup check was not set!" + this.gameObject.name);
        if (_pickpupAction == null) Debug.LogWarning("Pickup action was not set!" + this.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
        if (_pickpupAction == null) return;
        if (_pickupCheck == null) return;

        if (_pickupCheck.CheckPickup(collision))
            if (_pickpupAction.PerformAction(collision)) DestroyPickupables();
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