/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : OverdriveFloatTrigger.cs
* Date   : 03.06.2022
* Author : Alexander Sigmund (AS)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*
******************************************************************************/
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
