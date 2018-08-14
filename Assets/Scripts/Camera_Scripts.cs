using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net;



public class Camera_Scripts : MonoBehaviour
{
    public WebCamTexture Cam;
    public Color32[] temp2;
    public Texture2D temp;
  


    private void Awake()
    {
    
        Cam = new WebCamTexture();
        UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = Cam;
        UIManager.Instance.CamOn_Btn.onClick.AddListener(() => CamOn());
        UIManager.Instance.CamOff_Btn.onClick.AddListener(() => CamOff());
        UIManager.Instance.CamShot_Btn.onClick.AddListener(() => Shot());
        UIManager.Instance.CamUpLoad_Btn.onClick.AddListener(() => StartCoroutine(_CamUpload()));
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

    IEnumerator  _CamUpload()
    {
        FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create("ftp://sky14786.cafe24.com:21/FM/Images/" + SystemManager.Instance.User_ID.ToString() + ".jpg");
        ftp.Credentials = new NetworkCredential("sky14786", "whdkfk32!~");
        ftp.UsePassive = true;
        ftp.UseBinary = true;
        ftp.KeepAlive = false;


        byte[] data = new byte[temp.width*temp.height*4];
        temp2 = temp.GetPixels32();
        int idx = 0;
        for (int i = 0; i < temp2.Length; i++)
        {
            data[idx] = temp2[i].a;
            data[idx + 1] = temp2[i].r;
            data[idx + 2] = temp2[i].g;
            data[idx + 3] = temp2[i].b;

            idx += 4;
        }

        ftp.ContentLength = data.Length;
        using (Stream reqStream = ftp.GetRequestStream())
        {
            reqStream.Write(data, 0, data.Length);
        }

        using (FtpWebResponse resp = (FtpWebResponse)ftp.GetResponse())
        {
            Debug.Log("Upload: {0}" + resp.StatusDescription);
        }

        yield break;

    }
}


