using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_User : MonoBehaviour
{
    public string url, ID_Check_url,User_PW;

    private void Awake()
    {
        url = "sky14786.cafe24.com/Create_User.php";
       ID_Check_url = "sky14786.cafe24.com/ID_Check.php";
    }

    IEnumerator _Createuser()
    {
        WWWForm form = new WWWForm();

        
           
        else
            Debug.Log("ID는 6~12자 입니다.");
        if (UIManager.Instance.Create_PW.text.ToString().Length >= 8 && UIManager.Instance.Create_PW.text.ToString().Length <= 15)
            if (UIManager.Instance.Create_PW == UIManager.Instance.Create_PW_Check)
                form.AddField("pw", UIManager.Instance.Create_PW.ToString());
            else
                Debug.Log("PW와 PW_Check가 같지 않습니다.");
        else
            Debug.Log("PW는 8~15자 입니다.");

    }

    IEnumerator _ID_Check()
    {
        WWWForm Form = new WWWForm();
        if (UIManager.Instance.Create_ID.text.ToString().Length >= 6 && UIManager.Instance.Create_ID.text.ToString().Length <= 12)
        {
            Form.AddField("id", UIManager.Instance.Create_ID.text.ToString());

            WWW WebRequest = new WWW(ID_Check_url, Form);
        }
        else
            Debug.Log("ID는 6~12자 입니다.");
        
    }
}
