using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerTextManager : MonoBehaviour
{
    //参照
    NumberData numD;
    TechnicalData tecD;
    PlayerInput playerInput;

    [SerializeField] Player[] players = new Player[2];

    /***Playerが持っている数字の表示***/
    public Text MyNnm1;                  //UIの表示
    public Text MyNnm2;                  //UIの表示

    /***Playerが持っている技の待ち時間表示***/
    public Text TecCool1;                //技1のクールタイム表示
    public Text TecCool2;                //技2のクールタイム表示


    ////Player(2p応急処置)
    //public Text p2MyNum1;
    //public Text p2MyNum2;
    //public Text p2TecCool1;
    //public Text p2TecCool2;

    // Start is called before the first frame update
    void Start()
    {
        //現在は"1Pのところにある"スクリプトDataをもってくるため注意
        numD = GameObject.Find("Player").GetComponent<NumberData>();
        tecD = GameObject.Find("Player").GetComponent<TechnicalData>();
    }

    // Update is called once per frame
    void Update()
    {
        //自分の数をUnityの画面で表示
        MyNnm1.text = numD.GetData(0).ToString()
            + (" ・ ") + numD.GetData(1).ToString();

        MyNnm2.text = numD.GetData(2).ToString()
            + (" ・ ") + numD.GetData(3).ToString();

        /**************技のクールタイム*************************/
        //文字表示
        TecCool1.text = tecD.GetCoolTime1()+ "秒";
        TecCool2.text = tecD.GetCoolTime2() + "秒";




        //p2MyNum1.text = numD.GetData(0).ToString()
        //    + (" ・ ") + numD.GetData(1).ToString();
        //p2MyNum2.text = numD.GetData(2).ToString()
        //    + (" ・ ") + numD.GetData(3).ToString();

        //p2TecCool1.text = tecD.GetCoolTime1() + "秒";
        //p2TecCool2.text = tecD.GetCoolTime2() + "秒";
    }
}
