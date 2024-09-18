using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class OptionMove : MonoBehaviour
{
    //PlayerのInputSystem
    PlayerInput playerInput;

    //枠
    public GameObject OptionWaku;
    public GameObject SE_Waku;
    public GameObject LightWaku;

    private float TimeRest = 0.1f;
    private float time;
    private int count;

    //ハンドル2個分
    public Slider SE_Handle; 
    public Slider Light_Handle;

    bool Rock_flg = false;
    bool Sentaku_Title_flg = false;  //タイトルのところ
    bool Sentaku_Light_flg = false;  //明るさのバー
    bool Sentaku_SE_flg = false;  //音量

    public Text LightNum;

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }

    // Start is called before the first frame update
    void Start()
    {
        Rock_flg = false;
        Sentaku_Title_flg = false;
        Sentaku_Light_flg = false;
        Sentaku_SE_flg = false;

        OptionWaku.SetActive(false);
        SE_Waku.SetActive(false);
        LightWaku.SetActive(false);

        //標準音量と明るさ
        SE_Handle.value = 50;
        Light_Handle.value = 50;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //コントローラの操作[B](決定ボタン)
        bool SelectButton = playerInput.actions["SelectButton"].WasPressedThisFrame();

        //InputSystemのactionmapからMoveを取得
        Vector2 MoveLeftButton = playerInput.actions["Move"].ReadValue<Vector2>();


        //特定の操作を確認したら
        //ボタンを押すとき特定の動きができるようにする。
        if (SelectButton && Sentaku_Title_flg)
        {
            /*もしカーソルがゲームスタートにいて
             決定ボタンを押したら*/
            ChangeScene_Title();
            Sentaku_Title_flg = false;
        }
        else if (SelectButton || Sentaku_Light_flg)
        {
            Rock();
            BarControll();
        }
        else if (SelectButton || Sentaku_SE_flg)
        {
            Sentaku_SE_flg = false;
            Rock();
            BarControll();
        }

        //もしYが0より小さい場合
        if (MoveLeftButton.y < 0)
        {
            //タイトル画面に戻るボタン
            Sentaku_Title_flg = true;
            Sentaku_Light_flg = false;
            Sentaku_SE_flg = false;
            OptionWaku.SetActive(true);
            SE_Waku.SetActive(false);
            LightWaku.SetActive(false);
        }

        //明るさバーの位置
        if (MoveLeftButton.y > 0 && MoveLeftButton.y < 200)
        {
            Sentaku_Title_flg = false;
            Sentaku_Light_flg = true;
            Sentaku_SE_flg = false;
            SE_Waku.SetActive(false);
            LightWaku.SetActive(true);
            OptionWaku.SetActive(false);
        }


        //音量バーの位置
        if (MoveLeftButton.y > 200)
        {
            Sentaku_Title_flg = false;
            Sentaku_Light_flg = false;
            Sentaku_SE_flg = true;
            OptionWaku.SetActive(false);
            SE_Waku.SetActive(true);
            LightWaku.SetActive(false);
        }

        LightNum.text = ("現在の音量" + Light_Handle.value);
    }

    void ChangeScene_Title()
    {
        OptionWaku.SetActive(false);
        Sentaku_Title_flg = false;
        SceneManager.LoadScene("TitleScene");
    }

    //バーの位置を動かす
    void BarControll()
    {
        //InputSystemのactionmapからMoveを取得
        Vector2 MoveLeftButton = playerInput.actions["Move"].ReadValue<Vector2>();
        //コントローラの操作[A](戻るボタン)
        bool BackButton = playerInput.actions["BackButton"].WasPressedThisFrame();

        if (Sentaku_Light_flg)
        {
            Sentaku_Light_flg = false;
            LightWaku.SetActive(false);

            if (time > TimeRest)
            {
                if (MoveLeftButton.x >= 0 || MoveLeftButton.x >= 50 && count > 10)
                {
                    count++;
                    time = 0;
                }
                else if (MoveLeftButton.x < 50 || MoveLeftButton.x >= 100 && count > 0)
                {
                    count--;
                    time = 0;
                }
            }
            Light_Handle.value = count;

            if (BackButton)
            {
                LightWaku.SetActive(true);
                Rock_flg = false;
            }
        }

        if (Sentaku_SE_flg)
        {
            if (MoveLeftButton.x > 0 && MoveLeftButton.x >= 50)
                SE_Handle.value--;
            else if (MoveLeftButton.x < 50 && MoveLeftButton.x >= 100)
                SE_Handle.value++;
        }
    }

    void Rock()
    {
        Rock_flg = true;
    }
}
