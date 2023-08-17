using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolingObject
{
    private void SetTarget(GameObject _target)
    {
        target = _target;
    }

    public void AddPatterns(/*EP_EnemyPatternBase _movePattern, EP_EnemyPatternBase _rotPattern*/)
    {
        gameObject.AddComponent<EP_MoveAround>();
        gameObject.AddComponent<EP_RotateToTarget>();
    }

    public void Init(GameObject _target)
    {
        gameObject.SetActive(true);

        SetTarget(_target);

        patterns = GetComponents<EP_EnemyPatternBase>();
        foreach (EP_EnemyPatternBase pattern in patterns)
            pattern.Init(target);

        nonAnchorPos = transform.position;
    }

    public void Release()
    {
        gameObject.SetActive(false);
    }

    public bool isAlive()
    {
        return gameObject.activeSelf;
    }

    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }

    private void Update()
    {
        if (!target) return;

        Vector3 anchorPos = Vector3.zero;
        foreach (EP_EnemyPatternBase pattern in patterns)
        {
            if (pattern.IsAnchorType)
                anchorPos += pattern.MovePatternProcess();
            else if (pattern.IsNonAnchorType)
                nonAnchorPos += pattern.MovePatternProcess();
            else if (pattern.IsRotationType)
                transform.rotation = pattern.RotatePatternProcess();
        }

        transform.position = anchorPos + nonAnchorPos;

        myPos = transform.position;
        targetPos = target.transform.position;

        if (Utility.DistanceWIthoutYAxis(myPos, targetPos) < 1f)
            Release();
        // 이렇게하면 편하긴 한데
        // 벡터는 구조체라서 복사되서 좀 별로 속도측면에서 좋지 않음.
    }


    private Vector3 myPos = Vector3.zero;
    private Vector3 targetPos = Vector3.zero;
    private Vector3 nonAnchorPos = Vector3.zero;

    private GameObject target = null;
    private EP_EnemyPatternBase[] patterns = null;

}
