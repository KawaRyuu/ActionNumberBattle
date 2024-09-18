using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerConnection : MonoBehaviour
{
    int connection = 0;
    // Start is called before the first frame update
    void Start()
    {
        connection = 0;

        //接続されているコントローラーの名前を調べる。
        var controllerNames = Input.GetJoystickNames();

        //4人分いるかどうか
        for (int i = 0; i < controllerNames.Length; i++)
        {
            //Debug.Log(controllerNames[i]);
            //もしコントローラーがなかったらエラーで返す
            if (controllerNames[i] == "") Debug.Log("エラー");
            else
            {
                connection++;
                Debug.Log("現在の接続状況は" + connection);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    //コントローラー接続の数を返す
    public int GetControllerNumber()
    {
        return connection;
    }
}
