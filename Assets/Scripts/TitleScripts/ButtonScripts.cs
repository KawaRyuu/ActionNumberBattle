using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタンを押したら実行する関数　実行するためにはボタンへ関数登録が必要
    public void ChangeScene(string ControllerScene)
    {
        //タイトルシーンからコントローラー設定のシーンへ
        SceneManager.LoadScene("ControllerScene");
    }
}
