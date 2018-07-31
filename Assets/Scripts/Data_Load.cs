using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Data_Load : MonoBehaviour {
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
        //for(int i=0;i<n.Count;i++)
        {
            Instantiate(SystemManager.Instance.one_recorde, SystemManager.Instance.Create.transform); //<< 여기부터
        }
     

        yield return WebRequest;
    }
}
