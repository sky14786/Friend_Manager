using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net;
using UnityEngine.Networking;



public class Camera_Scripts : MonoBehaviour
{
    private static Camera_Scripts instance;

    public static Camera_Scripts Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (Camera_Scripts)FindObjectOfType<Camera_Scripts>();
            }
            return instance;
        }
    }

    public WebCamDevice Cam_Device;
    public WebCamTexture Cam;
    string sourceFilePath = "temp.jpg";
   public  string CamCheckUrl, PictureUploadUrl, Image_url;
    public string targetFileURI;
    public Texture temptx;
    string userID = "sky14786";
    string password = "whdkfk32!~";
    string Field_no;


    private void Awake()
    {
        
        PictureUploadUrl = "sky14786.cafe24.com/FM/CamUpload.php";
        CamCheckUrl = "sky14786.cafe24.com/FM/CamCheck.php";
        Cam = new WebCamTexture();

        //UIManager.Instance.CamOn_Btn.onClick.AddListener(() => CamOn());
       
        UIManager.Instance.Cam_Retake.onClick.AddListener(() => Retake());
        UIManager.Instance.CamOff_Btn.onClick.AddListener(() => CamOff());
        UIManager.Instance.CamShot_Btn.onClick.AddListener(() => Shot());
        UIManager.Instance.CamUpLoad_Btn.onClick.AddListener(() => UploadFTPFile(sourceFilePath, targetFileURI, userID, password));
        Debug.Log("Cam Setting Complete");
    }

    public void CamOn(bool isHaveCam)
    {
        if (isHaveCam)
        {
            UIManager.Instance.CamObject.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
            StartCoroutine(_ImageView());
            UIManager.Instance.Camera_Panel.SetActive(true);
            UIManager.Instance.ALLPanelOff();
            UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = temptx;
            //Cam.Play();
        }
        else
        {
            UIManager.Instance.ALLPanelOff();
            UIManager.Instance.CamObject.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
            UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = Cam;
            UIManager.Instance.Camera_Panel.SetActive(true);

            Cam.Play();

        }
    }

    public void Retake()
    {
        Cam.Stop();
        Cam.Play();
        UIManager.Instance.CamObject.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
        UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = Cam;
        UIManager.Instance.CamObject.GetComponent<Texture2D>().SetPixels32(Cam.GetPixels32());


    }

    public void CamOff()
    {
        Cam.Stop();
        System.IO.File.Delete("temp.jpg");
        UIManager.Instance.Camera_Panel.SetActive(false);
        UIManager.Instance.ALL_DATA_PANEL.SetActive(true);

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
        StartCoroutine(_PicUpload());
        try
        {
            
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
            System.IO.File.Delete(targetFileURI);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public IEnumerator _CheckCam(string no)
    {
        Field_no = no;
        Image_url = "sky14786.cafe24.com/FM/Images/" + SystemManager.Instance.User_ID + "_" + no + ".jpg";
        targetFileURI = "ftp://sky14786.cafe24.com/FM/Images/" + SystemManager.Instance.User_ID +"_"+ no+ ".jpg";
        WWWForm Form = new WWWForm();
        Form.AddField("no", Field_no);
        Form.AddField("id", SystemManager.Instance.User_ID);

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

        CamOn(SystemManager.Instance.isHaveCam);
        yield break;
    }

    IEnumerator _ImageView()
    {
        //WWW WebRequest = new WWW(Image_url);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(Image_url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            //UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        else
        {
            UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        yield break;
    }

    IEnumerator _PicUpload()
    {
        WWWForm Form = new WWWForm();
        Form.AddField("cam_data", targetFileURI);
        Form.AddField("owner", SystemManager.Instance.User_ID);
        Form.AddField("no", Field_no);

        WWW WebRequest = new WWW(PictureUploadUrl, Form);

        while (!WebRequest.isDone)
        {
            yield return null;
        }

        yield return WebRequest;
        Debug.Log("Picture Upload Success");
        Debug.Log(WebRequest.text);
        yield break;
    }

}


