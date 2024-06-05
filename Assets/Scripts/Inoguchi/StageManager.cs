using UnityEngine;
using GimmickInfomation;

public class StageManager : MonoBehaviour
{
    //�X�e�[�W�̎��ԑт̎��
    public enum STAGE_TIME_ZONE
    {
        MORNING,        //��
        EVENING,        //�[
        NIGHT,          //��
        EMPTY,          //��̏��
    }

    STAGE_TIME_ZONE stage_time_zone   = STAGE_TIME_ZONE.EMPTY;   //���݂̎��ԑ�
    GIMMICK_ID      create_gimmick_id = GIMMICK_ID.EMPTY;

    //Unity��Őݒ�
    [SerializeField] GameObject[] gimmicks = new GameObject[gimmick_num];  //���������M�~�b�N�̔z��
    [SerializeField] GameTimer game_timer;                       //�c�莞�Ԃ̃X�N���v�g


    Vector3 create_gimmick_position = Vector3.zero;     //�����M�~�b�N�̈ʒu���W
    float   create_gimmick_time = 10.0f;                 //�M�~�b�N�����̃^�C�}�[
    float   create_gimmick_timer = 0.0f;                //�M�~�b�N��������
    bool    gimmick_decide_flag = false;                //�M�~�b�N�����܂��Ă��邩����

    const int gimmick_num = (int)GIMMICK_ID.EMPTY;

    private void Start()
    {
        InitializeStageManager();
    }

    //������
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

    /*�M�~�b�N�̏���*/

    //�X�e�[�W�M�~�b�N�����܂ł̍H�����܂Ƃ߂��֐�
    void StageGimmick()
    {
        //��������M�~�b�N�̎�ނ����܂��Ă��Ȃ����͕Ԃ�
        if (!gimmick_decide_flag)
            return;

        //�^�C�}�[����
        create_gimmick_timer += Time.deltaTime;

        //�������Ԍo�߂���܂Œʂ��Ȃ�
        if (create_gimmick_timer < create_gimmick_time)
            return;

        //�^�C�}�[������
        create_gimmick_timer = 0;

        //����������W�����߂�
        DecideGimmickPosition();

        //�M�~�b�N����
        Instantiate(gimmicks[(int)create_gimmick_id], create_gimmick_position, Quaternion.identity);

        //����������M�~�b�N�����܂��ĂȂ��̂�false
        gimmick_decide_flag = false;
    }


    //��������M�~�b�N�̍��W�����߂�
    void DecideGimmickPosition()
    {
        Vector3 position = Vector3.zero;
        float x = 0;
        float y = 0;


        if (create_gimmick_id == GIMMICK_ID.STAR)
        {
            //���ꂩ����M�~�b�N����

            //��ʊO�̉E��ɐ�������

            y = 1.1f;
            x = 1.1f;
        }
        else
        {
            //���ꂩ����M�~�b�N�����ȊO

            //�M�~�b�N����ʊO�̏㉺���E�ǂ��ɐ������邩���߂�
            int num = Random.Range(0, 4);

            //0�͏�A1�͉E�A2�͉��A3�͍�
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

        //x,y�ŕ\�������W�����
        position = new Vector3(x, y, Camera.main.nearClipPlane);

        //��������W���A�J�����̘g�ɍ��킹���`�ɂ���
        create_gimmick_position = Camera.main.ViewportToWorldPoint(position);
    }


  
    /*���ԑт̏���*/

    //�X�e�[�W�̎��ԑт̊m�F
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

    //���̎�
    void TimeZoneMorning()
    {
        //�c��3���ɂȂ�����[���ɕύX
        if (game_timer.GetCountDownTime() < 3)
            ChangeStageTimeZone(STAGE_TIME_ZONE.EVENING);

        //�M�~�b�N�����肵�Ă��鎞�͕Ԃ�
        if (gimmick_decide_flag)
            return;

        create_gimmick_id = GIMMICK_ID.KITE;

        gimmick_decide_flag = true;
    }

    //�[���̎�
    void TimeZoneEvening()
    {
        //�c��2���ɂȂ������ɕύX
        if (game_timer.GetCountDownTime() < 2)
            ChangeStageTimeZone(STAGE_TIME_ZONE.NIGHT);

        //�M�~�b�N�����肵�Ă��鎞�͕Ԃ�
        if (gimmick_decide_flag)
            return;

        create_gimmick_id = GIMMICK_ID.BIRD;

        gimmick_decide_flag = true;

    }

    //��̎�
    void TimeZoneNight()
    {
        //�M�~�b�N�����肵�Ă��鎞�͕Ԃ�
        if (gimmick_decide_flag)
            return;

        create_gimmick_id = GIMMICK_ID.STAR;

        gimmick_decide_flag = true;

    }


    //�X�e�[�W�̎��ԑѕύX
    void ChangeStageTimeZone(STAGE_TIME_ZONE change_time_zone)
    {
        stage_time_zone = change_time_zone;

    }

    //�X�e�[�W�̎��ԑю擾
    public STAGE_TIME_ZONE GetStageTimeZone()
    {
        return stage_time_zone;
    }



   

    /*���̑��̏���*/

}
