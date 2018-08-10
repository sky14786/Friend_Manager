using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_User : MonoBehaviour
{
    public string url, ID_Check_url;
    public bool isUsed, isSame;
    

    private void Awake()
    {
        url = "sky14786.cafe24.com/FM/Create_User.php";
        ID_Check_url = "sky14786.cafe24.com/FM/ID_Duplicate_Check.php";

        UIManager.Instance.Create_User_Button.onClick.AddListener(() =>StartCoroutine(_Createuser()));

        UIManager.Instance.ID_Check_Button.onClick.AddListener(() => StartCoroutine(_ID_Check()));
    }

    IEnumerator _Createuser()
    {
        Debug.Log("계정 생성 시작");
        WWWForm form = new WWWForm();

        if (UIManager.Instance.Create_PW.text.ToString().Length >= 8 && UIManager.Instance.Create_PW.text.ToString().Length <= 15)
            if (UIManager.Instance.Create_PW.text == UIManager.Instance.Create_PW_Check.text)
            {
                isSame = true;
            }
            else
            {
                Debug.Log("PW와 PW_Check가 같지 않습니다.");
                yield break;
            }
        else
        {
            Debug.Log("PW는 8~15자 입니다.");
            yield break;
        }
        
        if(isUsed && isSame)
        {
            form.AddField("id", UIManager.Instance.Create_ID.text.ToString());
            form.AddField("pw", UIManager.Instance.Create_PW.text.ToString());
            form.AddField("name", UIManager.Instance.Create_Name.text.ToString());

            WWW WebRequest = new WWW(url, form);

            Debug.Log(WebRequest.text);
            Debug.Log("계정 생성 성공");
        }
        
        yield break;
    }

    IEnumerator _ID_Check()
    {
        Debug.Log("ID 중복 체크 시작");
        WWWForm Form = new WWWForm();
        if (UIManager.Instance.Create_ID.text.ToString().Length >= 6 && UIManager.Instance.Create_ID.text.ToString().Length <= 12)
        {
            Form.AddField("id", UIManager.Instance.Create_ID.text.ToString());

            WWW WebRequest = new WWW(ID_Check_url, Form);


            while (!WebRequest.isDone)
            {
                yield return null;
            }

            yield return WebRequest;
            Debug.Log(WebRequest.text);
            if (WebRequest.text == "true")
            {
                UIManager.Instance.Create_ID_Check.text = "사용가능한 ID 입니다.";
                isUsed = true;
            }
            else
            {
                UIManager.Instance.Create_ID_Check.text = "이미 존재하는 ID입니다.";
                isUsed = false;
            }
        }
        else
        {
            UIManager.Instance.Create_ID_Check.text = "아이디는 6~12자 입니다.";
            isUsed = false;
        }


        Debug.Log("ID 중복 체크 종료");
        yield break;
    }
}
