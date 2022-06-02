/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : Rotateable.cs
* Date   : 17.04.22
* Author : Jan Apsel (JA)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*   25.4.22 JA created 
******************************************************************************/

using UnityEngine;

namespace AngleExtension
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Translate Vector2 to Angle
        /// </summary>
        public static float GetAngle(this Vector2 _vector2)
        {
            return Mathf.Atan2(_vector2.x, _vector2.y) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Translate Transform to Vector2
        /// </summary>
        public static Vector2 ToVector2(this Transform _transform)
        {
            return new Vector2(_transform.position.x, _transform.position.y);
        }

        public static Vector2 ToVector2(this Vector3 _vector3)
        {
            return new Vector2(_vector3.x, _vector3.y);
        }
        public static Vector3 ToVector3(this Vector2 _vector2)
        {
            return new Vector3(_vector2.x, _vector2.y, 0);
        }

        public static float AngleWrap(this float _angle)
        {
            return _angle < 0 ? 360 + _angle : _angle > 360 ? 0 + _angle : _angle;
        }

    }
}
