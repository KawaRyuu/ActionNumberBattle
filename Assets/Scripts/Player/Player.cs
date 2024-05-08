using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //プレイヤーの操作
    //private float playerSpeed = 5.0f;

    //プレイヤーのデータクラスから参照
    PlayerData info;
    TechnicalData waza;

    public int stBackCount = 0;     //ストライク&backの2度押しカウント
    public bool stBackFlg  = false; //ストライク&backの2度押しフラグ

    // 初期化
    void Start()
    {
        info = GetComponent<PlayerData>();
        waza = GetComponent<TechnicalData>();
        stBackCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        //Playerの基本の動き
        //もし気絶中なら行動不可
        if (!info.Swoon_Flg)
        {
            //技発動中は行動不可
            if (!waza.inactionableFlg)
            {
                if (Input.GetKey("left"))
                {
                    position.x -= info.Speed * Time.deltaTime;          //左方向
                }
                else if (Input.GetKey("right"))
                {
                    position.x += info.Speed * Time.deltaTime;          //右方向
                }
                else if (Input.GetKey("up"))
                {
                    position.y += info.Speed * Time.deltaTime;          //上方向
                }
                else if (Input.GetKey("down"))
                {
                    position.y -= info.Speed * Time.deltaTime;          //下方向
                }
                transform.position = position;
            }
        }
        //Debug.Log(("今のスピードは") + info.Speed);
        Debug.Log("今の体力は" + info.Hp);
        //Debug.Log("無敵フラグは:" + info.Invincibility_Flg);


        //技ストライク&backの技を最大2回分カウントする。
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("ストライク&back");
            //もし技ボタンが3回より小さいならカウントup
            if (stBackCount < 3)
            {
                stBackCount++;
                waza.technicalNumber = 3;
                //もし技ボタンを2回押したなら
                if (stBackCount == 2)
                {
                    Debug.Log("2度通った");
                    stBackFlg = true;   //元の位置へ戻るフラグ
                    //カウントを初期化し、
                    //技のクールタイムをはさむ
                    stBackCount = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            waza.technicalNumber = 2;
        }

        //もしｐボタンを押したとき且つクールタイムが0の時のみ
        if (Input.GetKeyDown(KeyCode.P) && info.Tec0_CoolTime <= 0)
        {
            waza.technicalNumber = 1;
        }
    }
}
