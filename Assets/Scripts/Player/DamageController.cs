using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngleExtension;
using Assets.Scripts.Player;

public class DamageController : MonoBehaviour, IDmgSegment
{
    int _dmgPointNum = 5;
    float[] collDiv;
    float[] areaHP;
    [SerializeField] float hp;
    [SerializeField] GameObject[] Masks;
    Rotateable _rotateable;

    private void Awake()
    {
        _rotateable = GetComponent<Rotateable>();
        _dmgPointNum = Masks.Length;
        foreach (GameObject m in Masks)
            m.SetActive(false);
        collDiv = new float[_dmgPointNum];
        areaHP = new float[_dmgPointNum];
        for (int i = 0; i < areaHP.Length; i++)
        {
            areaHP[i] = hp;
        }
        float div = 360 / _dmgPointNum;
        for (int i = 0; i < collDiv.Length; i++)
            collDiv[i] = div * (i +1);
    }

    int CheckCollArea(float collPoint)
    {
        float lastDiv = 0;
        for(int i = 0; i < collDiv.Length; i++)
        {
            if (collPoint > lastDiv && collPoint < collDiv[i])
                return i;
            lastDiv = collDiv[i];
        }
        return -1;

    }
    float GetCollision(Vector2 point)
    {
        Vector2 ownPos = transform.position.ToVector2();

        float angle = Vector2.Angle(ownPos, point);

        return AngleWrap(angle);
    }
    float GetCollision(Transform target)
    {
        return _rotateable.AngleDifferenceToTarget(target, false);
    }

    private float AngleWrap(float _angle)
    {
        return _angle < 0 ? 360 + _angle :
               _angle > 360 ? _angle - 360 : _angle;
    }

    public void DmgByPosition(Transform position, float dmg)
    {
        float collAngle = GetCollision(position);
        int areaNum = CheckCollArea(collAngle);
        print(areaNum);
        if (areaNum < 0) return;
        areaHP[areaNum] -= dmg;
        if(areaHP[areaNum] <= 0)
            if(areaNum<=Masks.Length)
                Masks[areaNum].SetActive(true);
    }
}
public interface IDmgSegment
{
    void DmgByPosition(Transform position, float dmg);
}
