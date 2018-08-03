using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour {
    private void Awake()
    {
        if(this.name=="Input_Button")
        {
            this.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance._InputPanelOn());
        }
    }
}
