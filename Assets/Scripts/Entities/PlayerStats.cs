﻿public class PlayerStats : EntityStats, IRestoreHealth, IRestoreArmor
{
    public void RestoreArmor(float amountToRestore)
    {
        if(amountToRestore > 0) Health += amountToRestore;
    }

    public void RestoreHealth(float amountToRestore)
    {
        if (amountToRestore > 0) Armor += amountToRestore;
    }

    protected override void Death()
    {
        base.Death();
    }
}