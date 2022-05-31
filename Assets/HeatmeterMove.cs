using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmeterMove : MonoBehaviour
{
    [SerializeField] Transform _player;
    private void FixedUpdate()
    {
        transform.position = _player.position;
    }
}
