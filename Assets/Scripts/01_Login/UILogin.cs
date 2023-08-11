using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILogin : MonoBehaviour
{
    public enum EInputType { ID, PW }
    public delegate void OnClickSignUpDelegate(string _id, string _pw);
    public delegate void OnClickSignInDelegate(string _id, string _pw);

    public OnClickSignUpDelegate OnClickSignUpCallback
    {
        set { onClickSignUpCallback = value; }
    }

    public OnClickSignInDelegate OnClickSignInCallback
    {
        set { onClickSignInCallback = value; }
    }

    public void OnChangedIdText(string _id)
    {
        id = _id;
    }


    private void Awake()
    {
        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>();

        string ifText = inputFields[(int)EInputType.ID].text;

        dicIF.Add("ID", inputFields[0]);
        dicIF.Add("PW", inputFields[1]);
        ifText = dicIF["ID"].text;

        Button btnSignUp = GetComponentInChildren<ButtonSignUp>().transform.GetComponent<Button>();
        btnSignUp.onClick.AddListener(
            () =>
            {
                onClickSignUpCallback?.Invoke(id, dicIF["PW"].text);
            }
            );

        Button btnSignIn = GetComponentInChildren<ButtonSignIn>().transform.GetComponent<Button>();
        btnSignIn.onClick.AddListener(
            () =>
            {
                onClickSignInCallback?.Invoke(id, dicIF["PW"].text);
            }
            );
    }

    private Dictionary<string, TMP_InputField> dicIF = new Dictionary<string, TMP_InputField>();

    private string id = null;
    private OnClickSignUpDelegate onClickSignUpCallback = null;
    private OnClickSignInDelegate onClickSignInCallback = null;
}