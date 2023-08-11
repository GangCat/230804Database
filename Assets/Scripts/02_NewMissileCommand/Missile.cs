using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public void Init(Vector3 _spawnPos, Quaternion _spawnRot, Vector3 _targetPos, voidListEnemyDelegate _hitEnemyListCallback = null)
    {
        targetPos = _targetPos;
        targetPos.y = transform.position.y;
        moveDir = (targetPos - transform.position).normalized;

        transform.position = _spawnPos;
        transform.rotation = _spawnRot;
        gameObject.SetActive(true);
        hitEnemyListCallback = _hitEnemyListCallback;

        isInit = true;
    }

    public void ExplosionFinish()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        explosion = GetComponentInChildren<Explosion>();
    }

    private void Update()
    {
        if (!isInit) return;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            explosion.Activate(ExplosionFinish, hitEnemyListCallback);
            isInit = false;
        }
    }


    private Vector3 targetPos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    [SerializeField, Range(1f, 30f)]
    private float moveSpeed = 10f;

    private bool isInit = false;

    private Explosion explosion = null;

    private voidListEnemyDelegate hitEnemyListCallback = null;
}