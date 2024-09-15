using GimmickInfomation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//雨雲のスクリプト

public class RainCloud : BaseGimmick
{
    const float rain_cloud_speed = 3.0f;

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

    public override void DecideGimmckDirection()
    {
        base.DecideGimmckDirection();

        //求めた方向にギミックを向く
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, gimmick_direction);
    }
}
