using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;

public class TechnicalData : MonoBehaviour
{
    //Playerのスクリプトから参照
    Player player;
    //PlayerDataのスクリプトから参照
    PlayerData playerD;


    //ツバメ返し時に出る自分周辺の当たり判定Obj
    [SerializeField] GameObject Attack_obj_tubame;

    //ハネトバシ時に出る攻撃オブジェクト
    [SerializeField] GameObject Attack_obj_wing;

    //Playerの座標を触るための(ストライク&back)
    [SerializeField] GameObject Player;

    //ストライク&backの前進後に自身がいた場所へマークする
    [SerializeField] GameObject Mark;


    //Playerの初期位置
    public static Vector3 PlayerLocation = new Vector2(0.0f, 0.0f);

    //Playerの移動先を保存するための
    public static Vector3 PlayerLocationGoal = new Vector2(0.0f, 0.0f);

    public Text TecCool1;                //技1のクールタイム表示
    public Text TecCool2;                //技2のクールタイム表示

    public int technicalNumber = 0;      //選んだ時点(Tec.1or2)での箱の役割
    public bool inactionableFlg = false; //一部の技が発動中、
                                         //操作を一定時間無効にするフラグ

    public int technicalNumber1 = 0;            //一個目の選択時に決めたわざを保存
    public int technicalNumber2 = 0;            //二個目の選択時に決めたわざを保存
    public bool technicalFlg1 = false;          //わざ1を発動したかのフラグ
    public bool technicalFlg2 = false;          //わざ2を発動したかのフラグ
    bool swallowReturn_F = false;               //ツバメ返しのフラグ

    float Waza_time = 0.0f;                     //わざを発動中の時間
    Vector3 StBackLoc = new Vector3(0.0f,2.0f); //ストライク&backの移動距離
    int wingCount = 0;                          //ハネトバシのカウント

