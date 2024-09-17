using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBreak : MonoBehaviour
{
    //Playerスクリプトを参照する
    Player player;
    Attack_ID_Sc attack_id_sc;
    TechnicalData tec;

    //5秒後に削除をする設定
    const float deleteTime = 5.0f;
    float time = 0.0f;

    //破壊フラグ
    bool destroyFlg = false;
    //時間停止フラグ
    bool TimeStopFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        attack_id_sc = GetComponentInChildren<Attack_ID_Sc>();

        destroyFlg = false;
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


        Timer();
    }

    public void SetPlayerScript(Player parent_player)
    {
        player = parent_player;
    }

    //時間をカウントダウンする
    void Timer()
    {
        if (!TimeStopFlg)
        {
            time += Time.deltaTime;
            //Debug.Log("いのいの" + time);
        }
            

        //5秒経過後破壊する
        if (deleteTime <= time)
        {
            player.stBackFlg = false;
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
            Player hit_player = collision.gameObject.GetComponent<Player>();

            if (hit_player.PlayerId() != attack_id_sc.PlayerID_Return())
                return;

            //破壊フラグを取得済みなら
            if (destroyFlg)
            {
                Debug.Log("破壊");

                player.stBackFlg = false;

                //マーク破壊
                Destroy(gameObject);
            }
        }
    }
}
