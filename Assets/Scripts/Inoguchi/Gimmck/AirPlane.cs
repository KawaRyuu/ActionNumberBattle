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

    public override void DecideGimmckDirection()
    {
        //画面サイズ
        Vector2 screen_size = new Vector3(Screen.width, Screen.height);

        //画面サイズとギミック(自身)の座標をビューポート座標に変換
        Vector3 screen_view_position = Camera.main.ScreenToViewportPoint(screen_size);
        Vector3 gimmick_view_position = Camera.main.WorldToViewportPoint(gimmick_position);

        //画面枠上の一箇所の座標
        Vector3 screen_frame_position = Vector3.zero;

        //ギミックと画面の比較した位置に応じて取得する枠を決める
        if (gimmick_view_position.x <= 0)
        {
            //左側

            //画面枠右の一点をランダム取得
            screen_frame_position.x = 1;
            screen_frame_position.y = Random.value;

            

        }
        else if (gimmick_view_position.x >= screen_view_position.x)
        {
            //右側

            //画面枠左の一点をランダム取得
            screen_frame_position.x = 0;
            screen_frame_position.y = Random.value;

        }
        else if (gimmick_view_position.y <= 0)
        {
            //上側

            //画面枠下の一点をランダム取得
            screen_frame_position.x = Random.value;
            screen_frame_position.y = 1;
        }
        else if (gimmick_view_position.y >= screen_view_position.y)
        {
            //下側

            //画面枠上の一点をランダム取得
            screen_frame_position.x = Random.value;
            screen_frame_position.y = 0;
        }

        //取得した画面枠座標から進む向きを算出
        gimmick_direction = Camera.main.ViewportToWorldPoint(screen_frame_position) - gimmick_position;

        //画面枠とギミックのz座標は同じとする
        gimmick_direction.z = 0;

        //求めた方向にギミックを向く
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, gimmick_direction);
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
