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

    string targetFileURI = "ftp://sky14786.cafe24.com/FM/Images/temp.jpg";

    string userID = "sky14786";

    string password = "whdkfk32!~";


    private void Awake()
    {

        Cam = new WebCamTexture();
        UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = Cam;
        UIManager.Instance.CamOn_Btn.onClick.AddListener(() => CamOn());
        UIManager.Instance.CamOff_Btn.onClick.AddListener(() => CamOff());
        UIManager.Instance.CamShot_Btn.onClick.AddListener(() => Shot());
        UIManager.Instance.CamUpLoad_Btn.onClick.AddListener(() => UploadFTPFile(sourceFilePath, targetFileURI, userID, password));
    }

    public void CamOn()
    {
        UIManager.Instance.Camera_Panel.SetActive(true);
        Cam.Play();
        UIManager.Instance.CamObject.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
    }

    public void CamOff()
    {
        Cam.Stop();
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
}


