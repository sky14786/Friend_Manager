using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net;
using UnityEngine.Networking;

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

        WWW Request = new WWW(Delete_url, Form);
        while (!Request.isDone)
        {
            yield return null;
        }

        yield return Request;




        //[ FTP FIle Delete] 
        string fileName = SystemManager.Instance.User_ID + this.transform.parent.GetChild(0).GetComponent<Text>().text + ".jpg";
        FtpWebRequest requestFileDelete = WebRequest.Create("ftp://sky14786.cafe24.com/FM/Images/" + fileName) as FtpWebRequest;
        requestFileDelete.Credentials = new NetworkCredential("sky14786", "whdkfk32!~");
        requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;

        FtpWebResponse responseFileDelete = (FtpWebResponse)requestFileDelete.GetResponse();




        Debug.Log("Delete Friend Complete!");
        yield break;
    }

}
