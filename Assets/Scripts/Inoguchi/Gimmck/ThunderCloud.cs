using GimmickInfomation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//雷雲のスクリプト

public class ThunderCloud : BaseGimmick
{
    const float thunder_cloud_speed = 5.0f;

    private void Start()
    {
        GimmickInitialize(thunder_cloud_speed, GIMMICK_ID.THUNDERCLOUD);

        //進む方向を決める
        DecideGimmckDirection();

    }

    private void Update()
    {
        GimmickUpdate();
    }

}
