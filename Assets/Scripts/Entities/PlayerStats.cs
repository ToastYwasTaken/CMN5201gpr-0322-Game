/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : PlayerStats.cs
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

public class PlayerStats : EntityStats, IRestoreHealth, IRestoreArmor
{
    public void RestoreArmor(float amountToRestore)
    {
        if (amountToRestore > 0) Armor += amountToRestore;
    }

    public void RestoreHealth(float amountToRestore)
    {
        if (amountToRestore > 0) Health += amountToRestore;
    }

    protected override void Death()
    {
        base.Death();
    }
}
