using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgFlash : MonoBehaviour
{
    [SerializeField] DmgFlash[] mirrorSprites;
    SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } }

    [SerializeField] float _intervaMin = 0.02f, _intervaMax = 0.04f;
    [SerializeField] float _time = 0.20f;
    [SerializeField] bool _finalState = true; 

    [SerializeField] Transform _movePos;
    [SerializeField] float _lerpSpeed;

    [SerializeField] EntityStats _entityStats;

    delegate void RoutineDone();
    event RoutineDone OnRoutineDone;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_entityStats != null)
        {
            _entityStats.OnHealthDamageTaken += DamageFlash;
            _entityStats.OnArmorDamageTaken += DamageFlash;
            _entityStats.OnDeath += DeathFlash;
        }
            
    }

    [SerializeField] bool test;

    private void FixedUpdate()
    {
        if (test)
        {
            test = false;
            StartCoroutine(Flash(_intervaMin, _intervaMax, _time, _finalState));
            foreach (DmgFlash sprite in mirrorSprites)
            {
                sprite.StartFlash(_finalState);
            }
        }
    }
    void DeathFlash()
    {
        OnRoutineDone += DestroySelf;
        StartCoroutine(Flash(_intervaMin, _intervaMax, _time, false));
    }
    void DamageFlash(float foo, bool bar)
    {
        StartCoroutine(Flash(_intervaMin, _intervaMax, _time, true));
    }
    void DestroySelf()
    {
        Destroy(transform.root.gameObject);
    }

    public void StartFlash(bool finalState)
    {
        StartCoroutine(Flash(_intervaMin, _intervaMax, _time, finalState));
    }

    IEnumerator Flash(float intervalMin, float intervalMax, float time, bool finalStatus)
    {
        if (mirrorSprites!=null)
            foreach (DmgFlash sprite in mirrorSprites)
                sprite.gameObject.SetActive(true);

        while (time > 0)
        {
            float interval = Random.Range(intervalMin, intervalMax);
            time -= interval;
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(interval);
        }
        _spriteRenderer.enabled = finalStatus;

        if(mirrorSprites!=null)
            foreach(DmgFlash sprite in mirrorSprites)
                sprite.gameObject.SetActive(finalStatus);

        OnRoutineDone?.Invoke();
        yield return null;
    }
    IEnumerator Move()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = _movePos.position;
        float currLerp = 0.1f;
        while(currLerp < 1f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, currLerp);
            currLerp += _lerpSpeed;
            yield return new WaitForFixedUpdate();
        }
        while (currLerp > 0f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, currLerp);
            currLerp -= _lerpSpeed;
            yield return new WaitForFixedUpdate();
        }
        transform.position = startPos;
    }
}
