using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public Text   TextCountDown;                  //UIの表示
           int    CountDownTime           = 4;    //分
           float  CountDownSecond         = 60f;  //秒
           bool   CountUpFlg              = false;


    // Start is called before the first frame update
    void Start()
    {
        //初期化
        CountDownTime = 4 - 1;      //初めに引く
        CountDownSecond = 60f;
        CountUpFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        //もしCountUpFlgがfalseなら
        if (!CountUpFlg)
        {
            //もし"分"が0分以上なら
            if (CountDownTime > 0)
            {
                //カウントダウン
                CountDownSecond -= Time.deltaTime;

                //もし秒数が0なら

                if (CountDownSecond <= 0)
                {
                    CountDownTime--;
                    CountDownSecond = 60f;      //60秒追加
                }

                //文字表示
                TextCountDown.text = "残り" + CountDownTime + "分"
                  + (int)CountDownSecond + "秒";
            }
            else
            {
                //もし残り秒数が0より大きいなら
                if(CountDownSecond >0)
                {
                    //カウントダウン
                    CountDownSecond -= Time.deltaTime;
                }
                else
                {
                    CountDownSecond = 0.0f;
                    CountUpFlg = true;
                    Invoke("SceneChange", 1.5f);
                }
                //文字表示
                TextCountDown.text = "残り" + (int)CountDownSecond + "秒";
            }
        }
    }

    //シーン切り替え
    void SceneChange()
    {
        SceneManager.LoadScene("Result");
    }

    public int GetCountDownTime()
    {
        return CountDownTime;
    }
}
