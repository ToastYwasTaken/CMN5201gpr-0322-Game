using Assets.Scripts.Player.Relay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DirLightDimmer : MonoBehaviour
{
    Light2D mLight;
    [SerializeField] float angle;

    private void Awake()
    {
        mLight = GetComponent<Light2D>();
    }

    private void FixedUpdate()
    {
        angle = ReferenceLib.sPlayerCtrl.AngleDifferenceToTarget(transform, true);
        mLight.intensity = 1 - Mathf.Clamp01(ReferenceLib.sPlayerCtrl.AngleDifferenceToTarget(transform, true) / 360);
    }
}
