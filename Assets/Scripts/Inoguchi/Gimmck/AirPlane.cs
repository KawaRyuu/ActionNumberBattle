using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;

//飛行機のスクリプト

public class AirPlane : BaseGimmick
{
    const float airplane_speed = 12.0f; //飛行機の移動速度


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

    public override void GimmickMove()
    {
        //ギミックの進む力を決める
        gimmick_vector = transform.right* gimmick_speed;

        //移動させる
        this.transform.position += gimmick_vector * Time.deltaTime;
    }


}
