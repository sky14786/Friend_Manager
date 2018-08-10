using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{

    public string Login_url;
    public InputField User_id, User_Pw;
    private void Awake()
    {
        Debug.Log("Login URL Setting Complete");
        Login_url = "sky14786.cafe24.com/FM/User_Login.php";

        this.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(_Login()));
    }

    IEnumerator _Login()
    {
        WWWForm form = new WWWForm();

        form.AddField("id", User_id.text.ToString());
        form.AddField("pw", User_Pw.text.ToString());

        WWW WebReqeust = new WWW(Login_url, form);

        if (WebReqeust.text == User_id.text.ToString())
        {
            SystemManager.Instance.User_ID = WebReqeust.text;
            UIManager.Instance.Main_Panel.SetActive(false);
            UIManager.Instance._MoveHomePanel();
        }
        else
        {

            switch (WebReqeust.text)
            {
                case "ID Error":
                    {
                        Debug.Log(WebReqeust.text);
                        yield break;
                    }
                case "PW Error":
                    {
                        Debug.Log(WebReqeust.text);
                        yield break;
                    }
            }
        }


    }
}
