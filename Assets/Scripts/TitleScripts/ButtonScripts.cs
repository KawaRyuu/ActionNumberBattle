using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    //PlayerのInputSystem
    PlayerInput playerInput;

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool SelectButton = playerInput.actions["SelectButton"].WasPressedThisFrame();

        if (SelectButton)
        {
            ChangeScene();
        }
    }

    //ボタンを押したら実行する関数　実行するためにはボタンへ関数登録が必要
    public void ChangeScene()
    {
        //タイトルシーンからコントローラー設定のシーンへ
        SceneManager.LoadScene("ControllerScene");
    }
}
