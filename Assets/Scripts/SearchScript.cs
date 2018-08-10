using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;


public class SearchScript : MonoBehaviour
{
    public string Requirment;
    public string url;
    public Dropdown Search_Requirement;
    public InputField Search_Keyword;
    public int Temp;
    
    private void Awake()
    {
        Debug.Log("검색 URL 초기화 완료");
        url = "sky14786.cafe24.com/FM/Search_Data.php";
        this.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(_Search()));
    }


    //검색 조건 0 : 이름 // 1 : 직업 // 2 : 나이 // 3 : 성별
    IEnumerator _Search()
    {

        WWWForm form = new WWWForm();
        switch (Search_Requirement.value)
        {
            case 0:
                {
                    Requirment = "names";
                    break;
                }
            case 1:
                {
                    Requirment = "job";
                    break;
                }
            case 2:
                {
                    Requirment = "age";
                    break;
                }
            case 3:
                {
                    Requirment = "sex";
                    break;
                }
        }

        form.AddField("Search_Requirement", Requirment);
        form.AddField("Search_Keyword", Search_Keyword.text.ToString());
        form.AddField("owner", SystemManager.Instance.User_ID.ToString());


        Debug.Log(Requirment);
        Debug.Log(Search_Keyword.text.ToString());
        WWW WebRequest = new WWW(url, form);
        while (!WebRequest.isDone)
        {
            yield return null;
        }
        if (WebRequest.error == null)
        {
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
            }
            SystemManager.Instance.temp = n.Count;
           SystemManager.Instance.isFirst = false;
        }
        else
            Debug.Log("error");


        yield return WebRequest;
    }


}
