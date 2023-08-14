using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public void Attack(Vector3 _targetPos, voidListPoolingObjectDelegate _hitCallback = null)
    {
        if (attackCo != null)
            StopCoroutine(attackCo);

        attackCo = StartCoroutine(AttackCoroutine(_targetPos, _hitCallback));
    }

    private void Awake()
    {
        im = InputMouse.Instance;
        missileSpawnPoint = GetComponentInChildren<MissileSpawnPoint>();
    }

    private void Start()
    {
        for (int i = 0; i < maxMissileCount; ++i)
        {
            GameObject missile = Instantiate(missilePrefab);
            missile.name = "Missile_" + i.ToString("D2");
            missile.SetActive(false);
            missileList.Add(missile.GetComponent<Missile>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StopCoroutine("AttackCoroutine");
    }

    private IEnumerator AttackCoroutine(Vector3 _targetPos, voidListPoolingObjectDelegate _hitCallback)
    {
        float towerAngle = CalcAngleToTarget(transform.position, transform.forward);

        float targetAngle = CalcAngleToTarget(transform.position, _targetPos);

        float difAngle = Mathf.Abs(towerAngle - targetAngle);

        if (difAngle > 180)
        {
            if (targetAngle > 0)
                targetAngle -= 360;
            else
                targetAngle += 360;

            difAngle = 360 - difAngle;
        }

        float s = difAngle / 360f;

        float t = 0f;

        while (t < 1f)
        {
            float angle = Mathf.Lerp(towerAngle, targetAngle, t);
            RotateYaw(transform, angle);

            t += (Time.deltaTime * rotateSpeed) / s;
            yield return null;
        }

        RotateYaw(transform, targetAngle);

        Missile missile = GetUsableMissile();
        if (missile != null)
        {
            missile.Init(missileSpawnPoint.GetSpawnPoint(), missileSpawnPoint.GetRotation(), _targetPos, _hitCallback);
        }
    }

    public static float CalcAngleToTarget(Vector3 _oriPos, Vector3 _targetPos)
    {
        Vector3 oriPos = _oriPos;
        oriPos.y = 0f;
        Vector3 targetPos = _targetPos;
        targetPos.y = 0f;

        Vector3 dirToTarget = (targetPos - oriPos).normalized;
        return Mathf.Atan2(dirToTarget.z, dirToTarget.x) * Mathf.Rad2Deg;
    }

    public static void SetRotation(Transform _tr, Vector3 _euler)
    {
        _tr.rotation = Quaternion.Euler(_euler);
    }

    public static void RotateYaw(Transform _tr, float _angle)
    {
        _tr.rotation = Quaternion.Euler(0f, -_angle + 90f, 0f);
    }

    private void PickingSample()
    {
        Vector3 point = Vector3.zero;
        if (im.Picking("Stage", ref point))
        {
            float theta = CalcAngleToTarget(transform.position, point);

            transform.rotation = Quaternion.Euler(0f, -theta + 90, 0f);
        }
    }

    private Missile GetUsableMissile()
    {
        foreach (Missile missile in missileList)
        {
            if (!missile.gameObject.activeSelf)
                return missile;
        }
        return null;
    }


    [SerializeField, Range(0.1f, 1f)]
    private float rotateSpeed = 0.5f;
    [SerializeField]
    private GameObject missilePrefab = null;
    [SerializeField]
    private int maxMissileCount = 3;

    private InputMouse im = null;
    private MissileSpawnPoint missileSpawnPoint = null;
    private Coroutine attackCo = null;
    private List<Missile> missileList = new List<Missile>();
}