    /****ストライク＆バック*****/
    [SerializeField] private float MoveSpeed = 1000.0f;    //移動速度
    private int TargetDistance = 2;                     //目的距離
    private float NowDistance = 0;                      //現在地
    private float Return = 0;                           //バック

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        player = GetComponent<Player>();
        playerD = GetComponent<PlayerData>();
        Attack_obj_tubame.SetActive(false);
        technicalFlg1 = false;
        technicalFlg2 = false;
        inactionableFlg = false;
        swallowReturn_F = false;
        technicalNumber = 0;
        technicalNumber1 = 0;
        technicalNumber2 = 0;
        Waza_time = 0.0f;
        Player.transform.position = PlayerLocation;
    }

    // Update is called once per frame
    void Update()
    {
        //（技:1）もしクールタイムがあるなら
        if (playerD.Tec01_CoolTime > 0)
        {
            Debug.Log("技1が使えるまで" + playerD.Tec01_CoolTime);
            playerD.Tec01_CoolTime -= Time.deltaTime;        //カウントダウン
        }
        else
        {
            /*もしクールタイムが0を下回ったら
            空きのクールタイムを0へ戻す*/
            playerD.Tec01_CoolTime = 0.0f;
        }

        //(技:2)もしクールタイムがあるなら
        if (playerD.Tec02_CoolTime >= 0)
        {
            Debug.Log("技2が使えるまで" + playerD.Tec02_CoolTime);
            playerD.Tec02_CoolTime -= Time.deltaTime;        //カウントダウン
        }
        else
        {
            /*もしクールタイムが0を下回ったら
            空きのクールタイムを0へ戻す*/
            playerD.Tec02_CoolTime = 0.0f;
        }


        //もしツバメ返しの技フラグがtrueなら
        if (swallowReturn_F)
            Waza_time += Time.deltaTime;

        //もし技1がtrueなら技の番号を判別後にswitchから発動
            if (technicalFlg1)
            {
                //技1に保存した番号をtecNumへ入れる
                technicalNumber = Fonts.Tec1;
            }
            else if (technicalFlg2)
            {
                technicalNumber = Fonts.Tec2;
            }
        
        

        //技選択時の区別受け取り
        switch (technicalNumber)
        {
            //技1 仮称「ハネトバシ」
            case 1:
                FeatherFlying();        //技1の関数を呼び出し
                break;

            //技2 仮称「ツバメ返し」
            case 2:
                SwallowReturn();
                break;

            //技3 仮称「ストライク＆back」
            case 3:
                StrikeBack();
                break;

            //技4 仮称「トッシン」
            case 4:
                Rush();
                break;

            //技の選択時何も選ばなかった時ランダムで処理する(仮)
            case 5:
                break;
        }
    }

    //待ち時間1,2をPlayerTextManagerへ受け渡し
    public int GetCoolTime1()
    {
        return (int)playerD.Tec01_CoolTime;
    }

    public int GetCoolTime2()
    {
        return (int)playerD.Tec02_CoolTime;
    }


    /*************フラグ打消しの処理***************************/
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
        //技1なら
        if (technicalFlg1)
        {
            //もしクールタイムが0秒以下なら
            if (playerD.Tec01_CoolTime <= 0)
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
                    playerD.Tec01_CoolTime = playerD.FlyingFeather_CoolTime;
                    Rest1_2();              //技1or2を使った最後にリセットする
                }
            }
        }


        //技2なら
        if (technicalFlg2)
        {
            //もしクールタイムが0秒以下なら
            if (playerD.Tec02_CoolTime <= 0)
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
                    //技2のクールタイムを入れる。
                    playerD.Tec02_CoolTime = playerD.SwallowReturn_CoolTime;
                    Rest1_2();              //技1or2を使った最後にリセットする
                }
            }
        }
    }

    //ツバメ返しの動き
    void SwallowReturn()
    {
        //技1なら
        if (technicalFlg1)
        {
            //もしクールタイムが0秒以下なら
            if (playerD.Tec01_CoolTime <= 0)
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
                    //PlayerのDataにある、空きのクールタイムに
                    //技1のクールタイムを入れる。
                    playerD.Tec01_CoolTime = playerD.FlyingFeather_CoolTime;
                    Rest1_2();              //技1or2を使った最後にリセットする
                }
            }
        }

        //技2なら
        if (technicalFlg2)
        {
            //もしクールタイムが0秒以下なら
            if (playerD.Tec02_CoolTime <= 0)
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
                    //PlayerのDataにある、空きのクールタイムに
                    //技1のクールタイムを入れる。
                    playerD.Tec02_CoolTime = playerD.SwallowReturn_CoolTime;
                    Rest1_2();              //技1or2を使った最後にリセットする
                }
            }
        }
    }

    

    //ストライク&backの動き
    void StrikeBack()
    {
        //3にすると連続して呼び出されるから別の数字を与える
        technicalNumber = 5;

        //Vector3 move = Player.transform.position / MoveSpeed;

        //ここで前進する
        if (player.stBackCount <= 1)
        {
            //座標を保存
            PlayerLocation = Player.transform.position;

            Debug.Log("一回目");
            
            //ストライクぬるりと移動する処理（呼び出し）
            StartCoroutine(Move(Vector3.up));

            Instantiate(Mark,       //生成するオブジェクトのプレハブ(Mark)
            PlayerLocation,         //初期位置は移動前にいた場所
            Quaternion.identity);   //初期回転情報
        }
        //もし技のボタンを2回押したら以前記録した場所へ戻る
        if (player.stBackFlg)
        {
            //座標登録をもとに現在の位置に反映させる
            this.transform.DOMove(PlayerLocation, 2.0f);
           // Player.transform.position = PlayerLocation;
            //StartCoroutine(Back());
            //情報を初期化
            technicalNumber = 0;
            player.stBackCount = 0;
            player.stBackFlg = false;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }


        /*********旧考えた処理（没)***************/
        ////技が発動
        //float x;
        //float y;
        ////現在位置を技が発動後に記録
        //Vector2 posi = this.transform.position;

        ////一度前進して帰る場合もあるため位置を保持
        //x = posi.x;
        //y = posi.y;
        //Debug.Log("保持の内容" + x + y);
        /********旧考えた処理Part2*****************/
        if (false)
        {
            //移動前の座標と移動先の座標を保存
            PlayerLocation = Player.transform.position;
            PlayerLocationGoal = Player.transform.position + StBackLoc;

            Debug.Log(PlayerLocationGoal.y - this.transform.position.y);

            Debug.Log("y");

            Debug.Log("現在の座標" + Player.transform.position);

            //移動先へ進んだ後、移動前にいた場所にマークを付与する。
            Instantiate(Mark,           //生成するオブジェクトのプレハブ(Mark)
                PlayerLocation,         //初期位置は移動前にいた場所
                Quaternion.identity);   //初期回転情報
        }
    }

    //移動(ストライク処理)
    IEnumerator Move(Vector3 TmpVector)
    {
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Debug.Log("目標＝"+NowDistance);
        while (true) 
            {
            //終了条件を満たしているか確認
            //現在の座標が移動する
            //もし現在の座標が移動先未満なら
            if (TargetDistance <= NowDistance +(MoveSpeed*Time.deltaTime))//終了条件
            {
                transform.position += TmpVector * (TargetDistance - NowDistance);
                playerD.ActionFlg = true;   //技発動中は他の操作を受け付けない（未完成）
                NowDistance = 0;            //移動前の座標リセット
                yield break;
            }
            //入力方向に移動
            transform.position += TmpVector * Time.deltaTime * MoveSpeed;
            
            //移動した距離を更新
            NowDistance += MoveSpeed * Time.deltaTime;
            Debug.Log("前回からの移動距離" + MoveSpeed * Time.deltaTime);
            Debug.Log("累計移動距離"+NowDistance);
            yield return null;
            }
    }

    //移動（バック）処理
    IEnumerator Back()
    {
        Vector3 distance = PlayerLocation - player.transform.position;
       // Vector3 BackAngleVector = new Vector3(Cos(), distance.y / MoveSpeed, 0);
            //終了条件を満たしているか確認
            //現在の座標が移動する
            //もし現在の座標が移動先未満なら
            if (PlayerLocation.y >= Player.transform.position.y)//終了条件
            {
                playerD.ActionFlg = true;   //技発動中は他の操作を受け付けない（未完成）
                NowDistance = 0;            //移動前の座標リセット
                Return = 0;                 //座標リセット
                yield break;
            }

            //入力方向に移動
          //  transform.position += BackAngleVector * Time.deltaTime;
            //移動した距離を更新
            Return -= MoveSpeed * Time.deltaTime;
            yield return null;
        
    }
    //トッシンの動き
    void Rush()
    {
        /*この関数へ切り替わるとPlayerDataでここの関数番号から技発動を検知し
         * playerDataでスタン処理を行います*/
        /*ここの関数の処理は自分の周囲から最も近いPlayerを検知し移動する処理*/
    }
}
