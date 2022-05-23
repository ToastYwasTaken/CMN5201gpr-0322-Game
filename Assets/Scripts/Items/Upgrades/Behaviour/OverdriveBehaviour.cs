using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OverdriveBehaviour : ScriptableObject
{
    public virtual void UseOverdriveEffect(PlayerInformation playerInformation)
    {

    }
}

public class SummonProjectiles : OverdriveBehaviour
{
    [SerializeField] private int _amountOfBullets;

    public override void UseOverdriveEffect(PlayerInformation playerInformation)
    {
        
    }
}