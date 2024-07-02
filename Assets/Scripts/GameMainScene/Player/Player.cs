using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //PlayerのInputSystem
    PlayerInput playerInput;

    //プレイヤーのデータクラスから参照
    PlayerData info;
    TechnicalData waza;


    public int stBackCount = 0;             //ストライク&backの2度押しカウント
    public bool stBackFlg = false;         //ストライク&backの2度押しフラグ
    public bool StBc_TimeOverFlg = false;   //StBackの技2回押さなかった時のフラグ

    const float time = 5.0f;      //バックの入力受付時間(定数化)
    public float num = 0;        //数を入れる

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
                    //技枠１をtureにする
                    waza.technicalFlg1 = true;

                    //もし技3だった場合
                    if (waza.technicalNumber == 3)
                        StrikeBack();//2回押し処理の呼び出し
                }

                //もしPボタンを押したとき且つクールタイムが0の時のみ(技2)
                if (Input.GetKeyDown(KeyCode.P) && info.Tec02_CoolTime <= 0)
                {
                    //技枠2をtureにする
                    waza.technicalFlg2 = true;

                    //もし技3だった場合
                    if (waza.technicalNumber == 3)
                        StrikeBack();//2回押し処理の呼び出し
                }

                Debug.Log("操作できる");

                //Playerのポジションに速度を加える
                position.x = input_value.x * info.Speed;
                position.y = input_value.y * info.Speed;

                //方向左へ向かせる
                if (position.x < 0)
                {

                }

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

                transform.position += Time.deltaTime * position;
            }
        }
        Debug.Log("今の体力は" + info.Hp);

        //トッシン（仮）
        if (Input.GetKeyDown(KeyCode.T))
        {
            waza.technicalNumber = 2;
        }

        //もし技3がある且つ、技枠1のフラグがTrueなら
        if (waza.technicalNumber == 3 && waza.technicalFlg1)
            Timer();

        //もし技3がある且つ、技枠2のフラグがTrueなら
        else if (waza.technicalNumber == 3 && waza.technicalFlg2)
            Timer();
    }

    //技ストライク&backの技を最大2回分カウントする。
    void StrikeBack()
    {
        //もし技ボタンが3回より小さいならカウントup
        if (stBackCount < 3)
        {
            stBackCount++;
            Debug.Log("ストライク&backカウント" + stBackCount);

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
    void Timer()
    {
        Debug.Log("技二度受付可能残り" + num);
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

    //Playerの識別
    public int GetPlayer()
    {
        return playerInput.user.index;
    }
}
