using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgFlashComponent : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    public SpriteRenderer spriteRenderer { get { return _spriteRenderer; } }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
