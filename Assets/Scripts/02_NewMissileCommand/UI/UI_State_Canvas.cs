using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_State_Canvas : MonoBehaviour
{
    public enum EState { Ready, Start, GameOver }

    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    // ���߿� �ش� Ÿ�ֿ̹� ȿ������ �ְų� ��� �� �� �ֱ� ������ �̸� ������.
    public void OnReady()
    {
        UpdateVIsible(EState.Ready);
    }

    public void OnStart()
    {
        UpdateVIsible(EState.Start);
    }

    public void OnGameOver(int _killCnt, int _time)
    {
        gameOver.SetInfo(_killCnt, _time);
        UpdateVIsible(EState.GameOver);
    }

    private void UpdateVIsible(EState _state)
    {
        readyGo.SetActive(false);
        startGo.SetActive(false);
        gameOver.SetActive(false);

        switch (_state)
        {
            case EState.Ready:
                readyGo.SetActive(true);
                break;
            case EState.Start:
                startGo.SetActive(true);
                break;
            case EState.GameOver:
                gameOver.SetActive(true);
                break;
        }
    }

    [SerializeField]
    private GameObject readyGo = null;
    [SerializeField]
    private GameObject startGo = null;
    [SerializeField]
    private UI_Stage_GameOver gameOver = null;
}
