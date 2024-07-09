using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    //PlayerのInputSystem
    PlayerInput playerInput;

    //プレイヤーのデータクラスから参照
    PlayerData info;
    TechnicalData waza;

    [SerializeField] public GameObject AttackRush;

    public int stBackCount = 0;             //ストライク&backの2度押しカウント
    public bool stBackFlg = false;         //ストライク&backの2度押しフラグ
    public bool StBc_TimeOverFlg = false;  //StBackの技2回押さなかった時のフラグ

    public bool waza1_2 = false;           //技1のはずなのにストライクバックで
                                           //技2を押すと反応するためこのフラグをおいておきます。

    const float time = 5.0f;      //バックの入力受付時間(定数化)
    public float time2 = 3.0f;     //トッシンの入力受付時間(定数化)
    public float num = 0;         //数を入れる(ストライクバック)
    public float num2 = 0;        //数を入れる(トッシン)
    public bool RushFlg = false;     //トッシン不発したかのフラグ
    private void Awake()
    {
        TryGetComponent(out playerInput);
    }


    // 初期化
    void Start()
    {
        info = GetComponent<PlayerData>();
        waza = GetComponent<TechnicalData>();
        stBackCount = 0;
        num = time;
        num2 = time2;
        waza1_2 = false;
        RushFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        //InputSystemのactionmapからMoveを取得
        var input_value = playerInput.actions["Move"].ReadValue<Vector2>();

        //Playerの基本の動き
        //もし気絶中なら行動不可
        if (!info.Swoon_Flg)
        {
            //技発動中は行動不可
            if (!waza.inactionableFlg)
            {
                //もしOボタンを押したとき且つクールタイムが0の時のみ(技1)
                if (Input.GetKeyDown(KeyCode.O) && info.Tec01_CoolTime <= 0)
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
                if (Input.GetKeyDown(KeyCode.P) && info.Tec02_CoolTime <= 0)
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

                //Playerのポジションに速度を加える
                position.x = input_value.x * info.Speed;
                position.y = input_value.y * info.Speed;

                //方向左へ向かせる
                if (position.x < 0)
                {

                }

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