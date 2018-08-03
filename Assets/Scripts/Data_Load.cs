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
        GameObject[] Records = new GameObject[n.Count];
        for (int i = 0; i < n.Count; i++)
        {
            Records[i] = Instantiate(Resources.Load("One_recorde"), SystemManager.Instance.Create.transform) as GameObject;
            Debug.Log(n[i][0].ToString());
            for (int j = 0; j < Records[i].transform.childCount; j++)
            {
                if (Records[i].transform.GetChild(j).name.ToString() == "name")
                    Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
                if (Records[i].transform.GetChild(j).name.ToString() == "age")
                    Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
                if (Records[i].transform.GetChild(j).name.ToString() == "sex")
                    Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
                if (Records[i].transform.GetChild(j).name.ToString() == "phone")
                    Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
                if (Records[i].transform.GetChild(j).name.ToString() == "job")
                    Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
                if (Records[i].transform.GetChild(j).name.ToString() == "place")
                    Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
                if (Records[i].transform.GetChild(j).name.ToString() == "personality")
                    Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
                if (Records[i].transform.GetChild(j).name.ToString() == "etc")
                    Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
                if (Records[i].transform.GetChild(j).name.ToString() == "update_date")
                    Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
            }
        }


        yield return WebRequest;
    }
}
