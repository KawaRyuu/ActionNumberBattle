using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*プレイヤー（ライバルも含む）のデータを管理する。*/
//プレイヤーの基礎的なデータ
public class PlayerData : MonoBehaviour
{
    public SpriteRenderer player;
    public TechnicalData tec;
    public NumberData number;

    public string Id = ("");         //おそらく名前
    public int Hp = 3;               //体力（耐久値）
    public int Attack = 1;           //攻撃
    public float Speed = 3.0f;       //移動速度

    //文字表示
    public Text SwoonFont;           //気絶時の文字&カウントを表示

    /********クールタイムのカウントたち************/
    public float Tec01_CoolTime = 0.0f;           //技1:空のクールタイム
    public float Tec02_CoolTime = 0.0f;           //技2:空のクールタイム
    public float FlyingFeather_CoolTime = 11.0f;  //技1専用のクールタイム時間
    public float SwallowReturn_CoolTime = 11.0f;  //技2専用のクールタイム時間
    public float StrikeBack_CoolTime = 11.0f;     //技3専用のクールタイム時間
    public float Rush_CoolTime = 11.0f;           //技4専用のクールタイム時間

    public float invincibility = 0.5f;          //ダメージ喰らった際の無敵時間
    public bool Swoon_Flg = false;              //気絶フラグ
    public bool Stun_Flg = false;               //スタンフラグ
    public bool RecoveryTime_Flg = false;       //回復タイムに入るフラグ
    public bool Invincibility_Flg = false;      //無敵フラグ
    public bool ExchangeTakeover_Flg = false;   //交換奪取するフラグ
    public bool BluntFootEffect_Flg = false;    //鈍足効果のフラグ
    public bool Swaps_Flg = false;              //交換のフラグ

    public float inv_count = 0.0f;              //無敵時間中のカウント
    public float stun_count = 0.0f;             //スタン中のカウント
    public float blunt_count = 0.0f;            //鈍足中のカウント
    public float hael_count = 0.0f;             //回復中のカウント
    public float swoon_count = 0.0f;            //気絶中のカウント
    public float swoon_countDown = 2.0f;        //気絶時の文字(カウントダウン)

    public GameObject SwoonObj;                 //気絶時に出るobj(これで判定させる)

    //初期化
    void Start()
    {
        tec = GetComponent<TechnicalData>();
        number = GetComponent<NumberData>();
        SwoonObj.SetActive(false);
        Invincibility_Flg = false;
        Swoon_Flg = false;
        Stun_Flg = false;
        BluntFootEffect_Flg = false;
        Swaps_Flg = false;

        inv_count = 0.0f;
        blunt_count = 0.0f;
        hael_count = 0.0f;
        swoon_count = 0.0f;
        swoon_countDown = 2.0f;
        stun_count = 0.0f;
        Tec01_CoolTime = 0.0f;
        Tec02_CoolTime = 0.0f;
        FlyingFeather_CoolTime = 10.0f;
        SwallowReturn_CoolTime = 10.0f;
        StrikeBack_CoolTime = 10.0f;
        Rush_CoolTime = 10.0f;
    }

    void Update()
    {
        //スタン関数
        Stun();

        //気絶関数
        Swoon();

        //回復関数
        Recovery();

        //無敵関数
        Inv();

        //鈍足関数
        BluntFoot();
    }


    /**********スタンの処理************/
    public void Stun()
    {
        //もしスタンフラグがtrue且つカウントが1.0秒以下なら
        if (Stun_Flg && stun_count <= 1.0f)
        {
            Debug.Log("スタン中");
            stun_count += Time.deltaTime;   //カウント加算
        }
        else if (stun_count >= 1)           //もしカウントが1秒を超えたら
        {
            //スタン状態を解除
            stun_count = 0.0f;
            Stun_Flg = false;
        }
    }

    /**********気絶の処理*************/
    public void Swoon()
    {
        //もし気絶フラグがtrue且つカウントが1.5秒以下なら
        if (Swoon_Flg && swoon_count <= 2f)
        {
            Debug.Log("気絶now");
            //気絶時プレイヤーが分かりやすいように文字を表示
            SwoonFont.text = "気絶中..." + (int)swoon_countDown;
            SwoonObj.SetActive(true);         //気絶時交換されるように当たり判定をON
            swoon_count += Time.deltaTime;    //カウントの加算
            swoon_countDown -= Time.deltaTime;          //カウントダウン
        }
        else if (swoon_count >= 2)          //もしカウントが1.5秒を超えたら
        {
            swoon_count = 0.0f;             //気絶カウントをリセット
            swoon_countDown = 2.0f;         //カウントリセット
            Swoon_Flg = false;              //気絶フラグをfalseに変える
            SwoonFont.text = " ";           //文字を消す
            Hp = 3;                         //HPは強制で全回復
            SwoonObj.SetActive(false);
        }
    }

    /**********回復の処理*************/
    public void Recovery()
    {
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
    }

    /**********無敵の処理*************/
    public void Inv()
    {
        //無敵フラグがON且つもし無敵時間が0.5秒以下なら
        if (Invincibility_Flg && invincibility >= inv_count)
        {
            inv_count += Time.deltaTime;
            RecoveryTime_Flg = true;                //回復フラグをONにする
            StartCoroutine(BlinkingControl());
            Invincibility();                        //無敵時の点滅処理関数へ
            //Debug.Log("無敵時間" + inv_count);
        }
        else
        {
            //無敵解除
            Invincibility_Flg = false;
            inv_count = 0;
        }
    }

    /**********鈍足効果の処理************/
    public void BluntFoot()
    {
        //もし鈍足効果のフラグがfalseなら通常の速度
        if (!BluntFootEffect_Flg)
        {
            Speed = 3.0f;
        }

        //鈍足効果がtrue且つ鈍足カウントが2.0秒以下なら
        if (BluntFootEffect_Flg && blunt_count <= 2.0)
        {
            blunt_count += Time.deltaTime;
        }
        else
        {
            //鈍足状態解除
            BluntFootEffect_Flg = false;
            blunt_count = 0;
        }
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





    /************当たった時の処理(何かの当たった時)*****************/
    private void OnTriggerEnter2D(Collider2D other)
    {
        //敵の攻撃（EnemyAttackというtag）に触れたとき
        if (other.gameObject.tag == "EnemyAttack")
        {
            Debug.Log("痛い");
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
            //速度を3から1.5の速度に変化する。
            Speed = 1.5f;
            BluntFootEffect_Flg = true;
        }
        //トッシン(技)が発動した際Playerに触れたとき
        if (other.gameObject.tag == "Player" && tec.technicalNumber == 4)
        {
            Stun_Flg = true;
        }

        //もし気絶tagに触れたら
        if (other.gameObject.tag == "Swoon")
        {
            Debug.Log("交換フラグは" + Swaps_Flg);
            Swaps_Flg = true;               //交換のフラグをtureにする
        }
    }
}
