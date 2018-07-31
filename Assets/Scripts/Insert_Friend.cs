using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Insert_Friend : MonoBehaviour {
    public string url;
    public InputField names, age, sex, phone, job, where, personality, etc;
    public Button Insert_btn,Exit_btn;
    public GameObject Insert_Panel;
    private void Awake()
    {
        url = "sky14786.cafe24.com/Insert_Friend.php";

        Insert_btn.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(_InsertFriend()));

        Exit_btn.GetComponent<Button>().onClick.AddListener(() => Insert_Panel.SetActive(false));

    }
    
    IEnumerator _InsertFriend()
    {
        Debug.Log("DB 정보 입력 시작");
        WWWForm form = new WWWForm();

        form.AddField("name", names.text.ToString());
        form.AddField("age", age.text.ToString());
        form.AddField("sex", sex.text.ToString());
        form.AddField("phone", phone.text.ToString());
        form.AddField("job", job.text.ToString());
        form.AddField("where", where.text.ToString());
        form.AddField("personality", personality.text.ToString());
        form.AddField("etc", etc.text.ToString());

        WWW WebRequest = new WWW(url, form);
        yield return WebRequest;

        Debug.Log(WebRequest.error);
        Debug.Log("DB 정보 입력 종료");
        yield break;
    }
}
