using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_HUD_Timer : MonoBehaviour
{
    public void SetTime(int _sec)
    {
        textMin.text = (_sec / 60).ToString("D2");
        textSec.text = (_sec % 60).ToString("D2");
    }




    [SerializeField]
    private TMP_Text textMin = null;
    [SerializeField]
    private TMP_Text textSec = null;
}
