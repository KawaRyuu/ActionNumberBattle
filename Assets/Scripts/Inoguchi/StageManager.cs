using UnityEngine;
using GimmickInfomation;
using Unity.Collections.LowLevel.Unsafe;

//ステージ上の管理

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

    //列挙型変数
    STAGE_TIME_ZONE stage_time_zone    = STAGE_TIME_ZONE.EMPTY;                   //現在の時間帯
    GIMMICK_ID[]    time_zone_gimmicks = new GIMMICK_ID[create_gimmick_num];      //時間帯で生成するギミックの識別子
    GIMMICK_ID      create_gimmick_id  = GIMMICK_ID.EMPTY;                        //次に生成するギミックの識別子

    //Unity上で設定
    [SerializeField] GameObject[] gimmicks        = new GameObject[gimmick_num];  //生成されるギミックの配列
    [SerializeField] GameTimer    game_timer;                                     //残り時間のスクリプト
    [SerializeField] int[]        gimmick_weights = new int[create_gimmick_num];  //生成されるギミックの確率の重み

    //各種定数
    const float   create_gimmick_time        = 5.0f;                              //ギミック生成のタイマー
    const int     gimmick_num                = (int)GIMMICK_ID.EMPTY;           　//ギミックの種類数
    const int     create_gimmick_num         = 4;                                //時間帯ごとに生成されるギミックの種類数

    //各種変数
    GameObject[] players;
    NumberData[] player_number_datas = new NumberData[4];

    Vector3       create_gimmick_position    = Vector3.zero;                    　//生成ギミックの位置座標
    float         create_gimmick_timer       = 0.0f;                            　//ギミック生成時間
    bool          gimmick_decide_flag        = false;                           　//ギミックが決まっているか判定
    bool          gimmick_set_flag           = false;                           　//時間帯ごとのギミックの設定ができているか判定
    bool          meteor_shower_flag         = false;                             //流星群が発動するか判定
    int           total_weight               = 0;                               　//ギミックの重みの総計

    //ScritableObjectを使用した変数
    [SerializeField] ResultScoreScriptableObject result_score;

    private void Start()
    {
        InitializeStageManager();
    }

    //初期化
    void InitializeStageManager()
    {
        stage_time_zone     = STAGE_TIME_ZONE.MORNING;
        gimmick_decide_flag = false;
        gimmick_set_flag    = false;


        for(int i = 0; i < 4; i++)
        {
            player_number_datas[i] = result_score.players_array[i].GetComponent<NumberData>();
        }

        SetTimeZoneGimmicks(GIMMICK_ID.EMPTY,GIMMICK_ID.EMPTY,GIMMICK_ID.EMPTY,GIMMICK_ID.EMPTY);
        SetGimmickWeights(0, 0, 0, 0);
    }

    private void Update()
    {
        StageGimmick();
        CheckStageTimeZone();
        SetPlayerData();

        Debug.Log(stage_time_zone);
        Debug.Log(create_gimmick_id);
    }

    /*ギミックの処理*/

    //ステージギミック生成までの工程をまとめた関数
    void StageGimmick()
    {
        /*
          生成するギミックの種類が決まっていない時、
          ステージ上に前回出したギミックが残っている時は返す
        */
        if (!gimmick_decide_flag || this.gameObject.transform.childCount > 0)
            return;

        //時間帯が切り替わる15秒前の時は返す
        if(game_timer.GetCountDownSecond() <= 15.0f)
        {
            gimmick_decide_flag  = false;
            create_gimmick_timer = 0;
            return;
        }


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
        CreateGimmick();

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

            y = 1.1f;
            x = Random.Range(0.5f,1.2f);

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
        position = new Vector3(x, y, 0);

        //作った座標を、カメラの枠に合わせた形にする
        create_gimmick_position = Camera.main.ViewportToWorldPoint(position);

        create_gimmick_position.z = 0;
    }

    //ギミックの生成
    void CreateGimmick()
    {

        //生成するギミックの種類に応じて生成の仕方を変える
        switch (create_gimmick_id)
        {
            case GIMMICK_ID.EMPTY: 
                break;

            case GIMMICK_ID.KITE:
            case GIMMICK_ID.AIRPLANE:
            case GIMMICK_ID.UFO:
            case GIMMICK_ID.BIRD:
                Instantiate(gimmicks[(int)create_gimmick_id], create_gimmick_position, Quaternion.identity,this.transform);
                break;

            case GIMMICK_ID.STAR:
                if (meteor_shower_flag)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        Instantiate(gimmicks[(int)create_gimmick_id], create_gimmick_position, Quaternion.identity, this.transform);
                        DecideGimmickPosition();
                    }
                }
                else
                {
                    Instantiate(gimmicks[(int)create_gimmick_id], create_gimmick_position, Quaternion.identity, this.transform);
                }
                break;

            case GIMMICK_ID.RAINCLOUD:
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(gimmicks[(int)create_gimmick_id], create_gimmick_position, Quaternion.identity, this.transform);
                    DecideGimmickPosition();
                }
                break;

            case GIMMICK_ID.THUNDERCLOUD:
                for(int i = 0; i < 5; i++)
                {
                    Instantiate(gimmicks[(int)create_gimmick_id], create_gimmick_position, Quaternion.identity, this.transform);
                    DecideGimmickPosition();
                }

                create_gimmick_id = GIMMICK_ID.RAINCLOUD;

                CreateGimmick();

                break;
        }
    }
    
    //次に生成するギミックの種類を決める
    void DecideCreateGimmickId()
    {
        //ギミックが決定している時は返す
        if (gimmick_decide_flag)
            return;
        
        int random_weight  = Random.Range(0,total_weight);  
        int current_weight = 0;

        //ランダム値が0～重みの合計までのどの位置なのか判断する
        for(int i = 0; i < gimmick_weights.Length; i++)
        {
            //現在の重みの合計に次の重みを加える
            current_weight += gimmick_weights[i];

            //ランダム値が現在の合計以下なら、その要素番号のギミックにする
            if(random_weight <= current_weight)
            {
                create_gimmick_id = time_zone_gimmicks[i];
                break;
            }
        }

        //ギミックが決まったのでtrue
        gimmick_decide_flag = true;
    }

    //ギミックの重みの設定
    void SetGimmickWeights(int first_weight, int second_weight, int third_weight, int force_weight)
    {
        gimmick_weights[0] = first_weight;
        gimmick_weights[1] = second_weight;
        gimmick_weights[2] = third_weight;
        gimmick_weights[3] = force_weight;

        total_weight = 0;

        for (int i = 0; i < gimmick_weights.Length; i++)
        {
            total_weight += gimmick_weights[i];
        }
    }

    //流星群にするか決める
    void DecideWeatherMeteorShower()
    {
        //次に生成するギミックが星以外なら返す
        if (create_gimmick_id != GIMMICK_ID.STAR)
            return;

        //乱数を生成
        int num = Random.Range(0, 10);


        if (num > 7)
        {
            meteor_shower_flag = true;
        }
        else
        {
            meteor_shower_flag = false;
        }
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
        {
            ChangeStageTimeZone(STAGE_TIME_ZONE.EVENING);
            return;
        }


        //現時間帯の生成ギミックを決める
        if (!gimmick_set_flag)
        {
            SetTimeZoneGimmicks(GIMMICK_ID.KITE, GIMMICK_ID.AIRPLANE, GIMMICK_ID.RAINCLOUD, GIMMICK_ID.THUNDERCLOUD);
            SetGimmickWeights(50, 40, 5, 5);
            gimmick_set_flag = true;
            Debug.Log("朝");
        }

        //時間帯が切り替わる15秒前の時は返す
        if (game_timer.GetCountDownSecond() <= 15.0f)
            return;

        //生成するギミックを決める
        DecideCreateGimmickId();
    }

    //夕方の時
    void TimeZoneEvening()
    {
        //残り2分になったら夜に変更
        if (game_timer.GetCountDownTime() < 2)
        {
            ChangeStageTimeZone(STAGE_TIME_ZONE.NIGHT);
            return;
        }
       
        //現時間帯のギミックを決める
        if (!gimmick_set_flag)
        {
            SetTimeZoneGimmicks(GIMMICK_ID.BIRD, GIMMICK_ID.AIRPLANE, GIMMICK_ID.RAINCLOUD, GIMMICK_ID.THUNDERCLOUD);
            SetGimmickWeights(50, 40, 5, 5);
            gimmick_set_flag = true;
            Debug.Log("昼");

        }

        //時間帯が切り替わる15秒前の時は返す
        if (game_timer.GetCountDownSecond() <= 15.0f)
            return;

        //生成するギミックを決める
        DecideCreateGimmickId();

    }

    //夜の時
    void TimeZoneNight()
    {
        //現時間帯のギミックを決める
        if (!gimmick_set_flag)
        {
            SetTimeZoneGimmicks(GIMMICK_ID.STAR, GIMMICK_ID.UFO, GIMMICK_ID.RAINCLOUD, GIMMICK_ID.THUNDERCLOUD);
            SetGimmickWeights(60, 30, 5, 5);
            gimmick_set_flag = true;

            Debug.Log("夜");
        }

        //時間帯が切り替わる15秒前の時は返す
        if (game_timer.GetCountDownSecond() <= 15.0f)
            return;

        //生成するギミックを決める
        DecideCreateGimmickId();

        //天候が流星群かどうか決める
        DecideWeatherMeteorShower();
    }


    //ステージの時間帯変更
    void ChangeStageTimeZone(STAGE_TIME_ZONE change_time_zone)
    {
        stage_time_zone = change_time_zone;
        gimmick_set_flag = false;
    }

    //ステージの時間帯取得
    public STAGE_TIME_ZONE GetStageTimeZone()
    {
        return stage_time_zone;
    }

    //時間帯ごとのギミック識別子の設定
    void SetTimeZoneGimmicks( GIMMICK_ID first_id,  GIMMICK_ID second_id,  GIMMICK_ID third_id,  GIMMICK_ID force_id)
    {
        time_zone_gimmicks[0] = first_id;
        time_zone_gimmicks[1] = second_id;
        time_zone_gimmicks[2] = third_id;
        time_zone_gimmicks[3] = force_id;
    }


    /*その他の処理*/

   void SetPlayerData()
    {
        for(int i = 0; i < 4; i++)
        {
            result_score.player_total_sums[i]           = player_number_datas[i].GetTotalSum();
            result_score.player_number_sums[i]          = player_number_datas[i].GetNumberSum();
            result_score.player_total_bonus_points[i]   = player_number_datas[i].GetTotalBonusPoint();
            result_score.player_change_number_counts[i] = player_number_datas[i].GetChangeNumberCount();
        }
    }

    

    
}
