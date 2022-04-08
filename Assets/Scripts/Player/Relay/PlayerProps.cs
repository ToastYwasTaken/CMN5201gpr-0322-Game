using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Relay
{
    public interface PlayerProps
    {
        public float AngleDifferenceToTarget(Transform _target, bool _isAbsolut);
    }
}