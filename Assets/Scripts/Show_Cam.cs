using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Cam : MonoBehaviour
{


    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Camera_Scripts.Instance._CheckCam(this.transform.parent.GetChild(0).GetComponent<Text>().text)));
    }

  
}
