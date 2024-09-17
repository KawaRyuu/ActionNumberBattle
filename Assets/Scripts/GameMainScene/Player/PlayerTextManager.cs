using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerTextManager : MonoBehaviour
{
    //参照
    NumberData[]    numD    = new NumberData[4];
    TechnicalData[] tecD    = new TechnicalData[4];
    PlayerData[]    playerD = new PlayerData[4];

    PlayerInput playerInput;

    Player[] players = new Player[4];

    /***Playerが持っている数字の表示***/
    [SerializeField] Text[] player_num_texts = new Text[4];

    /***Playerが持っている技の待ち時間表示***/
    [SerializeField] Text[] player_cooltime_texts = new Text[8];

    [SerializeField] Text[] player_swoon_texts = new Text[4];

    int num = 0;

    ////Player(2p応急処置)
    //public Text p2MyNum1;
    //public Text p2MyNum2;
    //public Text p2TecCool1;
    //public Text p2TecCool2;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players  = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            numD[i] = players[i].GetComponent<NumberData>();
            tecD[i] = players[i].GetComponent<TechnicalData>();
            playerD[i] = players[i].GetComponent<PlayerData>();
        }

        //現在は"1Pのところにある"スクリプトDataをもってくるため注意
    }

    // Update is called once per frame
    void Update()
    {
        //自分の数をUnityの画面で表示
        for(int i = 0;i < player_num_texts.Length;i++)
        {
            player_num_texts[i].text = numD[i].GetData(0).ToString()+(" ・ ") + 
                                       numD[i].GetData(1).ToString() + "\n"+
                                       numD[i].GetData(2).ToString() + (" ・ ") +
                                       numD[i].GetData(3).ToString(); 
        }


       

        /**************技のクールタイム*************************/
        for(int i = 0;i<tecD.Length; i++)
        {
            player_cooltime_texts[num].text   = tecD[i].GetCoolTime1().ToString();
            player_cooltime_texts[num+1].text = tecD[i].GetCoolTime2().ToString();

           // Debug.Log("tecD = "+ tecD.Length);
           // Debug.Log("num = "+ num);

            if(num+1 < player_cooltime_texts.Length)
                num += 2;
        }

        
        for(int i = 0; i< player_swoon_texts.Length; i++)
        {
            if (playerD[i].GetPlayerState() == PlayerData.PLAYER_STATE.SWOON)
                player_swoon_texts[i].text = "気絶中";
            else
                player_swoon_texts[i].text = " ";
        }



        //p2MyNum1.text = numD.GetData(0).ToString()
        //    + (" ・ ") + numD.GetData(1).ToString();
        //p2MyNum2.text = numD.GetData(2).ToString()
        //    + (" ・ ") + numD.GetData(3).ToString();

        //p2TecCool1.text = tecD.GetCoolTime1() + "秒";
        //p2TecCool2.text = tecD.GetCoolTime2() + "秒";
    }

    private void LateUpdate()
    {
        num = 0;
    }
}
