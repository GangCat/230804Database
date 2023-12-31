using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP_MoveAround : EP_EnemyPatternBase
{
    public override void Init(GameObject _target)
    {
        base.Init(_target);

        calcType = ECalcType.Anchor;
    }

    public override Vector3 MovePatternProcess()
    {
        float time = Time.time * rotSpeed;
        float x = Mathf.Cos(time);
        float z = Mathf.Sin(time);

        return new Vector3(x, 0f, z) * radius;
    }

    [SerializeField, Range(1f, 5f)]
    private float radius = 1f;
    [SerializeField, Range(1f, 10f)]
    private float rotSpeed = 5f;
}