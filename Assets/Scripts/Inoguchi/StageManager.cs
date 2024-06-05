using UnityEngine;
using GimmickInfomation;

public class StageManager : MonoBehaviour
{
    //ステージの時間帯の種類
    public enum STAGE_TIME_ZONE
    {
        MORNING,        //朝
        EVENING,        //夕
        NIGHT,          //夜
        EMPTY,          //空の状態
    }

    STAGE_TIME_ZONE stage_time_zone   = STAGE_TIME_ZONE.EMPTY;   //現在の時間帯
    GIMMICK_ID      create_gimmick_id = GIMMICK_ID.EMPTY;

    //Unity上で設定
    [SerializeField] GameObject[] gimmicks = new GameObject[gimmick_num];  //生成されるギミックの配列
    [SerializeField] GameTimer game_timer;                       //残り時間のスクリプト


    Vector3 create_gimmick_position = Vector3.zero;     //生成ギミックの位置座標
    float   create_gimmick_time = 10.0f;                 //ギミック生成のタイマー
    float   create_gimmick_timer = 0.0f;                //ギミック生成時間
    bool    gimmick_decide_flag = false;                //ギミックが決まっているか判定

    const int gimmick_num = (int)GIMMICK_ID.EMPTY;

    private void Start()
    {
        InitializeStageManager();
    }

    //初期化
    void InitializeStageManager()
    {
        stage_time_zone = STAGE_TIME_ZONE.MORNING;
        gimmick_decide_flag = false;
    }

    private void Update()
    {
        StageGimmick();
        CheckStageTimeZone();
    }

    /*ギミックの処理*/

    //ステージギミック生成までの工程をまとめた関数
    void StageGimmick()
    {
        //生成するギミックの種類が決まっていない時は返す
        if (!gimmick_decide_flag)
            return;

        //タイマー増加
        create_gimmick_timer += Time.deltaTime;

        //生成時間経過するまで通さない
        if (create_gimmick_timer < create_gimmick_time)
            return;

        //タイマー初期化
        create_gimmick_timer = 0;

        //生成する座標を決める
        DecideGimmickPosition();

        //ギミック生成
        Instantiate(gimmicks[(int)create_gimmick_id], create_gimmick_position, Quaternion.identity);

        //次生成するギミックが決まってないのでfalse
        gimmick_decide_flag = false;
    }


    //生成するギミックの座標を決める
    void DecideGimmickPosition()
    {
        Vector3 position = Vector3.zero;
        float x = 0;
        float y = 0;


        if (create_gimmick_id == GIMMICK_ID.STAR)
        {
            //これから作るギミックが星

            //画面外の右上に生成する

            y = 1.1f;
            x = 1.1f;
        }
        else
        {
            //これから作るギミックが星以外

            //ギミックを画面外の上下左右どこに生成するか決める
            int num = Random.Range(0, 4);

            //0は上、1は右、2は下、3は左
            switch (num)
            {
                case 0:
                    y = 1.1f;
                    x = Random.value;
                    break;

                case 1:
                    y = Random.value;
                    x = 1.1f;
                    break;

                case 2:
                    y = -0.1f;
                    x = Random.value;
                    break;

                case 3:
                    y = Random.value;
                    x = -0.1f;
                    break;
            }

           

           
        }

        //x,yで表した座標を作る
        position = new Vector3(x, y, Camera.main.nearClipPlane);

        //作った座標を、カメラの枠に合わせた形にする
        create_gimmick_position = Camera.main.ViewportToWorldPoint(position);
    }


  
    /*時間帯の処理*/

    //ステージの時間帯の確認
    void CheckStageTimeZone()
    {
        switch (stage_time_zone)
        {
            case STAGE_TIME_ZONE.MORNING:
                TimeZoneMorning();
                break;

            case STAGE_TIME_ZONE.EVENING:
                TimeZoneEvening();
                break;

            case STAGE_TIME_ZONE.NIGHT:
                TimeZoneNight();
                break;
        }
    }

    //朝の時
    void TimeZoneMorning()
    {
        //残り3分になったら夕方に変更
        if (game_timer.GetCountDownTime() < 3)
            ChangeStageTimeZone(STAGE_TIME_ZONE.EVENING);

        //ギミックが決定している時は返す
        if (gimmick_decide_flag)
            return;

        create_gimmick_id = GIMMICK_ID.KITE;

        gimmick_decide_flag = true;
    }

    //夕方の時
    void TimeZoneEvening()
    {
        //残り2分になったら夜に変更
        if (game_timer.GetCountDownTime() < 2)
            ChangeStageTimeZone(STAGE_TIME_ZONE.NIGHT);

        //ギミックが決定している時は返す
        if (gimmick_decide_flag)
            return;

        create_gimmick_id = GIMMICK_ID.BIRD;

        gimmick_decide_flag = true;

    }

    //夜の時
    void TimeZoneNight()
    {
        //ギミックが決定している時は返す
        if (gimmick_decide_flag)
            return;

        create_gimmick_id = GIMMICK_ID.STAR;

        gimmick_decide_flag = true;

    }


    //ステージの時間帯変更
    void ChangeStageTimeZone(STAGE_TIME_ZONE change_time_zone)
    {
        stage_time_zone = change_time_zone;

    }

    //ステージの時間帯取得
    public STAGE_TIME_ZONE GetStageTimeZone()
    {
        return stage_time_zone;
    }



   

    /*その他の処理*/

}
