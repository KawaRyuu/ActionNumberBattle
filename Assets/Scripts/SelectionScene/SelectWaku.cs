using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectWaku : MonoBehaviour
{
    PlayerInput playerInput;


    public GameObject Waku1;
    public GameObject Waku2;
    public GameObject Waku3;
    public GameObject Waku4;

    int XmoveCount = 0;
    bool XMove_flg = false;

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }


    // Start is called before the first frame update
    void Start()
    {
        Waku1.SetActive(false);
        Waku2.SetActive(false);
        Waku3.SetActive(false);
        Waku4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //InputSystemのactionmapからMoveを取得
        Vector2 MoveLeftButton = playerInput.actions["Move"].ReadValue<Vector2>();

        //枠の横スライド(X座標)
        if (XmoveCount <= 3 && !XMove_flg)
        {
            if (MoveLeftButton.x > 0)
            {
                XmoveCount++;
                XMove_flg = true;
            }
            else if (MoveLeftButton.x < 0)
            {
                XmoveCount--;
                XMove_flg = true;
            }
            if (XmoveCount >= 3)
                XmoveCount = 3;
            else if (XmoveCount <= 0)
                XmoveCount = 0;
        }
        else XMove_flg = false;

        //数に応じて位置が変わる。
        switch (XmoveCount)
        {
            case 0:
                Waku1.SetActive(true);
                Waku2.SetActive(false);
                Waku3.SetActive(false);
                Waku4.SetActive(false);
                break;
            case 1:
                Waku2.SetActive(true);
                Waku1.SetActive(false);
                Waku3.SetActive(false);
                Waku4.SetActive(false);
                break;
            case 2:
                Waku3.SetActive(true);
                Waku1.SetActive(false);
                Waku2.SetActive(false);
                Waku4.SetActive(false);
                break;
            case 3:
                Waku4.SetActive(true);
                Waku1.SetActive(false);
                Waku2.SetActive(false);
                Waku3.SetActive(false);
                break;
        }
    }
}
