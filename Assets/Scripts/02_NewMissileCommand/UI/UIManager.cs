using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void UIHUDInitHP(int _maxHp)
    {
        uiHudMgr.InitHp(_maxHp);
    }

    public void UIHUDInitMissile(int _maxMissileCnt)
    {
        uiHudMgr.InitMissile(_maxMissileCnt);
    }

    public void UIHUDInitKillCount()
    {
        uiHudMgr.InitKillCount();
    }



    public void UIHUDUpdateHp(int _curHp)
    {
        uiHudMgr.UpdateHp(_curHp);
    }

    public void UIHUDUpdateMissileStateWithIndex(int _index, bool _isFill)
    {
        uiHudMgr.UpdateMissile(_index, _isFill);
    }

    public void UIHUDUpdateKillCount(int _count)
    {
        uiHudMgr.UpdateKillCount(_count);
    }

    public void UIHUDUpdateTimer(int _sec)
    {
        uiHudMgr.UpdateTimer(_sec);
    }



    private void Awake()
    {
        uiHudMgr = GetComponentInChildren<UI_HUDManager>();
    }

    private UI_HUDManager uiHudMgr = null;
}
