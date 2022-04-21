using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OverdriveTrigger : ScriptableObject
{
    public bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return false;
    }

    public void SetupEventTriggers()
    {

    }

    public void RemoveEventTriggers()
    {

    }
}
