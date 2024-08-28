using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;

public class AirPlane : BaseGimmick
{
    const float airplane_speed = 12.0f; //飛行機の速度

    private void Start()
    {
        GimmickInitialize(airplane_speed, GIMMICK_ID.AIRPLANE);

        //進む方向を決める
        DecideGimmckDirection();
    }

    private void Update()
    {
        GimmickUpdate();
    }

 
}
