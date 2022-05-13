using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

public class DmgSegment : MonoBehaviour, IHealth
{
    [SerializeField] SpriteMask[] _masks;
    [SerializeField][Range(0f,1f)] float[] _percentage;
    [SerializeField] Collider2D _collider;
    [SerializeField] float _health;
    [SerializeField] bool _criticalComponent;
    [SerializeField] GameObject[] _destroyOnDeath;
    float _currHealth;

    public void ChangeHealth(float _amount)
    {
        _currHealth -= _amount;
        for (int i = 0; i < _percentage.Length; i++)
        {
            if(_currHealth < _health * _percentage[i])
            {
                _masks[i].enabled = true;
            }
        }
        if(_currHealth <= 0)
        {
            _collider.enabled = false;
            if (_criticalComponent) Destroy(transform.root.gameObject);
        }
    }

    private void Awake()
    {
        foreach (SpriteMask m in _masks)
            m.enabled = false;
        _currHealth = _health;
    }
}
