using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void HitCallback(List<IPoolingObject> _hitList)
    {
        enemyMng.SetDamages(_hitList);

        killCnt += _hitList.Count;

        uiHudKillCount.SetKillCount(killCnt);
    }

    private void EnemyAttackCallback(int _dmg = 1)
    {
        int curHp = tower.Damage(_dmg);
        if (curHp < 0) return;

        uiHudHp.UpdateHp(curHp);
        if (curHp == 0)
        {
            Debug.Log("GameOver");
            StopCoroutine("TimerCoroutine");
        }
    }

    private void MissileStateCallback(int _missileIdx, bool _isFill)
    {
        uiHudMissile.UpdateMissileStateWithIndex(_missileIdx, _isFill);
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
            Debug.Log(killCnt);
    }

    private void Awake()
    {
        tower = FindAnyObjectByType<Tower>();
        inputMouse = InputMouse.Instance;
    }

    private void Start()
    {
        tower.Init(MissileStateCallback);
        enemyMng.Init(tower.gameObject, EnemyAttackCallback);

        uiHudHp.Init(tower.MaxHp);
        uiHudMissile.Init(tower.MaxMissileCount);
        uiHudKillCount.SetKillCount(killCnt);

        StartCoroutine("TimerCoroutine");
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            uiHudTimer.SetTime(timeSec);
            yield return new WaitForSeconds(1f);
            ++timeSec;
        }
    }


    [SerializeField]
    private EnemyManager enemyMng = null;

    [SerializeField]
    private UI_HUD_HP uiHudHp = null;
    [SerializeField]
    private UI_HUD_Missile uiHudMissile = null;
    [SerializeField]
    private UI_HUD_KillCount uiHudKillCount = null;
    [SerializeField]
    private UI_HUD_Timer uiHudTimer = null;

    private int killCnt = 0;
    private int timeSec = 0;

    private InputMouse inputMouse = null;
    private Tower tower = null;
}
