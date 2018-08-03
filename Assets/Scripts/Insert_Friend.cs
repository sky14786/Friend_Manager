using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Insert_Friend : MonoBehaviour {
    public string url;
    public InputField names, age, sex, phone, job, where, personality, etc;
    
    public GameObject Insert_Panel;
    public WWW WebRequest;
    private void Awake()
    {
        url = "sky14786.cafe24.com/Insert_Friend.php";
    }
    public IEnumerator _InsertFriend()
    {
        Debug.Log("DB 정보 입력 시작");
        WWWForm form = new WWWForm();

        Dictionary<string, string> DataDic = new Dictionary<string, string>();
        DataDic.Add("names", names.text);
        DataDic.Add("age", age.text);
        DataDic.Add("sex", sex.text);
        DataDic.Add("phone", phone.text);
        DataDic.Add("job", job.text);
        DataDic.Add("place", where.text);
        DataDic.Add("personality", personality.text);
        DataDic.Add("etc", etc.text);

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
        yield break;
    }
}
