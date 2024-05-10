using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TechnicalData : MonoBehaviour
{
    //Playerのスクリプトから参照
    Player player;
    //PlayerDataのスクリプトから参照
    PlayerData playerD;

    Fonts font;

    //ツバメ返し時に出る自分周辺の当たり判定Obj
    [SerializeField] GameObject Attack_obj_tubame;
    //ハネトバシ時に出る攻撃オブジェクト
    [SerializeField] GameObject Attack_obj_wing;

    public Text TecCool1;                //技1のクールタイム表示
    public Text TecCool2;                //技2のクールタイム表示
    public int technicalNumber = 0;      //選んだ時点(Tec.1or2)での箱の役割
    public bool inactionableFlg = false; //一部の技が発動中、
                                         //操作を一定時間無効にするフラグ

    public int technicalNumber1 = 0;            //一個目の選択時に決めたわざを保存
    public int technicalNumber2 = 0;            //二個目の選択時に決めたわざを保存
    bool technicalFlg = false;                  //わざ発動したかのフラグ
    public bool technicalFlg1 = false;          //わざ1を発動したかのフラグ
    public bool technicalFlg2 = false;          //わざ2を発動したかのフラグ
    bool swallowReturn_F = false;               //ツバメ返しのフラグ

    float Waza_time = 0.0f;                     //わざを発動中の時間
    int wingCount = 0;                          //ハネトバシのカウント

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        player = GetComponent<Player>();
        playerD = GetComponent<PlayerData>();
        font = GetComponent<Fonts>();
        Attack_obj_tubame.SetActive(false);
        technicalFlg = false;
        technicalFlg1 = false;
        technicalFlg2 = false;
        inactionableFlg = false;
        swallowReturn_F = false;
        technicalNumber = 0;
        technicalNumber1 = 0;
        technicalNumber2 = 0;
        Waza_time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        //文字表示
        TecCool1.text = (int)playerD.Tec0_CoolTime + "秒";

        //もしクールタイムがあるなら
        if (playerD.Tec0_CoolTime >= 0)
        {
            Debug.Log("技1が使えるまで" + playerD.Tec0_CoolTime);
            playerD.Tec0_CoolTime -= Time.deltaTime;        //カウントダウン
        }
        else
        {
            /*もしクールタイムが0を下回ったら
            空きのクールタイムを0へ戻す*/
            playerD.Tec0_CoolTime = 0.0f;
        }


        //もしツバメ返しの技フラグがtrueなら
        if (swallowReturn_F)
            Waza_time += Time.deltaTime;

        //もし技を使用したなら
        if (technicalFlg)
        {
            //もし技1がtrueなら技の番号を判別後にswitchから発動
            if (technicalFlg1)
            {
                //技1に保存した番号をtecNumへ入れる
                technicalNumber = font.Tec1;
            }
            else if (technicalFlg2)
            {
                technicalNumber = font.Tec2;
            }
        }
        

        //技選択時の区別受け取り
        switch (technicalNumber)
        {
            //技1 仮称「ハネトバシ」
            case 1:
                FeatherFlying();        //技1の関数を呼び出し
                Reset();                //技使用時に技使用フラグをリセット
                break;

            //技2 仮称「ツバメ返し」
            case 2:
                SwallowReturn();
                Reset();                //技使用時に技使用フラグをリセット
                break;

            //技3 仮称「ストライク＆back」
            case 3:
                StrikeBack();
                Reset();                //技使用時に技使用フラグをリセット
                break;

            //技4 仮称「トッシン」
            case 4:                                                                                                                                                                                         
                Rush();
                Reset();                //技使用時に技使用フラグをリセット
                break;

            //技の選択時何も選ばなかった時ランダムで処理する(仮)
            case 5:
                Reset();                //技使用時に技使用フラグをリセット
                break;
        }

        //技1or2を使った最後にリセットする
        Rest1_2();
    }

    /*************フラグ打消しの処理***************************/
    private void Reset()
    {
        technicalFlg = false;
    }
    private void Rest1_2()
    {
        //もしtechnicalFlg1がtrueなら
        if (technicalFlg1)
            technicalFlg1 = false;
        //もしtechnicalFlg2がtrueなら
        else if (technicalFlg2)
            technicalFlg2 = false;

    }

    /*************それぞれの技の動き処理***********************/

    //ハネトバシの動き
    void FeatherFlying()
    {
        //もしクールタイムが0秒以下なら
        if (playerD.Tec0_CoolTime <= 0)
        {
            if (wingCount < 3)
            {
                //弾の生成
                //200フレームに1度だけ弾を発射する
                if (Time.frameCount % 200 == 0)
                {
                    //ハネトバシ生成
                    Instantiate(Attack_obj_wing,//生成するオブジェクトのプレハブ
                        this.transform.position,//初期位置
                        Quaternion.identity);//初期回転情
                    wingCount++;
                }
                //Debug.Log("wingCountは" + wingCount);
            }
            else
            {
                //羽の状態を初期化
                wingCount = 0;
                technicalNumber = 0;
                //PlayerのDataにある、空きのクールタイムに
                //技1のクールタイムを入れる。
                playerD.Tec0_CoolTime = playerD.Tec1_CoolTime;
            }
        }
    }

    //ツバメ返しの動き
    void SwallowReturn()
    {
        //もし時間が1.5秒以下なら
        if (Waza_time < 1.5f)
        {
            //行動不可のフラグを一時的にONにし、
            //playerの操作scriptで操作を不可にさせる
            inactionableFlg = true;
            Attack_obj_tubame.SetActive(true);         //技の範囲の当たり判定を表示
            swallowReturn_F = true;
        }
        else if (Waza_time > 1.5f)
        {
            inactionableFlg = false;
            Attack_obj_tubame.SetActive(false);
            swallowReturn_F = false;
            Waza_time = 0.0f;
            technicalNumber = 0;
        }
    }

    //ストライク&backの動き
    void StrikeBack()
    {
        int count = 0;
        //技が発動
        float x;
        float y;
        //現在位置を技が発動後に記録
        Vector2 posi = this.transform.position;

        //一度前進して帰る場合もあるため位置を保持
        x = posi.x;
        y = posi.y;
        Debug.Log("保持の内容" + x + y);

        //ここで前進する
        if (count < 1)
        {
            Debug.Log("カウント" + count);
            posi.y = 2.0f + Time.deltaTime;
            this.transform.position = posi;
            count++;
        }
        //もし技のボタンを2回押したら以前記録した場所へ戻る
        if (player.stBackFlg)
        {
            posi.x = x;
            posi.y = y;
            Debug.Log("現在のポジションは" + posi.x + posi.y);
            //情報を初期化
            player.stBackFlg = false;
            technicalNumber = 0;
        }

    }

    //トッシンの動き
    void Rush()
    {
        /*この関数へ切り替わるとPlayerDataでここの関数番号から技発動を検知し
         * playerDataでスタン処理を行います*/
        /*ここの関数の処理は自分の周囲から最も近いPlayerを検知し移動する処理*/
    }
}
