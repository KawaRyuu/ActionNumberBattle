using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number_of_People : MonoBehaviour
{
    //コントローラーマネージャーについてるスクリプトを参照
    ControllerConnection connection;
    public GameObject Manager;

    //文字変化させるテキスト数
   public Text text1;
   public Text text2;
   public Text text3;

    // Start is called before the first frame update
    void Start()
    {
        connection = Manager.GetComponent<ControllerConnection>();
    }

    // Update is called once per frame
    void Update()
    {
        //現在の人数からCPUが何体付くか判定
        switch (connection.GetControllerNumber())
        {
            case 0:
                return;
            case 1:
                Debug.Log("Playerは1人");
                TextChange(3);
                break;
            case 2:
                Debug.Log("Playerは2人");
                TextChange(2);
                break;
            case 3:
                Debug.Log("Playerは3人");
                TextChange(1);
                break;
            case 4:
                Debug.Log("Playerは4人");
                return;
        }
    }

    void TextChange(int cpu_num)
    {
        //CPU補充の人数
        switch (cpu_num)
        {
            case 1:
                text1.text = ("2Player");
                text2.text = ("3Player");
                text3.text = ("CPU1");
                break;
            case 2:
                text1.text = ("2Player");
                text2.text = ("CPU1");
                text3.text = ("CPU2");
                break;
            case 3:
                text1.text = ("CPU1");
                text2.text = ("CPU2");
                text3.text = ("CPU3");
                break;
        }
    }
}
