using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;



public class Birds : BaseGimmick
{
    //鳥の群れの速度
    const float bird_speed = 7.0f;

    private void Start()
    {
        //初期化
        GimmickInitialize(bird_speed, GIMMICK_ID.BIRD);

        //進む方向を決める
        DecideGimmckDirection();
    }

    //更新
    private void Update()
    {
        GimmickUpdate();
    }

   
  
}
