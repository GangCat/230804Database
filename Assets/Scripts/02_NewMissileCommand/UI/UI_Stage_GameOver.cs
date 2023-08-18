using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage_GameOver : MonoBehaviour
{
    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void SetInfo(int _killCnt, int _time)
    {
        //StringBuilder sb = null;
        //sb.AppendLine(_killCnt.ToString());
        //sb.Append(_time.ToString());
        //scoreText.text += sb.ToString();
        //sb.Clear();

        int min = _time / 60;
        int sec = _time % 60;

        // $�� @�� �� �տ� �ֱ⵵ �ϴµ� �����ڵ带 ������ ASCII�ڵ带 ������ �̷��� 
        scoreText.text = string.Format("{0}\n{1:D2}:{2:D2}", _killCnt, min, sec);
    }

    public void SetRetryButtonCallback(VoidVoidDelegate _retryCallback)
    {
        // ����-> ������ �߻����� ����.
        // ����-> �� ������ ��𿡼� ������ ������� �Ⱥ���.
        //if (btnRetry == null) return;

        // �ν����Ϳ� �־���ϴµ� �ȳ־ �߻��ϴ� ������ ���� ���� ������ ��°� ���� ã�⵵ ���� ������.
        // �ٸ� getcomponent���� �����Ҵ��� �ϴ� �������� ������ ��������� ���� ����ó���ؼ� Ȯ�����ִ°� ����.

        btnRetry.onClick.AddListener(
            () =>
            {
                _retryCallback?.Invoke();
            }
            );
    }

    [SerializeField]
    private TMP_Text scoreText = null;
    [SerializeField]
    private Button btnRetry = null;

    
}