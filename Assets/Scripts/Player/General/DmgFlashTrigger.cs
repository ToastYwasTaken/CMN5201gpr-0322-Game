using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgFlashTrigger : MonoBehaviour, IDamageable, IReturnEntityType
{
    [SerializeField] DmgFlash _dmgFlash;
    public void DealDamage(float attackPower, float armorPenetration, bool canCrit, float critChance)
    {
        _dmgFlash.StartFlash(true);
    }

    public eEntityType GetEntityType()
    {
        return eEntityType.Environment;
    }
}
