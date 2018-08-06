using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Update_SQL : MonoBehaviour {
    public string url;
    public WWW WebRequest;

    private void Awake()
    {
        Debug.Log("Update Query URL Setting Complete");
        url = "sky14786.cafe24.com/FR/Update_Friend.php";
        UIManager.Instance.Update_Button.onClick.AddListener(() => StartCoroutine(_UpdateFriend()));
    }

    
    public IEnumerator _UpdateFriend()
    { 
        Debug.Log("Friend Data Update Start");
        WWWForm form = new WWWForm();   

        Dictionary<string, string> DataDic = new Dictionary<string, string>();
        DataDic.Add("no", UIManager.Instance.Update_No.text);
        DataDic.Add("names", UIManager.Instance.Update_Name.text);
        DataDic.Add("age", UIManager.Instance.Update_Age.text);
        DataDic.Add("sex", UIManager.Instance.Update_Sex.text);
        DataDic.Add("phone", UIManager.Instance.Update_Phone.text);
        DataDic.Add("job", UIManager.Instance.Update_Job.text);
        DataDic.Add("place", UIManager.Instance.Update_Place.text);
        DataDic.Add("personality", UIManager.Instance.Update_Personality.text);
        DataDic.Add("etc", UIManager.Instance.Update_ETC.text);

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
        Debug.Log("Friend Data Update Complete");
        yield break;
    }
}

