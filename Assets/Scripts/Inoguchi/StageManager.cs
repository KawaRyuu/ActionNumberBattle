using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //�X�e�[�W�̎��ԑт̎��
    enum STAGE_TIME_ZONE
    {
        MORNING,    //��
        EVENING,    //�[
        NIGHT,      //��
        EMPTY,      //�����Ȃ�
    }

    STAGE_TIME_ZONE  stage_time_zone = STAGE_TIME_ZONE.EMPTY;   //���݂̎��ԑ�
                     
 

    private void Start()
    {
       InitializeStageManager();
    }

    //������
    void InitializeStageManager()
    {
        stage_time_zone = STAGE_TIME_ZONE.MORNING;
    }

    private void Update()
    {
        CheckStageState();
    }


    //���݂̃X�e�[�W�̎��ԑт��m�F
    void CheckStageState()
    {
        //���ԑтɂ���Ďd�|�����ς��
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

    //���̎��ԑ�
    void TimeZoneMorning()
    {
        
    }

    //���̎��ԑ�
    void TimeZoneEvening()
    {

    }
   
    //��̎��ԑ�
    void TimeZoneNight()
    {

    }

    void ChangeStageTimeZone(STAGE_TIME_ZONE change_time_zone)
    {
        stage_time_zone = change_time_zone;
    }
}
