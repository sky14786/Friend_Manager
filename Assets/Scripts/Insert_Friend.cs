using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Insert_Friend : MonoBehaviour {
    public string url;
    
    public GameObject Insert_Panel;
    public WWW WebRequest;
    private void Awake()
    {
        url = "sky14786.cafe24.com/FM/Insert_Friend.php";
        UIManager.Instance.Insert_Button.onClick.AddListener(() => Null_Check());
    }

    public void Null_Check()
    {
        if (UIManager.Instance.Insert_Age.text == null && UIManager.Instance.Insert_Name.text == null && UIManager.Instance.Insert_Sex.text == null && UIManager.Instance.Insert_Phone.text == null &&
            UIManager.Instance.Insert_Job.text == null && UIManager.Instance.Insert_Place.text == null && UIManager.Instance.Insert_Personality.text == null)
        {
            StartCoroutine(_InsertFriend());
        }
        else
            Debug.Log("모두 입력해 주세요");
        
    }
    public IEnumerator _InsertFriend()
    {
        Debug.Log("DB 정보 입력 시작");
        WWWForm form = new WWWForm();

        Dictionary<string, string> DataDic = new Dictionary<string, string>();
        DataDic.Add("names", UIManager.Instance.Insert_Name.text);
        DataDic.Add("age", UIManager.Instance.Insert_Age.text);
        DataDic.Add("sex", UIManager.Instance.Insert_Sex.text);
        DataDic.Add("phone", UIManager.Instance.Insert_Phone.text);
        DataDic.Add("job", UIManager.Instance.Insert_Job.text);
        DataDic.Add("place", UIManager.Instance.Insert_Place.text);
        DataDic.Add("personality", UIManager.Instance.Insert_Personality.text);
        DataDic.Add("etc", UIManager.Instance.Insert_ETC.text);
        DataDic.Add("owner", SystemManager.Instance.User_ID);

        foreach (KeyValuePair<string, string> data in DataDic)
        {
            form.AddField(data.Key, data.Value);
        }
        WebRequest = new WWW(url, form);

        yield return WebRequest;

        while (!WebRequest.isDone)
        {
            yield return null;
        }
        Debug.Log(WebRequest.text);
        Debug.Log(WebRequest.error);
        Debug.Log("DB 정보 입력 종료");

        UIManager.Instance._ResetText();
        yield break;
    }
}
