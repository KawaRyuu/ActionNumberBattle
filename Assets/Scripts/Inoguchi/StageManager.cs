using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //ステージの時間帯の種類
    enum STAGE_TIME_ZONE
    {
        MORNING,    //朝
        EVENING,    //夕
        NIGHT,      //夜
        EMPTY,      //何もなし
    }

    STAGE_TIME_ZONE  stage_time_zone = STAGE_TIME_ZONE.EMPTY;   //現在の時間帯
                     
 

    private void Start()
    {
       InitializeStageManager();
    }

    //初期化
    void InitializeStageManager()
    {
        stage_time_zone = STAGE_TIME_ZONE.MORNING;
    }

    private void Update()
    {
        CheckStageState();
    }


    //現在のステージの時間帯を確認
    void CheckStageState()
    {
        //時間帯によって仕掛けが変わる
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

    //朝の時間帯
    void TimeZoneMorning()
    {
        
    }

    //昼の時間帯
    void TimeZoneEvening()
    {

    }
   
    //夜の時間帯
    void TimeZoneNight()
    {

    }

    void ChangeStageTimeZone(STAGE_TIME_ZONE change_time_zone)
    {
        stage_time_zone = change_time_zone;
    }
}
