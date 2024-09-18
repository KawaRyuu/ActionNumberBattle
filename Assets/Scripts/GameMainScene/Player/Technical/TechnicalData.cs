using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEngine.Windows;
using UnityEngine.EventSystems;

public class TechnicalData : MonoBehaviour
{
    //Playerのスクリプトから参照
    Player player;
    //PlayerDataのスクリプトから参照
    PlayerData playerD;

    RushRangeJudge rushRangeJudge;

    //子オブジェクトの当たり判定についているスクリプト
    Attack_ID_Sc attack_id_sc;

    //ツバメ返し時に出る自分周辺の当たり判定Obj
    [SerializeField] GameObject Attack_obj_tubame;

    //ハネトバシ時に出る攻撃オブジェクト
    [SerializeField] GameObject Attack_obj_wing;

    //Playerの座標を触るための(ストライク&back)
    [SerializeField] GameObject Player;

    //ストライク&backの前進後に自身がいた場所へマークする
    [SerializeField] GameObject Mark;

    //ハネトバシ
    [SerializeField] GameObject Wing1;
    [SerializeField] GameObject Wing2;
    [SerializeField] GameObject Wing3;

    /******ストライク&バックに使うLocation********/

    //Playerがストライクで進む際に使うLocation
    public static Vector3 PlayerLocation = new Vector2(0.0f, 0.0f);

    //Playerが元の位置へ戻る際仕様するLocation
    public Vector3 PlayerReturnLocation = new Vector2(0.0f, 0.0f);
    //Playerの移動先の距離
    public static Vector3 PlayerLocationDistance = new Vector2(0.0f, 2.0f);



    public Text TecCool1;                       //技1のクールタイム表示
    public Text TecCool2;                       //技2のクールタイム表示

    public int technicalNumber = 0;             //選んだ時点(Tec.1or2)での箱の役割
    public bool inactionableFlg = false;        //一部の技が発動中、
                                                //操作を一定時間無効にするフラグ

    public int technicalNumber1 = 0;            //一個目の選択時に決めたわざを保存
    public int technicalNumber2 = 0;            //二個目の選択時に決めたわざを保存
    public bool technicalFlg1 = false;          //わざ1を発動したかのフラグ
    public bool technicalFlg2 = false;          //わざ2を発動したかのフラグ
    bool swallowReturn_F = false;               //ツバメ返しのフラグ

    float Waza_time = 0.0f;                     //わざを発動中の時間
    int wingCount = 0;                          //ハネトバシのカウント

    //public GameObject TecAttack;                //技の当たり判定

    /****ストライク＆バック****/
    [SerializeField] private const float MoveSpeed = 1.0f;      //移動速度
    Rigidbody2D player_rb2d;
    float move_distance = 3.0f;

    /****トッシン****/
    [SerializeField] private GameObject rush_attack_range;
    public bool rush_target_flg = false;
    bool target_flg = false;
    Vector3     target_position;            //ターゲット


    // Start is called before the first frame update
    void Start()
    {
        //初期化
        player = GetComponent<Player>();
        playerD = GetComponent<PlayerData>();
        player_rb2d = GetComponent<Rigidbody2D>();
        rushRangeJudge = GetComponentInChildren<RushRangeJudge>();
        attack_id_sc = GetComponentInChildren<Attack_ID_Sc>();
        Attack_obj_tubame.SetActive(false);
        rush_attack_range.SetActive(false);
        //TecAttack.SetActive(false);
        technicalFlg1 = false;
        technicalFlg2 = false;
        inactionableFlg = false;
        swallowReturn_F = false;
        target_flg = false;
        technicalNumber = 0;
        technicalNumber1 = 0;
        technicalNumber2 = 0;
        Waza_time = 0.0f;
        Player.transform.position = PlayerReturnLocation;

        target_position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //（技:1）もしクールタイムがあるなら
        if (playerD.Tec01_CoolTime > 0)
        {
            playerD.Tec01_CoolTime -= Time.deltaTime;        //カウントダウン
        }
        else
        {
            /*もしクールタイムが0を下回ったら
            空きのクールタイムを0へ戻す*/
            playerD.Tec01_CoolTime = 0.0f;
        }

        //(技:2)もしクールタイムがあるなら
        if (playerD.Tec02_CoolTime > 0)
        {
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
            //もし技1のクールタイムが0秒以下なら
            if (playerD.Tec01_CoolTime <= 0)
            {
                FeatherFlyingWaza();        //処理の呼び出し
            }
        }

        //技2なら
        if (technicalFlg2)
        {
            //もし技2のクールタイムが0秒以下なら
            if (playerD.Tec02_CoolTime <= 0)
            {
                FeatherFlyingWaza();        //処理の呼び出し
            }
        }

      
    }

