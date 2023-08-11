using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void RespawnEnemy(List<Enemy> _enemyList)
    {
        for (int i = 0; i < maxEnemyCount; ++i)
        {
            foreach (Enemy enemy in _enemyList)
            { 
                if (enemy == enemyList[i])
                {
                    enemy.transform.position = GetRandomPosition(15, 10, Vector3.zero);
                }
            }
        }
    }


    private void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs\\P_Enemy");
    }

    private void Start()
    {
        for(int i = 0; i < maxEnemyCount; ++i)
        {
            GameObject go = Instantiate(enemyPrefab, GetRandomPosition(15, 10, Vector3.zero), Quaternion.identity, transform);
            go.name = "Enemy_" + i;
            enemyList.Add(go.GetComponent<Enemy>());
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 rnd = Random.insideUnitCircle * 20f;
        return new Vector3(rnd.x, 0f, rnd.y);
    }

    private Vector3 GetRandomPosition(float _outerCircleRad, float _innerCircleRad, Vector3 _centerPos)
    {
        if (_outerCircleRad < _innerCircleRad)
        {
            Debug.LogError("OuterCircleRad must bigger than InnerCircleRad");
            Debug.Break();
            return Vector3.zero;
        }

        float rndLength = Random.Range(_outerCircleRad, _innerCircleRad);
        float rndAngle = Random.Range(-180, 180);

        return _centerPos + new Vector3(rndLength * Mathf.Cos(rndAngle), 0f, rndLength * Mathf.Sin(rndAngle));
    }


    [SerializeField]
    private int maxEnemyCount = 20;

    private GameObject enemyPrefab = null;
    private GameObject[] enemyPrefabs = null;
    private List<Enemy> enemyList = new List<Enemy>();
}