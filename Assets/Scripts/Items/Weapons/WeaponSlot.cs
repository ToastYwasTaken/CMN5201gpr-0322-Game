using UnityEngine;

public class WeaponSlot : Object
{
    [SerializeField] private Weapon _weaponItem;
    public Weapon WeaponItem
    {
        get => _weaponItem;
        set
        {
            if (value == null) return;

            if (_weaponInstance != null) _weaponInstance.OnUnequip();

            _weaponItem = value;
            _weaponInstance = Instantiate(value);

            if (_weaponInstance != null) _weaponInstance.OnEquip();
        }
    }

    private Weapon _weaponInstance;
    public Weapon WeaponInstance => _weaponInstance;

    public WeaponSlot(Weapon defaultWeapon)
    {
        WeaponItem = defaultWeapon;
    }
}
