using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WazaCoolTimeMove : MonoBehaviour
{
    PlayerData CoolTimeData;
    public Image UIobj;
    public bool roop;
    float Tec1_countTime = 1.0f;
    float Tec2_countTime = 0.0f;

    void Start()
    {
        CoolTimeData = GetComponent<PlayerData>();
    }
    // Update is called once per frame
    void Update()
    {
        if (CoolTimeData.Tec01_CoolTime > 0)
        {
            roop = true;
        }
        else
        {
            roop = false;
            UIobj.fillAmount = 1;
        }

        if (roop)
        {
            
            UIobj.fillAmount -= 1.0f / Tec1_countTime * Time.deltaTime;
        }

        if (UIobj.fillAmount <= 0)
        {
            UIobj.fillAmount = 1;
        }
    }
}
