using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*プレイヤー（ライバルも含む）のデータを管理する。*/
//プレイヤーの基礎的なデータ
public class PlayerData : MonoBehaviour
{
    public SpriteRenderer player;

    public string Id = ("");         //おそらく名前
    public int Hp = 3;               //体力（耐久値）
    public int Attack = 1;           //攻撃
    public int NomberBox = 4;        //4つの数を保持する
    public int RandomNomber = 0;     //自分の数を出すランダム数をここに
    public float Speed = 5.0f;       //移動速度
    public float CoolTime = 4f;
    //技や奪取（交換）を含めたクールタイム全般

    public float invincibility = 0.5f;          //ダメージ喰らった際の無敵時間
    public bool Swoon_Flg = false;              //気絶フラグ
    public bool Stun_Flg = false;               //スタンフラグ
    public bool RecoveryTime_Flg = false;       //回復タイムに入るフラグ
    public bool Invincibility_Flg = false;      //無敵フラグ
    public bool ExchangeTakeover_Flg = false;   //交換奪取するフラグ
    public bool BluntFootEffect_Flg = false;    //鈍足効果のフラグ

    public float inv_count = 0.0f;              //無敵時間中のカウント
    public float blunt_count = 0.0f;            //鈍足中のカウント
    public float hael_count = 0.0f;             //回復中のカウント
    public float swoon_count = 0.0f;            //気絶中のカウント

    //初期化
    void Start()
    {
        Invincibility_Flg = false;
        Swoon_Flg = false;
        BluntFootEffect_Flg = false;
        inv_count = 0.0f;
        blunt_count = 0.0f;
        hael_count = 0.0f;
        swoon_count = 0.0f;
    }

    void Update()
    {
        /**********気絶の処理*************/
        //もし気絶フラグがtrue且つカウントが1.5秒以下なら
        if (Swoon_Flg && swoon_count <= 1.5f)
        {
            Debug.Log("気絶now");
            swoon_count += Time.deltaTime;    //カウントの加算
        }
        else if (swoon_count >= 1.5)          //もしカウントが1.5秒を超えたら
        {
            swoon_count = 0.0f;               //気絶カウントをリセット
            Swoon_Flg = false;              //気絶フラグをfalseに変える
            Hp = 3;                  //HPは強制で全回復
        }


        /**********回復の処理*************/
        //もし回復フラグがtrue且つカウントが5.0秒以下なら
        //※次の攻撃が来るのが5秒以降になるなら全回復する。
        if (RecoveryTime_Flg && hael_count <= 5.0f)
        {
            hael_count += Time.deltaTime;
            Debug.Log("回復中");
            //Debug.Log("回復カウントは" + hael_count);
        }
        //もし回復のカウントが5秒を超えたなら
        else if (hael_count > 5.0)
        {
            Debug.Log("回復");
            Hp = 3;                                 //Hpを回復する。
            hael_count = 0;                         //カウントリセット
            RecoveryTime_Flg = false;               //回復フラグOFF
        }



        /**********無敵の処理*************/

        //無敵フラグがON且つもし無敵時間が0.5秒以下なら
        if (Invincibility_Flg && invincibility >= inv_count)
        {
            inv_count += Time.deltaTime;
            RecoveryTime_Flg = true;                //回復フラグをONにする
            StartCoroutine(BlinkingControl());
            Invincibility();                        //無敵時の点滅処理関数へ
            Debug.Log("無敵時間" + inv_count);
        }
        else
        {
            //無敵解除
            Invincibility_Flg = false;
            inv_count = 0;
        }

        /**********鈍足効果の処理************/

        //もし鈍足効果のフラグがfalseなら通常の速度
        if (!BluntFootEffect_Flg)
        {
            Speed = 5.0f;
        }

        //鈍足効果がtrue且つ鈍足カウントが2.0秒以下なら
        if (BluntFootEffect_Flg && blunt_count <= 2.0)
        {
            blunt_count += Time.deltaTime;
        }
        else
        {
            BluntFootEffect_Flg = false;
            blunt_count = 0;
        }
    }


    /************当たった時の処理(何かの当たった時)*****************/
    private void OnTriggerEnter2D(Collider2D other)
    {
        //敵の攻撃（EnemyAttackというtag）に触れたとき
        if (other.gameObject.tag == "EnemyAttack")
        {

            //もし無敵状態じゃない且つ気絶してないときなら
            if (!Invincibility_Flg && !Swoon_Flg)
            {
                //フラグがtrueの時、追撃が飛んで来たら
                if (RecoveryTime_Flg)
                {
                    //回復のキャンセル
                    RecoveryTime_Flg = false;
                    hael_count = 0.0f;
                }

                Hp -= Attack;               //体力がAttackの攻撃参照で減る
                Invincibility_Flg = true;   //無敵フラグON
            }

            //もし体力が0以下になったら
            if (Hp <= 0)
            {
                //気絶フラグON
                Swoon_Flg = true;
            }
        }

        //もし鈍足効果のTagに触れたら
        if (other.gameObject.tag == "BluntFootEffect")
        {
            //速度を5から2.5の速度に変化する。
            Speed = 2.5f;
            BluntFootEffect_Flg = true;
        }
    }

    //移動速度UP関数
    public void SpeedUp()
    {
    }

    //無敵処理の関数(点滅処理)
    public void Invincibility()
    {
        //点滅の処理(*20は点滅速度)
        float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
        player.color = new Color(1f, 1f, 1f, level);
    }

    //点滅の制御関数
    public IEnumerator BlinkingControl()
    {
        //0.5秒の間点滅を繰り返す
        yield return new WaitForSeconds(0.5f);
        // 通常状態に戻す
        player.color = new Color(1f, 1f, 1f, 1f);
    }
}
