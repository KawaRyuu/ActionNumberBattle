using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerWakuMove : MonoBehaviour
{
    //PlayerのInputSystem
    PlayerInput playerInput;

    public GameObject playerWaku;

    bool Waku_flg = false;

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerWaku.SetActive(false);
        Waku_flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        //コントローラの操作[B](決定ボタン)
        bool SelectButton = playerInput.actions["SelectButton"].WasPressedThisFrame();
        //InputSystemのactionmapからMoveを取得
        Vector2 MoveLeftButton = playerInput.actions["Move"].ReadValue<Vector2>();

        if (MoveLeftButton.y > 0 || MoveLeftButton.y < 0)
        {
            Waku_flg = true;
            playerWaku.SetActive(true);
        }

        if (SelectButton && Waku_flg)
        {
            Waku_flg = false;
            SceneChange();
        }
    }

    //技選択画面に移行
    void SceneChange()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}
