using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Button : MonoBehaviour
{
    //PlayerのInputSystem
    PlayerInput playerInput;
    ControllerConnection connection;

    [SerializeField] GameObject CPUPanel;
    [SerializeField] GameObject PlayerPanel;

    bool push_flg = false;

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }


    // Start is called before the first frame update
    void Start()
    {
        connection = GetComponent<ControllerConnection>();
        push_flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool SelectButton = playerInput.actions["SelectButton"].WasPressedThisFrame();
        bool BackButton = playerInput.actions["BackButton"].WasPressedThisFrame();

        //もしコントローラーのBボタンを押したら
        if(SelectButton)
        {
            if (!push_flg)
            {
                push_flg = true;
                //もし接続が全てPlayerなら
                if (connection.GetControllerNumber() == 4)
                    PlayerPanel.SetActive(true);
                else
                    //CPU難易度パネルの表示をONにする。
                    CPUPanel.SetActive(true);
            }
            else
            {
                GotoScene();
                push_flg = false;
            }
        }

        //もしコントローラーのAボタンを押したら
        if(BackButton)
        {
            push_flg = false;
            PlayerPanel.SetActive(false);
            CPUPanel.SetActive(false);
        }
    }

    //シーン行く
    void GotoScene()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}
