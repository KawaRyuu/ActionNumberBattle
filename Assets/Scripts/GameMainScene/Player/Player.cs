using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;           //リジットボディから参照
    PlayerInput playerInput;    //PlayerのInputSystemから参照
    PlayerData info;            //プレイヤーのデータクラスから参照
    TechnicalData waza;         //技のスクリプトから参照
    EntryAndExitMessages Entry_Exit;

    [SerializeField] public GameObject AttackRush;


    public GameObject Input;    //InputManagerから参照に必要なやつ

    //PlayerのID一覧
    public enum PLAYER_ID
    {
        P1,
        P2,
        P3,
        P4,
    }

    [SerializeField]private PLAYER_ID playerID;

    public int stBackCount = 0;             //ストライク&backの2度押しカウント
    public bool stBackFlg = false;         //ストライク&backの2度押しフラグ
    public bool StBc_TimeOverFlg = false;  //StBackの技2回押さなかった時のフラグ

    public bool waza1_2 = false;           //技1のはずなのにストライクバックで
                                           //技2を押すと反応するためこのフラグをおいておきます。

    const float time = 5.0f;      //バックの入力受付時間(定数化)
    public float time2 = 3.0f;    //トッシンの入力受付時間(定数化)
    public float num = 0;         //数を入れる(ストライクバック)
    public float num2 = 0;        //数を入れる(トッシン)
    public  bool RushFlg = false;  //トッシン不発したかのフラグ
    public bool Right = false;    //右方向に向いた際フラグがONになる
    public bool Left = false;     //左方向に向いた際フラグがONになる。
    public bool Up = false;       //上方向に向いた際フラグがONになる。
    public bool Down = false;     //下方向に向いた際フラグがONになる。

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }


    // 初期化
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        info = GetComponent<PlayerData>();
        waza = GetComponent<TechnicalData>();
        Entry_Exit = Input.GetComponent<EntryAndExitMessages>();
        stBackCount = 0;
        num = time;
        num2 = time2;
        waza1_2 = false;
        RushFlg = false;
        Right = false;
        Left = false;
        Up = false;
        Down = false;
        //IDSorting();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        //InputSystemのactionmapからMoveを取得
        Vector2 input_value = playerInput.actions["Move"].ReadValue<Vector2>();
        //Vector2 right_input_value = playerInput.actions["TechnicalRange"].ReadValue<Vector2>();
        bool input_waza1 = playerInput.actions["Waza"].WasPressedThisFrame();
        bool input_waza2 = playerInput.actions["Waza2"].WasPressedThisFrame();

        //if (right_input_value.x > 0)
        //{
        //    Debug.Log("ポモイト");
        //}

        //Playerの基本の動き
        //もし気絶中なら行動不可
        if (!info.Swoon_Flg)
        {
            //技発動中は行動不可
            if (!waza.inactionableFlg)
            {
                //もしOボタンを押したとき且つクールタイムが0の時のみ(技1)
                if (input_waza1 && info.Tec01_CoolTime <= 0)
                {
                    //先行入力をさせないよう
                    if (waza.technicalFlg2)
                        return;

                    //技枠１をtureにする
                    waza.technicalFlg1 = true;

                    //もしストライクバックなら
                    if (waza.technicalNumber == 3)
                    {
                        //技１のあと技２で同じ処理を通すためこれを置く
                        waza1_2 = true;
                    }

                    //もし技3だった場合
                    if (waza.technicalNumber == 3 && waza1_2)
                    {
                        StrikeBack();//2回押し処理の呼び出し
                    }
                }

                //もしPボタンを押したとき且つクールタイムが0の時のみ(技2)
                if (input_waza2 && info.Tec02_CoolTime <= 0)
                {
                    //先行入力をさせないよう
                    if (waza.technicalFlg1)
                        return;

                    //技枠2をtureにする
                    waza.technicalFlg2 = true;

                    //もし技3だった場合
                    if (waza.technicalNumber == 3 && !waza1_2)
                    {
                        StrikeBack();//2回押し処理の呼び出し
                    }
                }

                /**********向きを変える処理***********/

                //進行方向へ向きを変える
                if (input_value.x < 0)
                {
                    //左方向
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    Left = true;
                    Right = false;
                    Up = false;
                    Down = false;
                }
                else if (input_value.x > 0)
                {
                    //右方向
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    Left = false;
                    Right = true;
                    Up = false;
                    Down = false;
                }

                //もしinput2が0より小さいなら下方向
                if (input_value.y < 0)
                {
                    Debug.Log("下");
                    Down = true;
                    Up = false;
                }////もしinput2が0より大きいなら上方向
                else if (input_value.y > 0)
                {
                    Debug.Log("上");
                    Up = true;
                    Down = false;
                }


                //Playerのポジションに速度を加える
                position.x = input_value.x * info.Speed;
                position.y = input_value.y * info.Speed;

                transform.position += Time.deltaTime * position;
            }
        }
        Debug.Log("今の体力は" + info.Hp);

       
        //もし技3がある且つ、技枠1のフラグがTrueなら
        if (waza.technicalNumber == 3 && waza.technicalFlg1)
            StrikeTimer();

        //もし技3がある且つ、技枠2のフラグがTrueなら
        else if (waza.technicalNumber == 3 && waza.technicalFlg2)
            StrikeTimer();

        //もし技4がある且つ、技枠1のフラグがTrueなら
        if (waza.technicalNumber == 4 && waza.technicalFlg1)
        {
            AttackRush.SetActive(true);
            RushTimer();
        }

        //もし技4がある且つ、技枠2のフラグがTrueなら
        else if (waza.technicalNumber == 4 && waza.technicalFlg2)
        {
            AttackRush.SetActive(true);
            RushTimer();
        }
    }

    //技ストライク&backの技を最大2回分カウントする。
    void StrikeBack()
    {
        //もし技ボタンが3回より小さいならカウントup
        if (stBackCount < 3)
        {
            stBackCount++;
            //ボタンを最大条件に達したら
            if (stBackCount == 3)
            {
                //もし技ボタンを2回押したなら
                Debug.Log("2度通った");
                num = time;
                stBackFlg = true;   //元の位置へ戻るフラグ
            }
        }
    }

    //ストライク&バックのタイマー(二度受付の)
    void StrikeTimer()
    {
        if (!StBc_TimeOverFlg)
        {
            //もし制限時間が0秒以上なら
            if (num >= 0)
            {
                //カウントダウンし続ける
                num -= Time.deltaTime;
            }
            //制限時間を超えたら
            else
            {
                num = time;
                StBc_TimeOverFlg = true;
                stBackFlg = true;
            }
        }
    }

    //トッシンのタイマー(攻撃範囲の表示時間計測)
    void RushTimer()
    {
        //もしフラグがONじゃないなら
        if (!RushFlg)
        {
            //もし制限時間が0秒以上なら
            if (num2 >= 0)
            {
                //カウントダウンし続ける
                num2 -= Time.deltaTime;
            }
            //制限時間を超えたら
            else
            {
                num2 = time2;
                //不発のフラグON
                RushFlg = true;
                //AttackRush.SetActive(false);
            }
        }
    }

    //Playerの識別
    public int GetPlayer()
    {
        return playerInput.user.index;
    }

    //受け渡し
    public PLAYER_ID PlayerId()
    {
        return playerID;
    }

    public void SetPlayerId(PLAYER_ID id)
    {
        playerID = id;
    }


    //Playerの数識別
    void IDSorting()
    {
        //i がPlayerの入った数だけ加算させる。
        for (int i = 0; i < Entry_Exit.PlayerNumberRetun(); i++)
        {
            switch (i)
            {
                case 0:
                    playerID = PLAYER_ID.P1;
                    break;
                case 1:
                    playerID = PLAYER_ID.P2;
                    break;
                case 2:
                    playerID = PLAYER_ID.P3;
                    break;
                case 3:
                    playerID = PLAYER_ID.P4;
                    break;
            }
        }
       
    }

    /********旧操作プログラム**********/
    //if (Input.GetKey("left"))
    //{
    //    position.x -= info.Speed * Time.deltaTime;          //左方向
    //}
    //else if (Input.GetKey("right"))
    //{
    //    position.x += info.Speed * Time.deltaTime;          //右方向
    //}
    //if (Input.GetKey("up"))
    //{
    //    position.y += info.Speed * Time.deltaTime;          //上方向
    //}
    //else if (Input.GetKey("down"))
    //{
    //    position.y -= info.Speed * Time.deltaTime;          //下方向
    //}
}