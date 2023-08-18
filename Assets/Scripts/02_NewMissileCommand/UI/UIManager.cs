using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void UIHUDInitHP(int _maxHp)
    {
        uiHudCanvas.InitHp(_maxHp);
    }

    public void UIHUDInitMissile(int _maxMissileCnt)
    {
        uiHudCanvas.InitMissile(_maxMissileCnt);
    }

    public void UIHUDInitKillCount()
    {
        uiHudCanvas.InitKillCount();
    }

    public void UIHUDUpdateHp(int _curHp)
    {
        uiHudCanvas.UpdateHp(_curHp);
    }

    public void UIHUDUpdateMissileStateWithIndex(int _index, bool _isFill)
    {
        uiHudCanvas.UpdateMissile(_index, _isFill);
    }

    public void UIHUDUpdateKillCount(int _count)
    {
        uiHudCanvas.UpdateKillCount(_count);
    }

    public void UIHUDUpdateTimer(int _sec)
    {
        uiHudCanvas.UpdateTimer(_sec);
    }



    private void Awake()
    {
        uiHudCanvas = GetComponentInChildren<UI_HUD_Canvas>();
        uiStateCanvas = GetComponentInChildren<UI_State_Canvas>();
    }

    private UI_HUD_Canvas uiHudCanvas = null;
    private UI_State_Canvas uiStateCanvas = null;
}
