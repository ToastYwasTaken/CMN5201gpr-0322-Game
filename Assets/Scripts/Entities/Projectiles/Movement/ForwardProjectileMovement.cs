/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : ProjectileMovement.cs
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

[CreateAssetMenu(fileName = "New Forward Movement", menuName = "Entities/Projectiles/MovementBehaviour/Forward", order = 100)]
public class ForwardProjectileMovement : ProjectileMovement
{
    public override Vector3 MovementVector(float speed, Transform transform)
    {
        return speed*Time.deltaTime*transform.up;
    }
}
