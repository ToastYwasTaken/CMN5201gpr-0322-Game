using System.Collections;
using System.Collections.Generic;
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
            return new Vector2(_transform.position.x, _transform.transform.position.y);
        }

        public static Vector2 ToVector2(this Vector3 _vector3)
        {
            return new Vector2(_vector3.x, _vector3.y);
        }

        public static float AngleWrap(this float _angle)
        {
            return _angle < 0 ? 360 + _angle : _angle > 360 ? 0 + _angle : _angle;
        }

    }
}
