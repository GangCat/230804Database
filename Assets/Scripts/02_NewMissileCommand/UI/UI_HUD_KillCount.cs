using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_HUD_KillCount : MonoBehaviour
{
    public void SetKillCount(int _cnt)
    {
        textKillCount.text = _cnt.ToString();
    }

    [SerializeField]
    private TMP_Text textKillCount = null;
}
