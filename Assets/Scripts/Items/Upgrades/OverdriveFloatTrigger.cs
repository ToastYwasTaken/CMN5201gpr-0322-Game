using UnityEngine;

public abstract class OverdriveFloatTrigger : OverdriveTrigger
{
    [SerializeField] protected TiggerDenotation checkOperator = TiggerDenotation.Equal;

    [SerializeField] protected float checkValue;

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

}
