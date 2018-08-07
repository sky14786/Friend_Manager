using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager)FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        Input_Penel_Exit.onClick.AddListener(() => _MoveHomePanel());

        Data_Input_Btn.onClick.AddListener(() => _InputPanelOn());

        Update_Panel_Exit.onClick.AddListener(() => _MoveHomePanel());
    }

    public GameObject Data_Input_Panel, Data_Update_Panel, ALL_DATA_PANEL;
    public Button Input_Penel_Exit, Data_Input_Btn, Update_Panel_Exit, Update_Button,ID_Check_Button;
    public InputField Update_No, Update_Name, Update_Age, Update_Sex, Update_Phone, Update_Job, Update_Place, Update_Personality, Update_ETC;
    public InputField Create_ID, Create_PW, Create_Name,Create_PW_Check;
    public Text Create_ID_Check;


    public void _InputPanelOn()
    {
        ALLPanelOff();
        Data_Input_Panel.SetActive(true);
    }

    public void _MoveHomePanel()
    {
        ALLPanelOff();
        ALL_DATA_PANEL.SetActive(true);
    }
    public void _UpdatePanelOn()
    {
        ALLPanelOff();
        Data_Update_Panel.SetActive(true);
    }
    public void ALLPanelOff()
    {
        Data_Update_Panel.SetActive(false);
        Data_Input_Panel.SetActive(false);
        ALL_DATA_PANEL.SetActive(false);
    }
}
