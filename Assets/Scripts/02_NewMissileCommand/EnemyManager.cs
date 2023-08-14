using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
                    enemy.transform.position = ComputeRandomPosWithCircleRange(Vector3.zero);
                }
            }
        }
    }

    // 보통 이럴때는 몇 번째 적이 맞았는지를 아는 것이 좋기 때문에
    // int배열같은 것을 받는다.
    // 가공도 안되고 속도도 빠르기 때문
    public void SetDamages(List<IPoolingObject> _hitList)
    {
        foreach (IPoolingObject enemy in enemies)
        {
            foreach(IPoolingObject hitEnemy in _hitList)
            {
                // equals가 object형일때는 조금 미묘하긴 한데 대부분의 경우 equals가 더 빠르다.
                if(enemy.Equals(hitEnemy))
                {
                    // Release
                    //Destroy(enemy.gameObject);
                    enemy.Release();

                    continue;
                }
            }
        }
    }

    private void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs\\P_Enemy");
    }

    public void Init(GameObject _target)
    {
        enemies = new IPoolingObject[maxEnemyCount];

        for(int i = 0; i < maxEnemyCount; ++i)
        {
            GameObject go = Instantiate(enemyPrefab, ComputeRandomPosWithCircleRange(Vector3.zero), Quaternion.identity, transform);
            go.name = "Enemy_" + i;
            //enemyList.Add(go.GetComponent<Enemy>());

            enemies[i] = go.GetComponent<IPoolingObject>();
            ((Enemy)enemies[i]).SetTarget(_target);
            ((Enemy)enemies[i]).Release();
        }

        StartCoroutine(RespawnCoroutine());
        // 이런 느낌으로 Init을 늦게 내가 원하는 타이밍에 시키는 것이 게으른 초기화
        // Init은 수치만 바꾸면 초기화가 되어야 한다.
        // HP, 스킬 쿨타임 등 초기화를 할 때 원래 상태로 돌아가는 것이다.
        // 이게 게으른 초기화의 기본이다.
    }

    private Vector3 RandomPosition()
    {
        Vector2 rnd = Random.insideUnitCircle * 20f;
        return new Vector3(rnd.x, 0f, rnd.y);
    }

    private Vector3 ComputeRandomPosWithCircleRange(Vector3 _centerPos)
    {
        if (outerCircleRad < innerCircleRad)
        {
            Debug.LogError("OuterCircleRad must bigger than InnerCircleRad");
            Debug.Break();
            return Vector3.zero;
        }

        float rndLength = Random.Range(outerCircleRad, innerCircleRad);
        float rndAngle = Random.Range(-180, 180);

        return _centerPos + new Vector3(rndLength * Mathf.Cos(rndAngle), 0f, rndLength * Mathf.Sin(rndAngle));
    }

    private IEnumerator RespawnCoroutine()
    {
        while (true)
        {
            foreach(Enemy enemy in enemies)
            {
                if (!enemy.isAlive())
                {
                    enemy.SetPosition(ComputeRandomPosWithCircleRange(Vector3.zero));
                    enemy.Init();
                    break;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }


    [SerializeField]
    private int maxEnemyCount = 20;
    [SerializeField]
    private int innerCircleRad = 20;
    [SerializeField]
    private int outerCircleRad = 20;

    private GameObject enemyPrefab = null;
    //private GameObject[] enemyPrefabs = null;
    private List<Enemy> enemyList = new List<Enemy>();

    // class PoolManager
    // private poolObject[] enemies = null;
    // init, release같은 기능을 가지고 있는 클래스를 만들었다고 가정할 때
    // 이런 풀링하는 오브젝트들을 관리하는 클래스를 만들어서 하는 거임.
    //private Enemy[] enemies = null;
    private IPoolingObject[] enemies = null;

}