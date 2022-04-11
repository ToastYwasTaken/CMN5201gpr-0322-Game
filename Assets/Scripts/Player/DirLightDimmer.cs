using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DirLightDimmer : MonoBehaviour
{
    Light2D mLight;

    private void Awake()
    {
        mLight = GetComponent<Light2D>();
    }

    private void FixedUpdate()
    {
        mLight.intensity = 1 - Mathf.Clamp01(ReferenceLib.sPlayerCtrl.AngleDifferenceToTarget(transform, true) / 180);
    }
}