    //ツバメ返しの動き
    void SwallowReturn()
    {
        //技1なら
        if (technicalFlg1)
        {
            //もし技1のクールタイムが0秒以下なら
            if (playerD.Tec01_CoolTime <= 0)
            {
                SwallowReturnWaza();        //処理の呼び出し
            }
        }

        //技2なら
        if (technicalFlg2)
        {
            //もし技2のクールタイムが0秒以下なら
            if (playerD.Tec02_CoolTime <= 0)
            {
                SwallowReturnWaza();        //処理の呼び出し
            }
        }
    }

    //ストライク&backの動き
    void StrikeBack()
    {
        //技１なら
        if(technicalFlg1)
        {
            //もし技1のクールタイムが0秒以下なら
            if (playerD.Tec01_CoolTime <= 0)
            {
                StrikeBackWaza();
            }
        }

        //技２なら
        if(technicalFlg2)
        {
            //もし技2のクールタイムが0秒以下なら
            if (playerD.Tec02_CoolTime <= 0)
            {
                StrikeBackWaza();
            }
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
        //移動前の座標と移動先の座標を保存
        //PlayerReturnLocation = Player.transform.position;
        //PlayerLocationGoal = Player.transform.position + StBackLoc;

        //Debug.Log(PlayerLocationGoal.y - this.transform.position.y);

        //Debug.Log("y");

        //Debug.Log("現在の座標" + Player.transform.position);

        ////移動先へ進んだ後、移動前にいた場所にマークを付与する。
        //Instantiate(Mark,           //生成するオブジェクトのプレハブ(Mark)
        //    PlayerReturnLocation,         //初期位置は移動前にいた場所
        //    Quaternion.identity);   //初期回転情報
        //移動（バック）処理
        //IEnumerator Back()
        //{
        //    Vector3 distance = PlayerReturnLocation - player.transform.position;
        //    // Vector3 BackAngleVector = new Vector3(Cos(), distance.y / MoveSpeed, 0);
        //    //終了条件を満たしているか確認
        //    //現在の座標が移動する
        //    //もし現在の座標が移動先未満なら
        //    if (PlayerReturnLocation.y >= Player.transform.position.y)//終了条件
        //    {
        //        playerD.ActionFlg = true;   //技発動中は他の操作を受け付けない（未完成）
        //        NowDistance = 0;            //移動前の座標リセット
        //        Return = 0;                 //座標リセット
        //        yield break;
        //    }

        //    //入力方向に移動
        //    //  transform.position += BackAngleVector * Time.deltaTime;
        //    //移動した距離を更新
        //    Return -= MoveSpeed * Time.deltaTime;
        //    yield return null;

        //}

    }

    //トッシンの動き
    void Rush()
    {
       
        //技１なら
        if (technicalFlg1)
        {
            //もし技1のクールタイムが0秒以下なら
            if (playerD.Tec01_CoolTime <= 0)
            {

                RushWaza();
            }
        }

        //技２なら
        if (technicalFlg2)
        {
            //もし技2のクールタイムが0秒以下なら
            if (playerD.Tec02_CoolTime <= 0)
            {
                RushWaza();
            }
        }
    }

    /******************各技の処理***********************/

    /***************ハネトバシの処理********************/
    void FeatherFlyingWaza()
    {
        //もし羽が3回打っていないなら
        if (wingCount < 3)
        {
            //弾の生成
            //200フレームに1度だけ弾を発射する
            if (Time.frameCount % 200 == 0)
            {
                /*******ハネトバシ生成**********/

                GameObject obj = Instantiate(Attack_obj_wing,        //生成するオブジェクトのプレハブ
                    Wing1.transform.position,       //初期位置
                    Quaternion.identity);           //初期回転情

                //プレイヤーのID,攻撃情報を持っている羽のスクリプトに渡す。
                obj.GetComponentInChildren<Attack_ID_Sc>().InitializeAttackInfo(Attack_ID_Sc.ATTACK.WING, player.PlayerId());



                obj = Instantiate(Attack_obj_wing,        //生成するオブジェクトのプレハブ
                    Wing2.transform.position,       //初期位置
                    Quaternion.identity);           //初期回転情

                //プレイヤーのID,攻撃情報を持っている羽のスクリプトに渡す。
                obj.GetComponentInChildren<Attack_ID_Sc>().InitializeAttackInfo(Attack_ID_Sc.ATTACK.WING, player.PlayerId());


                obj = Instantiate(Attack_obj_wing,        //生成するオブジェクトのプレハブ
                    Wing3.transform.position,       //初期位置
                    Quaternion.identity);           //初期回転情

                //プレイヤーのID,攻撃情報を持っている羽のスクリプトに渡す。
                obj.GetComponentInChildren<Attack_ID_Sc>().InitializeAttackInfo(Attack_ID_Sc.ATTACK.WING, player.PlayerId());

                wingCount++;
            }
            Debug.Log("wingCountは" + wingCount);
        }
        else
        {
            //羽の状態を初期化
            wingCount = 0;
            technicalNumber = 0;

            //もし技1の所にこの技をセットしたなら
            if (technicalFlg1)
            {
                //PlayerのDataにある、空きのクールタイムに
                //技1のクールタイムを入れる。
                playerD.Tec01_CoolTime = playerD.FlyingFeather_CoolTime;
            }
            //もし技2の所にこの技をセットしたなら
            else if (technicalFlg2)
            {
                //PlayerのDataにある、空きのクールタイムに
                //技2のクールタイムを入れる。
                playerD.Tec02_CoolTime = playerD.FlyingFeather_CoolTime;
            }

            Rest1_2();              //技1or2を使った最後にリセットする
        }
    }


    /***************ツバメ返しの技処理*****************/
    void SwallowReturnWaza()
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

            //もし技1の所にこの技をセットしたなら
            if (technicalFlg1)
            {
                //PlayerのDataにある、空きのクールタイムに
                //技1のクールタイムを入れる。
                playerD.Tec01_CoolTime = playerD.SwallowReturn_CoolTime;
            }
            //もし技2の所にこの技をセットしたなら
            else if (technicalFlg2)
            {
                //PlayerのDataにある、空きのクールタイムに
                //技2のクールタイムを入れる。
                playerD.Tec02_CoolTime = playerD.SwallowReturn_CoolTime;
            }

            Rest1_2();              //技1or2を使った最後にリセットする
        }
    }


    /***************ストライク＆バック処理*******************/
    void StrikeBackWaza()
    {
        //当たり判定の情報をストライク＆バックの情報に書き換える
        attack_id_sc.InitializeAttackInfo(Attack_ID_Sc.ATTACK.STRIKE_BACK, player.PlayerId());

        //ここで前進する
        if (player.stBackCount < 1)
        {

            //技発動中行動不可
            inactionableFlg = true;

            //移動後に行動不能を解除する(0.5秒後)
            Invoke("inactionablebreak", 0.5f);

            //前進前の座標を保存
            PlayerReturnLocation = Player.transform.position;

            //もしPlayerが右のフラグをtrueにしたなら右へ進む
            if (player.Right)
            {
                PlayerLocationDistance = new Vector2(move_distance, 0);
                Location();
                Debug.Log("右方向");
            }
            //もし右斜め上なら
            else if (player.Right && player.Up)
            {
                PlayerLocationDistance = new Vector2(move_distance, move_distance);
                Location();
            }
            //もし左斜め下なら
            else if (player.Right && player.Down)
            {
                PlayerLocationDistance = new Vector2(move_distance, -move_distance);
                Location();
            }

            //もしPlayerが左のフラグをtrueにしたなら左へ進む
            if (player.Left)
            {
                PlayerLocationDistance = new Vector2(-move_distance, 0);
                Location();
                Debug.Log("左方向");
            }

            //もしPlayerが上のフラグをtrueにしたなら上へ進む
            if (player.Up)
            {
                PlayerLocationDistance = new Vector2(0, move_distance);
                Location();
            }
            //もしPlayerが下のフラグをtrueにしたなら下へ進む
            else if (player.Down)
            {
                PlayerLocationDistance = new Vector2(0, -move_distance);
                Location();
            }

            Debug.Log("一回目");
            
            //マーク付与
            GameObject mark = Instantiate(Mark,       //生成するオブジェクトのプレハブ(Mark)
                              PlayerReturnLocation,   //初期位置は移動前にいた場所
                              Quaternion.identity);   //初期回転情報
                                                      //技発動中行動不可

            mark.GetComponentInChildren<Attack_ID_Sc>().InitializeAttackInfo
                                                        (Attack_ID_Sc.ATTACK.STRIKE_BACK, player.PlayerId());

            mark.GetComponent<MarkBreak>().SetPlayerScript(player);
            
            //当たり判定出現
            Attack_obj_tubame.SetActive(true);

            Invoke("HitJudgeActiveFalse", 1.0f);

            //前進時、ぬるりと移動を始める。
            player_rb2d.DOMove(PlayerLocation, 1.0f);
          

            player.stBackCount++;   //ストライク&バックの押した回数をカウント

            //ストライクぬるりと移動する処理（呼び出し）
            //StartCoroutine(Move(Vector3.up));
        }

        //もし技のボタンを2回押したら以前記録した場所へ戻る
        if (player.stBackFlg)
        {
            if (!player.StBc_TimeOverFlg)
            {
                //当たり判定出現
                Attack_obj_tubame.SetActive(true);

                Invoke("HitJudgeActiveFalse", 1.0f);

                //座標登録のところへ戻るよう、現在の位置に反映させる
                this.transform.DOMove(PlayerReturnLocation, 1.0f);

                Invoke("inactionablebreak", 1.0f);

                

                //技発動中行動不可
                inactionableFlg = true;
                
            }

            //PlayerのDataにある、空きのクールタイムに
            CoolTime();

            //情報を初期化
            technicalNumber = 0;
            player.stBackCount = 0;
            //player.stBackFlg = false;
            player.StBc_TimeOverFlg = false;
            player.waza1_2 = false;


            Rest1_2();              //技1or2を使った最後にリセットする
        }

        

        //移動する前の計算関数
        void Location()
        {
            //前進後の座標を計算しておく
            PlayerLocation = Player.transform.position + PlayerLocationDistance;
        }

        //StBackのクールタイム処理
        void CoolTime()
        {
            //もし技1の所にこの技をセットしたなら
            if (technicalFlg1)
            {
                //もし二度受付以内にバックしたら通常待ち時間
                if (!player.StBc_TimeOverFlg)
                {
                    //技1のクールタイムを入れる。
                    playerD.Tec01_CoolTime = playerD.StrikeBack_CoolTime;
                }
                //そうでなければ待ち時間を+5秒追加する。s
                else playerD.Tec01_CoolTime = playerD.StrikeBack_CoolTime + 5;
            }
            //もし技2の所にこの技をセットしたなら
            else if (technicalFlg2)
            {
                //もし二度受付以内にバックしたら通常待ち時間
                if (!player.StBc_TimeOverFlg)
                {
                    //PlayerのDataにある、空きのクールタイムに
                    //技2のクールタイムを入れる。
                    playerD.Tec02_CoolTime = playerD.StrikeBack_CoolTime;
                }
                //そうでなければ待ち時間を+5秒追加する。
                else playerD.Tec02_CoolTime = playerD.StrikeBack_CoolTime + 5;
            }
        }
    }

   

    /*****************トッシンの処理*********************/
    void RushWaza()
    {
        /*この関数へ切り替わるとPlayerDataでここの関数番号から技発動を検知し
        * playerDataでスタン処理を行います*/
        /*ここの関数の処理は自分の周囲から最も近いPlayerを検知し移動する処理*/

        //当たり判定の情報を突進の情報に書き換える
        attack_id_sc.InitializeAttackInfo(Attack_ID_Sc.ATTACK.RUSHATTACK, player.PlayerId());

        rush_attack_range.SetActive(true);

        //もし不発なら
        if (player.RushFlg)
        {
            CoolTime();             //クールタイムの処理
                                    //全てを初期化
            rush_attack_range.SetActive(false);
            technicalNumber = 0;
            //player.AttackRush.SetActive(false);
            player.RushFlg = false;
            Rest1_2();
        }

        
       //ターゲットを決める
       DecideTarget();
        

        Debug.Log(rush_target_flg);

        //もしtargetが入ってないのなら
        //if (target == null)
        //    return;

        //もしトッシンを一度も発動していないなら
        if (rush_target_flg)
        {

            //取得したオブジェクトに対してループでおこなう。
            //foreach (GameObject player in objects)
            //{
            //    Debug.Log("取得したプレイヤーは" + player.name);
            //}

                //技発動中時攻撃判定をつける。
                //TecAttack.SetActive(true);
            Attack_obj_tubame.SetActive(true);

            //不発じゃなかった際カウントはそのままなので初期化
            player.num2 = player.time2;

            //もし自分の位置が見つけた相手の所と同じ位置ではないなら
            if (this.transform.position != target_position)
            {
                //範囲に当たった瞬間、オブジェクトの範囲が早めに消えるので時間差を作る。
                Invoke("RushTargetFlgOFF", 1.0f);
                //範囲に居るPlayerを取得しTargetに入れ、
                //(↑この処理はRushRangeJudge)追いかける
                this.transform.DOMove(target_position, 1.0f);
            }
        }
        else
        {

            CoolTime();             //クールタイムの処理

            //全てを初期化
            technicalNumber = 0;
            //時間差でtrueにしているためこっちも時間差でfalseにする
            //player.AttackRush.SetActive(false);
            //TecAttack.SetActive(false);
            Attack_obj_tubame.SetActive(false);
            rush_attack_range.SetActive(false);
            player.RushFlg = false;
            target_flg = false;
            target_position = Vector3.zero;          //targetにしていたPlayerをnullにする
            Rest1_2();              //技1or2を使った最後にリセットする
        }

        //Rushのクールタイム処理
        void CoolTime()
        {
            //もし技1の所にこの技をセットしたなら
            if (technicalFlg1)
            {
                //もし技が不発ではないなら通常待ち時間
                if (!player.RushFlg)
                {
                    //技1のクールタイムを入れる。
                    playerD.Tec01_CoolTime = playerD.Rush_CoolTime;
                }
                //不発なら待ち時間を-5秒に変更する。
                else playerD.Tec01_CoolTime = playerD.Rush_CoolTime - 5;
            }

            //もし技2の所にこの技をセットしたなら
            else if (technicalFlg2)
            {
                //もし技が不発ではないなら通常待ち時間
                if (!player.RushFlg)
                {
                    //PlayerのDataにある、空きのクールタイムに
                    //技2のクールタイムを入れる。
                    playerD.Tec02_CoolTime = playerD.Rush_CoolTime;
                }
                //不発なら待ち時間を-5秒に変更する。
                else playerD.Tec02_CoolTime = playerD.Rush_CoolTime - 5;
            }
        }
    }

    void DecideTarget()
    {
        if (target_flg)
            return;

        //PlayerTagを持っている全ての敵の座標を取得
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");

        //Posi配列をobjectsの配列の数だけ生成する。
        List<Vector3> position_list = new List<Vector3>();

        //objectsの配列数だけiを進める。
        for (int i = 0; i < objects.Length; i++)
        {
            Player another_player = objects[i].GetComponent<Player>();

            if (player.PlayerId() == another_player.PlayerId())
                continue;

            //Posi[i]の配列に座標を保存する。
            Vector2 position = objects[i].GetComponent<Transform>().position;

            if (rushRangeJudge.CircleBuild(this.transform.position, position))
            {
                position_list.Add(position);
                Debug.Log("追加した");
            }

        }

        if (position_list.Count == 1)
        {
            target_position = position_list[0];

            if (!rush_target_flg)
                RushTargetFlgON();

            target_flg = true;
           
            Debug.Log("一人だけ");
        }
        else if (1 < position_list.Count)
        {
            float goal_distance = 5.0f;

            for (int i = 0; i < position_list.Count; i++)
            {
                float distance = Vector2.Distance(this.transform.position, position_list[i]);

                if (goal_distance > distance)
                {
                    goal_distance = distance;
                    target_position = position_list[i];
                }
            }

            if (!rush_target_flg)
                RushTargetFlgON();

            target_flg = true;
            Debug.Log("複数いた");
        }

        //リストを空にする
        position_list.Clear();

    }

    //ストライク後の行動不能を解除する関数
    public void inactionablebreak()
    {
        inactionableFlg = false;
    }

    //トッシンを発動したフラグをONにする関数
    void RushTargetFlgON()
    {
        rush_target_flg = true;     //技を一度出したのでtrueにする。
    }

    //トッシンを発動したフラグをOFFにする関数
    void RushTargetFlgOFF()
    {
         rush_target_flg = false;   //初期化するのでfalseへ
    }

    //技判定を消す処理
    public void HitJudgeActiveFalse()
    {
        Attack_obj_tubame.SetActive(false);
        rush_attack_range.SetActive(false);

    }

    //移動(ストライク処理)※現在は使用していません。
    //IEnumerator Move(Vector3 TmpVector)
    //{
    //    while (true)
    //    {
    //        //終了条件を満たしているか確認
    //        //現在の座標が移動する
    //        //もし現在の座標が移動先未満なら
    //        if (TargetDistance <= NowDistance + (MoveSpeed * Time.deltaTime))//終了条件
    //        {
    //            transform.position += TmpVector * (TargetDistance - NowDistance);
    //            playerD.ActionFlg = true;   //技発動中は他の操作を受け付けない（未完成）
    //            NowDistance = 0;            //移動前の座標リセット
    //            yield break;
    //        }
    //        //入力方向に移動
    //        transform.position += TmpVector * Time.deltaTime * MoveSpeed;

    //        //移動した距離を更新
    //        NowDistance += MoveSpeed * Time.deltaTime;
    //        //Debug.Log("前回からの移動距離" + MoveSpeed * Time.deltaTime);
    //        //Debug.Log("累計移動距離"+NowDistance);
    //        yield return null;
    //    }
    //}
}
