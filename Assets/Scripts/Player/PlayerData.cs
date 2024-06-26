using System;
using System.Collections.Generic;
using UnityEngine;


/*プレイヤー（ライバルも含む）のデータを管理する。*/
//プレイヤーの基礎的なデータ
public class PlayerData : MonoBehaviour
{

    public string Id = ("");     //おそらく名前
    public int Hp = 3;        //体力（耐久値）
    public int Attack = 1;        //攻撃
    public int NomberBox = 4;        //4つの数を保持する
    public int RandomNomber = 0;        //自分の数を出すランダム数をここに
    public float Speed = 5.0f;     //移動速度
    public float CoolTime = 4f;
    //技や奪取（交換）を含めたクールタイム全般

    public float invincibility = 0.5f;     //ダメージ喰らった際の無敵時間
    public bool SwoonFlg = false;    //気絶フラグ
    public bool StunFlg = false;     //スタンフラグ
    public bool RecoveryTime_Flg = false;    //回復タイムに入るフラグ
    public bool Invincibility_Flg = false;    //無敵フラグ
    public bool ExchangeTakeover_Flg = false;    //交換奪取するフラグ
    public bool BluntFootEffect_Flg = false;    //鈍足効果のフラグ

    public float inv_count = 0.0f;      //無敵時間中のカウント
    public float blunt_count = 0.0f;    //鈍足中のカウント

    //初期化
    void Start()
    {
        Invincibility_Flg   = false;
        SwoonFlg            = false;
        BluntFootEffect_Flg = false;
        inv_count = 0.0f;
    }

     void Update()
    {
        //無敵フラグがON且つもし無敵時間が0.5秒以下なら
        if (Invincibility_Flg && invincibility >= inv_count)
        {
            inv_count += Time.deltaTime;
        }
        else
        {
            //無敵解除
            Invincibility_Flg = false;
            inv_count = 0;
        }

        //もし鈍足効果のフラグがfalseなら通常の速度
        if(!BluntFootEffect_Flg)
        {
            Speed = 5.0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //敵の攻撃（EnemyAttackというtag）に触れたとき
        if (other.gameObject.tag == "EnemyAttack")
        {
            //もし無敵状態じゃない且つ気絶してないときなら
            if (!Invincibility_Flg && !SwoonFlg)
            {
                Hp -= Attack;         //体力がAttackの攻撃参照で減る
                Invincibility_Flg = true;  //無敵フラグON
            }

            //もし体力が0以下になったら
            if (Hp >= 0)
            {
                //気絶処理
                SwoonFlg = true;
                Hp = 3;
            }
        }

        //もし鈍足効果のTagに触れたら
        if (other.gameObject.tag == "BluntFootEffect")
        {
            //速度を5から2.5の速度に変化する。
            Speed = 2.5f;
            BluntFootEffect_Flg = true;
        }
        else BluntFootEffect_Flg = false;
    }
}
