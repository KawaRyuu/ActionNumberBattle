using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    //PlayerのInputSystem
    PlayerInput playerInput;

    public GameObject SentakuWaku1;
    public GameObject SentakuWaku2;

    bool Sentaku_flg1 = false;
    bool Sentaku_flg2 = false;

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }
    // Start is called before the first frame update
    void Start()
    {
        SentakuWaku1.SetActive(false);
        SentakuWaku2.SetActive(false);
        Sentaku_flg1 = false;
        Sentaku_flg2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        //コントローラの操作[B](決定ボタン)
        bool SelectButton = playerInput.actions["SelectButton"].WasPressedThisFrame();

        //InputSystemのactionmapからMoveを取得
        Vector2 MoveLeftButton = playerInput.actions["Move"].ReadValue<Vector2>();

        //特定の操作を確認したら
        //ボタンを押すとき特定の動きができるようにする。
        if (SelectButton && Sentaku_flg1)
        {
            /*もしカーソルがゲームスタートにいて
             決定ボタンを押したら*/
            ChangeScene_Controller();
        }
        else if (SelectButton && Sentaku_flg2)
        {
            /*もしカーソルが設定にいて
             決定ボタンを押したら*/
            ChangeScene_SettingScene();
        }

        //もしYが0より小さい場合
        if (MoveLeftButton.y < 0)
        {
            //スタートボタンに枠を表示
            SentakuWaku1.SetActive(true);
            SentakuWaku2.SetActive(false);
            Sentaku_flg1 = true;
            Sentaku_flg2 = false;
        }
        //もしYが0より大きい場合
        else if (MoveLeftButton.y > 0)
        {
            //歯車に選択枠を表示
            SentakuWaku2.SetActive(true);
            SentakuWaku1.SetActive(false);
            Sentaku_flg1 = false;
            Sentaku_flg2 = true;
        }
    }

    //コントローラシーンへ移行する関数
    public void ChangeScene_Controller()
    {
        //タイトルシーンからコントローラー設定のシーンへ
        SceneManager.LoadScene("ControllerScene");
    }

    //設定画面のシーンへ移行する関数
    public void ChangeScene_SettingScene()
    {
        //タイトルシーンからコントローラー設定のシーンへ
        //SceneManager.LoadScene("ControllerScene");
        Debug.Log("設定画面へ");
    }
}
