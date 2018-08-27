using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend_Delete : MonoBehaviour {
    public string Delete_url;
    private void Awake()
    {
        Delete_url = "sky14786.cafe24.com/FM/Delete_Friend.php";
        Debug.Log("Delete_Url Setting Complete!");
        this.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(_DeleteFriend()));

      

    }

    IEnumerator _DeleteFriend()
    {
        WWWForm Form = new WWWForm();

        Form.AddField("no", this.transform.parent.GetChild(0).GetComponent<Text>().text);

        WWW WebRequest = new WWW(Delete_url, Form);
        while (!WebRequest.isDone)
        {
            yield return null;
        }

        yield return WebRequest;
        yield break;
    }
}
