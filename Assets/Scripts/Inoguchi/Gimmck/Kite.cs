using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;


public class Kite : BaseGimmick
{
    Vector3     kite_velocity = Vector3.zero;  //凧特有の力
    float       kite_angle    = 0.0f;          //角度
    const float kite_speed    = 5.0f;          //凧の速度

    private void Start()
    {
        GimmickInitialize(kite_speed, GIMMICK_ID.KITE);

      
    }

    private void Update()
    {
        GimmickUpdate();
    }

  

    public override void GimmickMove()
    {
        //ギミックとしてのベクトル
        gimmick_velocity = transform.right * gimmick_speed;

        ///カイト特有の左右に揺れる
        kite_velocity = transform.up * Mathf.Sin(kite_angle) * gimmick_speed;
        kite_angle += 0.01f;

        //ギミックとカイト特有を合わせたベクトル
        this.transform.position += (gimmick_velocity + kite_velocity) * Time.deltaTime;
    }


}
