using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void HitCallback(List<IPoolingObject> _hitList)
    {
        enemyMgr.SetDamages(_hitList);

        killCnt += _hitList.Count;

        uiMgr.UIHUDUpdateKillCount(killCnt);
        //uiHudKillCount.SetKillCount(killCnt);
    }

    private void EnemyAttackCallback(int _dmg = 1)
    {
        int curHp = tower.Damage(_dmg);
        if (curHp < 0) return;

        uiMgr.UIHUDUpdateHp(curHp);
        //uiHudHp.UpdateHp(curHp);
        if (curHp == 0)
        {
            Debug.Log("GameOver");
            //StopCoroutine("TimerCoroutine");
        }
    }

    private void MissileStateCallback(int _missileIdx, bool _isFill)
    {
        uiMgr.UIHUDUpdateMissileStateWithIndex(_missileIdx, _isFill);
        //uiHudMissile.UpdateMissileStateWithIndex(_missileIdx, _isFill);
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
        enemyMgr.Init(tower.gameObject, EnemyAttackCallback);

        uiMgr.UIHUDInitHP(tower.MaxHp);
        //uiHudHp.Init(tower.MaxHp);
        uiMgr.UIHUDInitMissile(tower.MaxMissileCount);
        //uiHudMissile.Init(tower.MaxMissileCount);
        uiMgr.UIHUDInitKillCount();
        //uiHudKillCount.SetKillCount(killCnt);

        StartCoroutine("TimerCoroutine");
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            uiMgr.UIHUDUpdateTimer(timeSec);
            //uiHudTimer.SetTime(timeSec);
            yield return new WaitForSeconds(1f);
            ++timeSec;
        }
    }


    [SerializeField]
    private EnemyManager enemyMgr = null;
    [SerializeField]
    private UIManager uiMgr = null;
    

    //[SerializeField]
    //private UI_HUD_HP uiHudHp = null;
    //[SerializeField]
    //private UI_HUD_Missile uiHudMissile = null;
    //[SerializeField]
    //private UI_HUD_KillCount uiHudKillCount = null;
    //[SerializeField]
    //private UI_HUD_Timer uiHudTimer = null;

    private int killCnt = 0;
    private int timeSec = 0;

    private InputMouse inputMouse = null;
    private Tower tower = null;
}
