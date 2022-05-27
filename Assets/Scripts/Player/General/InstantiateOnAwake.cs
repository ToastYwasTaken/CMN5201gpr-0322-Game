using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnAwake : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    private void Awake()
    {
        GameObject go = Instantiate(_gameObject);
        go.transform.position = transform.position;
        go.transform.SetParent(transform.root);
    }
}
