using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Data_Load : MonoBehaviour
{
    public string url;

    private void Awake()
    {
        url = "sky14786.cafe24.com/Friend_Select.php";
        this.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(_SelectDB()));
    }

    IEnumerator _SelectDB()
    {
        WWW WebRequest = new WWW(url);
        if (WebRequest.error == null)
            Debug.Log("error null");
        else
            Debug.Log("error");
        while (!WebRequest.isDone)
        {
            yield return null;
        }
        Debug.Log(WebRequest.text);
        var n = LitJson.JsonMapper.ToObject(WebRequest.text);
        for (int i = 0; i < n.Count; i++)
        {
            GameObject gg = Instantiate(Resources.Load("One_recorde"), SystemManager.Instance.Create.transform) as GameObject;
            Friend_Info.Instance.name.text = n[i][0].ToString();
            Friend_Info.Instance.age.text = n[i][1].ToString();
            Friend_Info.Instance.sex.text = n[i][2].ToString();
            Friend_Info.Instance.phone.text = n[i][3].ToString();
            Friend_Info.Instance.job.text = n[i][4].ToString();
            Friend_Info.Instance.place.text = n[i][5].ToString();
            Friend_Info.Instance.personality.text = n[i][6].ToString();
            Friend_Info.Instance.etc.text = n[i][7].ToString();
            Friend_Info.Instance.update_date.text = n[i][8].ToString();
        }


        yield return WebRequest;
    }
}
