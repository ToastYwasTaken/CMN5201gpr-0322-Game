using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiggerDenotation
{
    Greater,
    Greater_Equal,
    Less,
    Less_Equal,
    Equal,
    Not_Equal
}

public abstract class OverdriveTrigger : ScriptableObject
{
    [SerializeField] protected bool intervetd = false;

    public virtual bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return true;
    }    

    public virtual void SetupEventTriggers()
    {

    }

    public virtual void RemoveEventTriggers()
    {

    }
}
