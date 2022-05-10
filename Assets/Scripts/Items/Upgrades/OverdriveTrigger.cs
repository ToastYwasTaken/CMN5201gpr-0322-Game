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

    [SerializeField] protected TiggerDenotation checkOperator = TiggerDenotation.Equal;

    [SerializeField] protected float checkValue;

    public virtual bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return true;
    }

    protected bool CheckTriggers(float triggerToCheck, float value, TiggerDenotation tiggerDenotation)
    {
        bool isValid = false;

        switch (tiggerDenotation)
        {
            case TiggerDenotation.Greater:

                if (triggerToCheck > value) isValid = true;

                break;
            case TiggerDenotation.Greater_Equal:

                if (triggerToCheck >= value) isValid = true;

                break;
            case TiggerDenotation.Less:

                if (triggerToCheck < value) isValid = true;

                break;
            case TiggerDenotation.Less_Equal:

                if (triggerToCheck <= value) isValid = true;

                break;
            case TiggerDenotation.Equal:

                if (triggerToCheck == value) isValid = true;

                break;
            case TiggerDenotation.Not_Equal:

                if (triggerToCheck != value) isValid = true;

                break;
        }

        if (intervetd) return !isValid;
        else return isValid;
    }

    public virtual void SetupEventTriggers()
    {

    }

    public virtual void RemoveEventTriggers()
    {

    }
}
