using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour {
    private static SystemManager instance;
    public static SystemManager Instance
    {
        get
        {
            if(instance==null)
            {
                instance = (SystemManager)FindObjectOfType<SystemManager>();
            }
            return instance;
        }
    }
    public GameObject one_recorde,Create;
    public int temp;
    public bool isFirst = true;




}
