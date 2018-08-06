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
        url = "sky14786.cafe24.com/FR/Friend_Select.php";
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
       SystemManager.Instance.Records = new GameObject[n.Count];
        if (!SystemManager.Instance.isFirst)
        {
            for (int z = 0; z < SystemManager.Instance.temp; z++)
            {
                Debug.Log("파괴");
                DestroyObject(SystemManager.Instance.Create.transform.GetChild(z).gameObject);
            }
        }
        for (int i = 0; i < n.Count; i++)
        {
            SystemManager.Instance.Records[i] = Instantiate(Resources.Load("One_recorde"), SystemManager.Instance.Create.transform) as GameObject;
            Debug.Log(n[i][0].ToString());
            for (int j = 0; j < SystemManager.Instance.Records[i].transform.childCount; j++)
            {
                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "no")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();

                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "name")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();

                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "age")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();

                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "sex")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();

                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "phone")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();

                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "job")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();

                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "place")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();

                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "personality")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();

                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "etc")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();

                if (SystemManager.Instance.Records[i].transform.GetChild(j).name.ToString() == "update_date")
                    SystemManager.Instance.Records[i].transform.GetChild(j).GetComponent<Text>().text = n[i][j].ToString();
            }
            SystemManager.Instance.temp = n.Count;
            SystemManager.Instance.isFirst = false;
        }

        
        yield return WebRequest;
    }
}
