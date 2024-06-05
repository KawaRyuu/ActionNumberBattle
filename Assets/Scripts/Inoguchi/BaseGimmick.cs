using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;

//ギミックの親オブジェクト
public class BaseGimmick : MonoBehaviour 
{
    protected GIMMICK_ID     gimmick_id;                        //識別子
    protected SpriteRenderer gimmick_sprite;                    //描画コンポーネント
    protected Vector3        gimmick_position = Vector3.zero;   //位置座標
    protected Vector3        gimmick_velocity = Vector3.zero;   //加速度
    protected Vector3        gimmick_direction= Vector3.zero;   //方向
    protected float          gimmick_speed    = 1.0f;           //移動速度
    protected bool           destroy_flag     = false;          //破壊判定フラグ
    

    //初期化
    public virtual void GimmickInitialize(float speed,GIMMICK_ID id)
    {
        //各種初期化
        gimmick_position    = this.transform.position;
        gimmick_sprite      = this.GetComponent<SpriteRenderer>();
        gimmick_velocity    = Vector3.zero;
        gimmick_direction   = Vector3.zero;
        gimmick_speed       = speed;
        gimmick_id          = id;
        destroy_flag        = false;

        //進む方向を決める
        DecideGimmckDirection();
    }

    //更新
    public virtual void GimmickUpdate()
    {
        GimmickMove();
        CheckOffScreen();
    }

    

    //画面外判定
    public  void CheckOffScreen()
    {
        


      
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


    public virtual void GimmickMove()
    {
        gimmick_velocity = transform.right * gimmick_speed;

        this.transform.position = gimmick_velocity * Time.deltaTime;
    }
   


    //ギミックのIDを取得
    public GIMMICK_ID GetGIMMICK_ID()
    {
        return gimmick_id;
    }


}
