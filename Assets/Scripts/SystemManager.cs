using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour {
    public string url;
    public InputField name, age, sex, phone, job, where, personality, etc;
    private void Awake()
    {
        url = "sky14786.cafe24.com/Insert_Friend.php";
    }

    IEnumerator _InsertFriend()
    {
        WWWForm form = new WWWForm();

        form.AddField("name", name.text);
        form.AddField("age", age.text);
        form.AddField("sex", sex.text);
        form.AddField("phone",phone.text);
        form.AddField("job", job.text);
        form.AddField("where", where.text);
        form.AddField("personality", personality.text);
        form.AddField("etc", etc.text);

        WWW WebRequest = new WWW(url, form);
        yield return WebRequest;

        Debug.Log(WebRequest.error);
        yield break;
    }
}
