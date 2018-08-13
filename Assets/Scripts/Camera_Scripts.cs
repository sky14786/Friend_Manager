using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera_Scripts : MonoBehaviour
{
    public static WebCamTexture Cam;

    private void Awake()

    {
        Cam = new WebCamTexture();
        
        UIManager.Instance.CamOn_Btn.onClick.AddListener(() => CamOn());
        UIManager.Instance.CamOff_Btn.onClick.AddListener(() => CamOff());
    }

    public void CamOn()
    {
        UIManager.Instance.Camera_Panel.SetActive(true);
        
        Cam.Play();
    }

    public void CamOff()
    {
        Cam.Stop();
    }
}
