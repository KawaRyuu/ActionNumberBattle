using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WazaCoolTimeMove : MonoBehaviour
{
    PlayerData CoolTimeData;
    public Image UIobj;
    //public Image UIobj2;

    public bool roop;
    public bool roop2;

    float Tec1_countTime = 1.0f;
    float Tec2_countTime = 1.0f;

    void Start()
    {
        CoolTimeData = GetComponent<PlayerData>();
    }
    // Update is called once per frame
    void Update()
    {
        //もし技1のクールタイムが発生したら
        if (CoolTimeData.Tec01_CoolTime > 0)
        {
            roop = true;
        }
        else
        {
            //クールタイムが0秒になったら円をもとに戻す
            roop = false;
            UIobj.fillAmount = 1;
        }

        //roopがtrueなら
        if (roop)
        {
            //技1の待ち時間演出を始める。
            UIobj.fillAmount -= 1.0f / Tec1_countTime * Time.deltaTime;
        }
        else if(roop2)
        {
            //技2の待ち時間演出を始める。
            //UIobj2.fillAmount -= 1.0f / Tec2_countTime * Time.deltaTime;
        }

        //もし1周したらもう一度もとに戻しループさせる
        if (UIobj.fillAmount <= 0)
        {
            UIobj.fillAmount = 1;
        }
        /*else if (UIobj2.fillAmount <= 0)
            UIobj2.fillAmount = 1;*/
    }
}
