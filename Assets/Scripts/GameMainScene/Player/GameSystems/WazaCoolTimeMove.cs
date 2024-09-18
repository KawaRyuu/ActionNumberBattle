using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WazaCoolTimeMove : MonoBehaviour
{
    PlayerData[] CoolTimeData = new PlayerData[4];
    [SerializeField]private Image[] Ui_cooltime = new Image[8];

    int num = 0;
    //public Image UIobj2;

    public bool roop;
    public bool roop2;

    float Tec1_countTime = 1.0f;
    float Tec2_countTime = 1.0f;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0;i<CoolTimeData.Length; i++)
        {
            CoolTimeData[i] = players[i].GetComponent<PlayerData>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < CoolTimeData.Length; i++)
        {
            //もし技1のクールタイムが発生したら
            if (CoolTimeData[i].Tec01_CoolTime > 0)
            {
                roop = true;
            }
            else
            {
                //クールタイムが0秒になったら円をもとに戻す
                roop = false;
                Ui_cooltime[i].fillAmount = 1;
            }

            if (CoolTimeData[i].Tec02_CoolTime > 0)
            {
                roop2 = true;
            }
            else
            {
                //クールタイムが0秒になったら円をもとに戻す
                roop2 = false;
                Ui_cooltime[i+1].fillAmount = 1;
            }
            //roopがtrueなら
            if (roop)
            {
                //技1の待ち時間演出を始める。
                Ui_cooltime[i].fillAmount -= 1.0f / Tec1_countTime * Time.deltaTime;
               // Debug.Log("amout1=" + Time.deltaTime);
            }


            if (roop2)
            {
                //技2の待ち時間演出を始める。
                //UIobj2.fillAmount -= 1.0f / Tec2_countTime * Time.deltaTime;
                Ui_cooltime[i + 1].fillAmount -= 1.0f / Tec2_countTime * Time.deltaTime;
                //Debug.Log("amout=" + Time.deltaTime);
            }

            //もし1周したらもう一度もとに戻しループさせる
            if (Ui_cooltime[i].fillAmount <= 0)
            {
                Ui_cooltime[i].fillAmount = 1;
            }

            if (Ui_cooltime[i + 1].fillAmount <= 0)
            {
                Ui_cooltime[i + 1].fillAmount = 1;
            }
        }
        /*else if (UIobj2.fillAmount <= 0)
            UIobj2.fillAmount = 1;*/
    }

    //public void SetObj(Image image)
    //{
    //    UIobj = image;
    //}
}
