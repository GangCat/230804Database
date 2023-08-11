using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void HitCallback(List<Enemy> _hitList)
    {
        enemyManager.RespawnEnemy(_hitList);

        killedEnemyCount += _hitList.Count;

        //foreach (Enemy enemy in _hitList)
        //    Destroy(enemy.gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Vector3.zero;
            if(inputMouse.Picking("Stage", ref point))
            {
                tower.Attack(point, HitCallback);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log(killedEnemyCount);
    }

    private void Awake()
    {
        tower = FindAnyObjectByType<Tower>();
        inputMouse = InputMouse.Instance;
    }



    [SerializeField]
    private EnemyManager enemyManager = null;

    private int killedEnemyCount = 0;

    private InputMouse inputMouse = null;
    private Tower tower = null;
}
