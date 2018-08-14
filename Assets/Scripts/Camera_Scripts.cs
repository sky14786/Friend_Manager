using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class Camera_Scripts : MonoBehaviour
{
    public WebCamTexture Cam;
    public Color32[] data;
    public Texture2D temp;


    private void Awake()
    {
        Cam = new WebCamTexture();
        UIManager.Instance.CamObject.GetComponent<Renderer>().material.mainTexture = Cam;
        UIManager.Instance.CamOn_Btn.onClick.AddListener(() => CamOn());
        UIManager.Instance.CamOff_Btn.onClick.AddListener(() => CamOff());
        UIManager.Instance.CamShot_Btn.onClick.AddListener(() => Shot());
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
        data = new Color32[Cam.width * Cam.height];
        Texture2D temp = new Texture2D(Cam.width, Cam.height);
        temp.SetPixels32(Cam.GetPixels32());
        ImageConversion.EncodeToJPG(temp);

        
        Cam.Stop();
    }
  
}
