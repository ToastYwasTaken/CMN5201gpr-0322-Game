using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeaponEntity : MonoBehaviour
{
    [SerializeField] private Weapon _itemWeapom;
    public Weapon Item { get { return _itemWeapom; } }

    private bool _canBeCLicked = true;

    private float _spamProtectionTimer;
    private float _clickCD = 1;

    public KeyCode WeaponInputSlot1 = KeyCode.Alpha1;
    public KeyCode WeaponInputSlot2 = KeyCode.Alpha2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_canBeCLicked)
        {
            if (_spamProtectionTimer < 0) _canBeCLicked = true;
            else 
            {
                _spamProtectionTimer -= Time.deltaTime;
                return;
            }
        }

        if (Input.GetKeyDown(WeaponInputSlot1))
        {
            IEquipWeapons equipWeapons = collision.GetComponent<IEquipWeapons>();

            if (equipWeapons == null) return;
            else if(equipWeapons.EquipWeapon(_itemWeapom, 0)) Destroy(gameObject);
        }

        if (Input.GetKeyDown(WeaponInputSlot2))
        {
            IEquipWeapons equipWeapons = collision.GetComponent<IEquipWeapons>();

            if (equipWeapons == null) return;
            else if (equipWeapons.EquipWeapon(_itemWeapom, 1)) Destroy(gameObject);
        }
    }
}