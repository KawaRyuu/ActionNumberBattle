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

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }
    // Start is called before the first frame update
    void Start()
    {
        SentakuWaku1.SetActive(false);
        SentakuWaku2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //コントローラの操作[B](決定ボタン)
        bool SelectButton = playerInput.actions["SelectButton"].WasPressedThisFrame();

        //InputSystemのactionmapからMoveを取得
        Vector2 MoveLeftButton = playerInput.actions["Move"].ReadValue<Vector2>();

        //特定の操作を確認したら
        if (SelectButton)
        {
            //ChangeScene();
            //ボタンを押すとき特定の動きができるようにする。
        }

        //もしYが0より小さい場合
        if (MoveLeftButton.y < 0)
        {
            //スタートボタンに枠を表示
            SentakuWaku1.SetActive(true);
            SentakuWaku2.SetActive(false);
        }
        //もしYが0より大きい場合
        else if (MoveLeftButton.y > 0)
        {
            //歯車に選択枠を表示
            SentakuWaku2.SetActive(true);
            SentakuWaku1.SetActive(false);
        }
    }

    //ボタンを押したら実行する関数　実行するためにはボタンへ関数登録が必要
    public void ChangeScene()
    {
        //タイトルシーンからコントローラー設定のシーンへ
        SceneManager.LoadScene("ControllerScene");
    }
}
