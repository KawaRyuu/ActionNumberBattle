using GimmickInfomation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UFO : BaseGimmick
{
    //各種定数
    const float ufo_speed          = 20.0f;     //UFOの移動速度             
    const int   ufo_max_move_count = 5;         //UFOの最大移動回数
              
    //各種変数
    Vector3 ufo_next_position = Vector3.zero;   //UFOの次に向かう座標
    Vector3 max_move_distance = Vector3.zero;   //UFOの移動距離
    Vector3 now_move_distance = Vector3.zero;   //UFOの現在の移動量
    int     ufo_move_count    = 0;              //UFOの現在の移動回数
    int     random_move_count = 0;              //UFOの移動回数

    private void Start()
    {
        //初期化
        GimmickInitialize(ufo_speed, GIMMICK_ID.UFO);

        //進む方向を決める
        DecideGimmckDirection();
    }

    public override void GimmickInitialize(float speed, GIMMICK_ID id)
    {
        base.GimmickInitialize(speed, id);

        random_move_count = Random.Range(0, ufo_max_move_count);
        Debug.Log(random_move_count);
    }

    private void Update()
    {
        //更新
        GimmickUpdate();

        //座標に到達しているか確認
        CheckUfoPosition();
    }

    //ギミックの進む向き(方向)を決める
    public override void DecideGimmckDirection()
    {
        Vector3 screen_position = Vector3.zero;

        //画面内の一箇所の座標をランダム取得
        screen_position.x = Random.value;
        screen_position.y = Random.value;

        //取得した座標をWorldポジションに直した値が、次に目指す座標
        ufo_next_position = Camera.main.ViewportToWorldPoint(screen_position);

        //z座標はカメラ座標に合わされているので、ギミック座標に合わせる
        ufo_next_position.z = 0;

        //取得した座標から移動距離を算出
        max_move_distance.x = Mathf.Abs(ufo_next_position.x - gimmick_position.x);
        max_move_distance.y = Mathf.Abs(ufo_next_position.y - gimmick_position.y);

        //取得した座標から進む向きを算出
        gimmick_direction = ufo_next_position - gimmick_position;

        //求めた方向にギミックを向く
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, gimmick_direction);

    }

    //目標座標まで到達しているか確認
   void CheckUfoPosition()
    {
        //指定回数動いているなら返す
        if (ufo_move_count >= random_move_count)
            return;

        //自分の座標を代入
        gimmick_position = this.transform.position;

        //現移動量が、移動量以上ならば次の座標を決める
        if(now_move_distance.x >= max_move_distance.x &&
           now_move_distance.y >= max_move_distance.y)
        {
            //次の座標を決め、その方向に向く
            DecideGimmckDirection();

            //移動回数加算
            ufo_move_count++;

            //現移動量初期化
            now_move_distance = Vector3.zero;
        }

    }

    //ギミックの動き
    public override void GimmickMove()
    {
        //親クラスの関数呼び出し
        base.GimmickMove();

        //移動量を現移動量に加算
        now_move_distance.x += Mathf.Abs(gimmick_vector.x * Time.deltaTime);
        now_move_distance.y += Mathf.Abs(gimmick_vector.y * Time.deltaTime);
    }

}
