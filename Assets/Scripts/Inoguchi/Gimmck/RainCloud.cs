using GimmickInfomation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//雨雲のスクリプト

public class RainCloud : BaseGimmick
{
    const float rain_cloud_speed = 5.0f;

    private void Start()
    {
        GimmickInitialize(rain_cloud_speed, GIMMICK_ID.RAINCLOUD);

        //進む方向を決める
        DecideGimmckDirection();
    }

    private void Update()
    {
        GimmickUpdate();
    }

   
}
