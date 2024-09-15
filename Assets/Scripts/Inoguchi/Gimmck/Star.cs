using GimmickInfomation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : BaseGimmick
{
    const float star_speed = 15.0f;

    private void Start()
    {
        //初期化
        GimmickInitialize(star_speed, GIMMICK_ID.STAR);

        //進む方向を決める
        DecideGimmckDirection();
    }

    private void Update()
    {
        GimmickUpdate();
    }

    //ギミックの進む向きを決める
    public override void DecideGimmckDirection()
    {
        //画面サイズ
        Vector2 screen_size = new Vector3(Screen.width, Screen.height);

        //画面サイズとギミック(自身)の座標をビューポート座標に変換
        Vector3 screen_view_position  = Camera.main.ScreenToViewportPoint(screen_size);
        Vector3 gimmick_view_position = Camera.main.WorldToViewportPoint(gimmick_position);

        //画面枠上の一箇所の座標
        Vector3 screen_frame_position = Vector3.zero;

        //左下の一箇所に絞る
        int random = Random.Range(0, 6);

        //乱数をビューポート座標に置き換える
        screen_frame_position.y = random / 10.0f;
        
        //x座標を決める
        if (screen_frame_position.y <= 0.0f)
        {
            //yが0以下なら、対象座標は画面枠の下側の一つ
            screen_frame_position.x = Random.Range(0.0f, 0.5f);
        }
        else
        {
            //yが0より大きいなら、対象座標は画面枠の左側の一つ
            screen_frame_position.x = 0.0f;

        }


        //取得した画面枠座標から進む向きを算出
        gimmick_direction = Camera.main.ViewportToWorldPoint(screen_frame_position) - gimmick_position;

        //画面枠とギミックのz座標は同じとする
        gimmick_direction.z = 0;

        //求めた方向にギミックを向く
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, gimmick_direction);

    }

}
