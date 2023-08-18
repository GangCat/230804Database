using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CanvasManager : MonoBehaviour
{
    public enum ECanvasType { None = -1, HUD, State }

    #region HUD
    public void SetActiveHUD(bool _active)
    {
        uiHudCanvas.SetActive(_active);
    }


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


    public void UIHUDFullHp()
    {
        uiHudCanvas.FullHp();
    }

    public void UIHUDReloadMissile()
    {
        uiHudCanvas.ReloadMissile();
    }
    #endregion

    #region State
    public void SetActiveState(bool _active)
    {
        uiStateCanvas.SetActive(_active);
    }


    public void OnReady()
    {
        uiStateCanvas.OnReady();
    }

    public void OnStart()
    {
        uiStateCanvas.OnStart();
    }

    public void OnGameOver(int _killCnt, int _timeSec)
    {
        uiStateCanvas.OnGameOver(_killCnt, _timeSec);
    }


    public void SetRetryButtonCallback(VoidVoidDelegate _callback)
    {
        uiStateCanvas.SetRetryButtonCallback(_callback);
    }
    #endregion






    private void Awake()
    {
        uiHudCanvas = GetComponentInChildren<UI_HUD_Canvas>();
        uiStateCanvas = GetComponentInChildren<UI_State_Canvas>();
    }

    private UI_HUD_Canvas uiHudCanvas = null;
    private UI_State_Canvas uiStateCanvas = null;
}
