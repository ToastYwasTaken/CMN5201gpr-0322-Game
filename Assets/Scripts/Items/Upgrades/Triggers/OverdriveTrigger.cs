/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : OverdriveTrigger.cs
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
