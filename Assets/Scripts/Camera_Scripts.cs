using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Camera_Scripts : MonoBehaviour
{
    public WebCamTexture Cam;
    public Color32[] data; 
   
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
    }

    public void CamOff()
    {
        Cam.Stop();
        UIManager.Instance.Camera_Panel.SetActive(false);
    }

    public void Shot()
    {
        data = new Color32[Cam.width * Cam.height];
        Cam.Stop();
    }
    //public struct RGB
    //{
    //    public byte Red;
    //    public byte Green;
    //    public byte Blue;

    //    public RGB(Color inputColor)
    //    {
    //        Red = inputColor.R;
    //        Green = inputColor.G;
    //        Blue = inputColor.B;
    //    }

    //    public RGB(byte red, byte green, byte blue)
    //    {
    //        Red = red;
    //        Green = green;
    //        Blue = blue;
    //    }
    //}

    //Bitmap RawImageSource;

    //public JPEGEncoding(RGB[,] colorInputArray)
    //{
    //    RawImageSource = new Bitmap(colorInputArray.GetLength(0), colorInputArray.GetLength(1));

    //    for (int j = 0; j & lt; colorInputArray.GetLength(0); j++)
    //        {
    //        for (int i = 0; i & lt; colorInputArray.GetLength(1); i++)
    //            {
    //            RawImageSource.SetPixel(j, i, Color.FromArgb(colorInputArray[j, i].Red, colorInputArray[j, i].Green, colorInputArray[j, i].Blue));
    //        }
    //    }

    //}

    //public void SaveImage(string path)
    //{
    //    RawImageSource.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);

    //}
}
