using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Update_Friend : MonoBehaviour {
    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(() => {
            Load_Data();
            UIManager.Instance._UpdatePanelOn();
        });
    }



    void Load_Data()
    {
        UIManager.Instance.Update_Name.text =this.transform.parent.GetChild(0).GetComponent<Text>().text;

        UIManager.Instance.Update_Age.text = this.transform.parent.GetChild(1).GetComponent<Text>().text;

        UIManager.Instance.Update_Sex.text = this.transform.parent.GetChild(2).GetComponent<Text>().text;

        UIManager.Instance.Update_Phone.text = this.transform.parent.GetChild(3).GetComponent<Text>().text;

        UIManager.Instance.Update_Job.text = this.transform.parent.GetChild(4).GetComponent<Text>().text;

        UIManager.Instance.Update_Place.text = this.transform.parent.GetChild(5).GetComponent<Text>().text;

        UIManager.Instance.Update_Personality.text = this.transform.parent.GetChild(6).GetComponent<Text>().text;

        UIManager.Instance.Update_ETC.text = this.transform.parent.GetChild(7).GetComponent<Text>().text;
    }
}
