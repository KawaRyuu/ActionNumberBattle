using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBreak : MonoBehaviour
{
    //Playerスクリプトを参照する
    Player player;

    TechnicalData tec;

    //5秒後に削除をする設定
    float deleteTime = 5.0f;
    float time = 0.0f;

    //破壊フラグ
    bool destroyFlg = false;
    //時間停止フラグ
    bool TimeStopFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        tec = GameObject.Find("Player").GetComponent<TechnicalData>()
 ;       destroyFlg = false;
        TimeStopFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        //バックフラグがtrueなら
        if (player.stBackFlg)
        {
            destroyFlg = true;
            //TimerStop();
        }
        else Timer();
    }

    //時間をカウントダウンする
    void Timer()
    {
        if (!TimeStopFlg)
        {
            time += Time.deltaTime;
            Debug.Log("いのいの" + time);
        }
            

        //5秒経過後破壊する
        if (deleteTime <= time)
        {
            Destroy(gameObject);
        }
    }

    //時間を止める関数
    void TimerStop()
    {
        TimeStopFlg = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //もしマークとPlayerが触れたなら
        if(collision.gameObject.tag=="Player")
        {
            //破壊フラグを取得済みなら
            if (destroyFlg)
            {
                Debug.Log("破壊");
                //マーク破壊
                Destroy(gameObject);
            }
        }
    }
}
