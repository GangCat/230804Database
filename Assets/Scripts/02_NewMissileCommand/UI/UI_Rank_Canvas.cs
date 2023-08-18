using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Rank_Canvas : MonoBehaviour
{
    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void ShowEnterName()
    {
        uiRankEnterName.SetActive(true);
    }

    public void ShowRanking()
    {
        uiRankEnterName.SetActive(false);
    }




    [SerializeField]
    private UI_Rank_EnterName uiRankEnterName = null;
}
