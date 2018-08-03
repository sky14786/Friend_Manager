using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(instance==null)
            {
                instance = (UIManager)FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    public GameObject Data_Input_Panel,Date_Update_Panel;

    public InputField Update_Name, Update_Age, Update_Sex, Update_Phone, Update_Job, Update_Place, Update_Personality, Update_ETC;

  public void _InputPanelOn()
    {
        Data_Input_Panel.SetActive(true);
    }
}
