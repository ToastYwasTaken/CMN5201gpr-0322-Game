using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 0.5f;
    [SerializeField] private Vector3 _offset = Vector3.zero;
    [SerializeField] private Vector3 _randomizeIntesnity = new Vector3(0.5f, 0f, 0f);

    private void Start()
    {
        Destroy(this.gameObject, _destroyDelay);
        transform.localPosition += _offset;
        transform.localPosition += new Vector3(Random.Range(-_randomizeIntesnity.x, _randomizeIntesnity.x),
                                               Random.Range(-_randomizeIntesnity.y, _randomizeIntesnity.y),
                                               Random.Range(-_randomizeIntesnity.z, _randomizeIntesnity.z));        
    }

    public void Crit(bool didCrit)
    {
        transform.localScale = new Vector3(2f, 2f, 1f);
    }
}
