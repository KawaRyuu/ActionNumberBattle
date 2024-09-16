using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    //PlayerのInputSystem
    PlayerInput playerInput;
    ControllerConnection connection;
    [SerializeField] GameObject CPUPanel;

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }


    // Start is called before the first frame update
    void Start()
    {
        connection = GetComponent<ControllerConnection>();
    }

    // Update is called once per frame
    void Update()
    {
        bool SelectButton = playerInput.actions["SelectButton"].WasPressedThisFrame();

        //もしコントローラーのBボタンを押したら
        if(SelectButton)
        {
            //もし接続が全てPlayerなら
            if (connection.Controller() == 2)
            {
                //一秒後に技選択シーンへ移行する。
                Invoke("GotoSelectScene", 1.0f);
            }
            else
            {
                //CPU難易度パネルの表示をONにする。
                CPUPanel.SetActive(true);
            }
        }
    }

    //選択シーンへ移行する関数
    void GotoSelectScene()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}
