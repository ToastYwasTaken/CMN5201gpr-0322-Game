using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

public class DmgSegment : MonoBehaviour, IHealth
{
    [SerializeField] SpriteMask _mask;
    [SerializeField] Collider2D _collider;
    [SerializeField] float _health;

    public void ChangeHealth(float _amount)
    {
        print("hit");
        _health -= _amount;
        if(_health <= 0)
        {
            _mask.enabled = true;
            _collider.enabled = false;
        }
    }

    private void Awake()
    {
        _mask.enabled = false;
    }
}
