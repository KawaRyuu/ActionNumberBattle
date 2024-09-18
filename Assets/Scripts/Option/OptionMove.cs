using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class OptionMove : MonoBehaviour
{
    //PlayerのInputSystem
    PlayerInput playerInput;

    //枠
    public GameObject OptionWaku;

    bool Sentaku_flg1 = false;

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }

    // Start is called before the first frame update
    void Start()
    {
        Sentaku_flg1 = false;
        OptionWaku.SetActive(false);
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
            ChangeScene_Title();
            Sentaku_flg1 = false;
        }

        //もしYが0より小さい場合
        if (MoveLeftButton.y < 0)
        {
            Sentaku_flg1 = true;
            OptionWaku.SetActive(true);
        }

    }

    void ChangeScene_Title()
    {
        OptionWaku.SetActive(false);
        Sentaku_flg1 = false;
        SceneManager.LoadScene("TitleScene");
    }
}
