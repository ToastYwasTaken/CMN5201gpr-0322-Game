using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityStats
{
    protected override void Death()
    {
        Destroy(this.gameObject);
    }
}
