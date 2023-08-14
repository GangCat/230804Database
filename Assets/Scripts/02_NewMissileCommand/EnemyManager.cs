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

    // ���� �̷����� �� ��° ���� �¾Ҵ����� �ƴ� ���� ���� ������
    // int�迭���� ���� �޴´�.
    // ������ �ȵǰ� �ӵ��� ������ ����
    public void SetDamages(List<IPoolingObject> _hitList)
    {
        foreach (IPoolingObject enemy in enemies)
        {
            foreach(IPoolingObject hitEnemy in _hitList)
            {
                // equals�� object���϶��� ���� �̹��ϱ� �ѵ� ��κ��� ��� equals�� �� ������.
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
        // �̷� �������� Init�� �ʰ� ���� ���ϴ� Ÿ�ֿ̹� ��Ű�� ���� ������ �ʱ�ȭ
        // Init�� ��ġ�� �ٲٸ� �ʱ�ȭ�� �Ǿ�� �Ѵ�.
        // HP, ��ų ��Ÿ�� �� �ʱ�ȭ�� �� �� ���� ���·� ���ư��� ���̴�.
        // �̰� ������ �ʱ�ȭ�� �⺻�̴�.
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
    // init, release���� ����� ������ �ִ� Ŭ������ ������ٰ� ������ ��
    // �̷� Ǯ���ϴ� ������Ʈ���� �����ϴ� Ŭ������ ���� �ϴ� ����.
    //private Enemy[] enemies = null;
    private IPoolingObject[] enemies = null;

}