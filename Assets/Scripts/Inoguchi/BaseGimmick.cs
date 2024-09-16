using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;

//ギミックの親クラスのスクリプト

public class BaseGimmick : MonoBehaviour 
{
    //各種変数
    protected SpriteRenderer gimmick_sprite;                         //ギミックの描画
    protected GIMMICK_ID     gimmick_id　       = GIMMICK_ID.EMPTY;  //ギミックの種類の識別ID
    protected Vector3        gimmick_position   = Vector3.zero;      //ギミックの位置座標
    protected Vector3        gimmick_vector     = Vector3.zero;      //ギミックに働く力
    protected Vector3        gimmick_direction  = Vector3.zero;      //ギミックが進む方向
    protected float          gimmick_speed      = 1.0f;              //ギミックの移動速度
    protected bool           destroy_flag       = false;             //ギミックを破壊するか判定するフラグ
   
    //初期化
    public virtual void GimmickInitialize(float speed,GIMMICK_ID id)
    {
        //各種初期化
        gimmick_position    = this.transform.position;              //現在の座標を取得してくる
        gimmick_sprite      = this.GetComponent<SpriteRenderer>();  //自身についているSpriteRendererを取得してくる
        gimmick_vector      = Vector3.zero;                         //x,y,zを0に初期化
        gimmick_direction   = Vector3.zero;                         //x,y,zを0に初期化
        gimmick_speed       = speed;                                //引数で受け取った速度を代入
        gimmick_id          = id;                                   //受けとったギミックの種類の識別IDを取得
        destroy_flag        = false;                                //今は破壊しないのでFalse
        
    }

    //毎フレーム更新
    public virtual void GimmickUpdate()
    {
        //ギミックの動き
        GimmickMove();  
        
        //画面外判定
        CheckOffScreen();
    }

    //画面外判定
    public  void CheckOffScreen()
    {
        //画面に描画されいた時
        if (gimmick_sprite.isVisible)
        {
            //破壊判定をtrue
            destroy_flag = true;

            //描画中は破壊しないので返す
            return;
        }
           
        //破壊フラグが有効なら自身を破壊
        if (destroy_flag)
            Destroy(this.gameObject);

    }

    //ギミックの進む向き(方向)を決める
   public virtual void DecideGimmckDirection()
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

    //ギミックの動き
    public virtual void GimmickMove()
    {
        //ギミックの進む力を決める
        gimmick_vector = transform.right * gimmick_speed;

        //移動させる
        this.transform.position += gimmick_vector * Time.deltaTime;
    }
   
    //ギミックのIDを取得
    public GIMMICK_ID GetGIMMICK_ID()
    {
        //ギミックを識別するIDを返す
        return gimmick_id;
    }


}
