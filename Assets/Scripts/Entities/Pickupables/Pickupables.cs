using UnityEngine;


[CreateAssetMenu(fileName = "New Pickupable", menuName = "Entities/Pickupables/Pickupable", order = 100)]
public class Pickupables : Item
{
    [SerializeField] private PickpupAction _pickpupAction;
    public PickpupAction PickpupAction { get => _pickpupAction; }

    [SerializeField] private PickupCheck _pickupCheck;
    public PickupCheck PickupCheck { get => _pickupCheck; }

    [SerializeField] private Color _pickupColor;
    public Color PickupColor { get => _pickupColor; }
}