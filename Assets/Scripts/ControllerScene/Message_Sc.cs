using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message_Sc : MonoBehaviour
{
    public Text ControllerMessage;
    // Start is called before the first frame update
    void Start()
    {
        ControllerMessage.text = ("コントローラー設定");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
