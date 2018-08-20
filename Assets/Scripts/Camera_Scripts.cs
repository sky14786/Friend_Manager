using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net;



public class Camera_Scripts : MonoBehaviour
{
    public WebCamTexture Cam;
    string sourceFilePath = "temp.jpg";
    string CamCheckUrl,PictureUploadUrl;
    public string targetFileURI;

    string userID = "sky14786";

    string password = "whdkfk32!~";


    private void Awake()
    {
        PictureUploadUrl = "sky14786.cafe24.com/FM/CamUpload.php";
        CamCheckUrl = "sky14786.cafe24.com/FM/CamCheck.php";
        Cam = new WebCamTexture();
        
        //UIManager.Instance.CamOn_Btn.onClick.AddListener(() => CamOn());
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(_CheckCam());
            CamOn(SystemManager.Instance.isHaveCam);
        });

        UIManager.Instance.CamOff_Btn.onClick.AddListener(() => CamOff());
        UIManager.Instance.CamShot_Btn.onClick.AddListener(() => Shot());
        UIManager.Instance.CamUpLoad_Btn.onClick.AddListener(() => UploadFTPFile(sourceFilePath, targetFileURI, userID, password));
        Debug.Log("Cam Setting Complete");
    }

    public void CamOn(bool isHaveCam)
    {
        if (isHaveCam)
        {
            StartCoroutine(_ImageView());
            UIManager.Instance.Camera_Panel.SetActive(true);
            Cam.Play();
        }
        else
        {
            UIManager.Instance.CamObject.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
           
            Cam.Play();
            UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = Cam;
            UIManager.Instance.Camera_Panel.SetActive(true);

        }
    }

    public void CamOff()
    {
        Cam.Stop();
        System.IO.File.Delete("temp.jpg");
        UIManager.Instance.Camera_Panel.SetActive(false);

    }

    public void Shot()
    {

        Texture2D temp = new Texture2D(Cam.width, Cam.height);
        temp.SetPixels32(Cam.GetPixels32());
        Debug.Log(temp.width);
        Debug.Log(temp.height);
        System.IO.File.WriteAllBytes("temp.jpg", ImageConversion.EncodeToJPG(temp));
        Cam.Stop();
    }



    public bool UploadFTPFile(string sourceFilePath, string targetFileURI, string userID, string password)
    {
        try
        {
            StartCoroutine(_PicUpload());
            System.Uri targetFileUri = new System.Uri(targetFileURI);

            FtpWebRequest ftpWebRequest = WebRequest.Create(targetFileUri) as FtpWebRequest;

            ftpWebRequest.Credentials = new NetworkCredential(userID, password);
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

            FileStream sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);

            Stream targetStream = ftpWebRequest.GetRequestStream();

            byte[] bufferByteArray = new byte[1024];
            while (true)
            {
                int byteCount = sourceFileStream.Read(bufferByteArray, 0, bufferByteArray.Length);

                if (byteCount == 0)
                {
                    break;
                }
                targetStream.Write(bufferByteArray, 0, byteCount);
            }
            targetStream.Close();
            sourceFileStream.Close();
            System.IO.File.Delete("temp.jpg");
        }
        catch
        {
            return false;   
        }
        return true;
    }

    IEnumerator _CheckCam()
    {
        targetFileURI = "ftp://sky14786.cafe24.com/FM/Images/" + SystemManager.Instance.User_ID + this.transform.parent.GetChild(0).GetComponent<Text>().text + ".jpg";
        WWWForm Form = new WWWForm();
        Form.AddField("No", this.transform.parent.GetChild(0).GetComponent<Text>().text);
        Form.AddField("ID", SystemManager.Instance.User_ID);

        WWW WebRequest = new WWW(CamCheckUrl, Form);

        while (!WebRequest.isDone)
        {
            yield return null;
        }

        yield return WebRequest;
        Debug.Log(WebRequest.text);
        if (WebRequest.text == "true")
            SystemManager.Instance.isHaveCam = true;
        else
            SystemManager.Instance.isHaveCam = false;
        yield break;
    }

    IEnumerator _ImageView()
    {
        WWW WebRequest = new WWW(targetFileURI);
       
        while (!WebRequest.isDone)
        {
            yield return null;
        }
        yield return WebRequest;
        UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = WebRequest.texture;
        yield break;
    }

    IEnumerator _PicUpload()
    {
        WWWForm Form = new WWWForm();
        Form.AddField("cam_data", targetFileURI);
        Form.AddField("owner", SystemManager.Instance.User_ID);
        Form.AddField("no", this.transform.parent.GetChild(0).GetComponent<Text>().text);

        WWW WebRequest = new WWW(PictureUploadUrl, Form);

        while (!WebRequest.isDone)
        {
            yield return null;
        }
        yield return WebRequest;
        Debug.Log("Picture Upload Success");

        yield break;
    }
    
}


