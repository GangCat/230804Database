using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolingObject
{
    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    public void Init()
    {
        moveSpeed = Random.Range(3f, 6f);
        gameObject.SetActive(true);
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
        if (target == null) return;

        Vector3 myPos = transform.position;
        myPos.y = 0f;
        Vector3 targetPos = target.transform.position;
        targetPos.y = 0f;

        Vector3 dir = (targetPos - myPos).normalized;

        transform.rotation = Quaternion.LookRotation(dir);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if((targetPos - myPos).magnitude < 1f)
        {
            Release();
        }
    }

    private float moveSpeed = 0f;
    private GameObject target = null;
}
