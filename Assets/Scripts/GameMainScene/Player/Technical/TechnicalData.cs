using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;

public class TechnicalData : MonoBehaviour
{
    //Player�̃X�N���v�g����Q��
    Player player;
    //PlayerData�̃X�N���v�g����Q��
    PlayerData playerD;


    //�c�o���Ԃ����ɏo�鎩�����ӂ̓����蔻��Obj
    [SerializeField] GameObject Attack_obj_tubame;

    //�n�l�g�o�V���ɏo��U���I�u�W�F�N�g
    [SerializeField] GameObject Attack_obj_wing;

    //Player�̍��W��G�邽�߂�(�X�g���C�N&back)
    [SerializeField] GameObject Player;

    //�X�g���C�N&back�̑O�i��Ɏ��g�������ꏊ�փ}�[�N����
    [SerializeField] GameObject Mark;


    //Player�̏����ʒu
    public static Vector3 PlayerLocation = new Vector2(0.0f, 0.0f);

    //Player�̈ړ����ۑ����邽�߂�
    public static Vector3 PlayerLocationGoal = new Vector2(0.0f, 0.0f);

    public Text TecCool1;                //�Z1�̃N�[���^�C���\��
    public Text TecCool2;                //�Z2�̃N�[���^�C���\��

    public int technicalNumber = 0;      //�I�񂾎��_(Tec.1or2)�ł̔��̖���
    public bool inactionableFlg = false; //�ꕔ�̋Z���������A
                                         //�������莞�Ԗ����ɂ���t���O

    public int technicalNumber1 = 0;            //��ڂ̑I�����Ɍ��߂��킴��ۑ�
    public int technicalNumber2 = 0;            //��ڂ̑I�����Ɍ��߂��킴��ۑ�
    public bool technicalFlg1 = false;          //�킴1�𔭓��������̃t���O
    public bool technicalFlg2 = false;          //�킴2�𔭓��������̃t���O
    bool swallowReturn_F = false;               //�c�o���Ԃ��̃t���O

    float Waza_time = 0.0f;                     //�킴�𔭓����̎���
    Vector3 StBackLoc = new Vector3(0.0f,2.0f); //�X�g���C�N&back�̈ړ�����
    int wingCount = 0;                          //�n�l�g�o�V�̃J�E���g

    /****�X�g���C�N���o�b�N*****/
    [SerializeField] private float MoveSpeed = 1000.0f;    //�ړ����x
    private int TargetDistance = 2;                     //�ړI����
    private float NowDistance = 0;                      //���ݒn
    private float Return = 0;                           //�o�b�N

    // Start is called before the first frame update
    void Start()
    {
        //������
        player = GetComponent<Player>();
        playerD = GetComponent<PlayerData>();
        Attack_obj_tubame.SetActive(false);
        technicalFlg1 = false;
        technicalFlg2 = false;
        inactionableFlg = false;
        swallowReturn_F = false;
        technicalNumber = 0;
        technicalNumber1 = 0;
        technicalNumber2 = 0;
        Waza_time = 0.0f;
        Player.transform.position = PlayerLocation;
    }

    // Update is called once per frame
    void Update()
    {
        //�i�Z:1�j�����N�[���^�C��������Ȃ�
        if (playerD.Tec01_CoolTime > 0)
        {
            Debug.Log("�Z1���g����܂�" + playerD.Tec01_CoolTime);
            playerD.Tec01_CoolTime -= Time.deltaTime;        //�J�E���g�_�E��
        }
        else
        {
            /*�����N�[���^�C����0�����������
            �󂫂̃N�[���^�C����0�֖߂�*/
            playerD.Tec01_CoolTime = 0.0f;
        }

        //(�Z:2)�����N�[���^�C��������Ȃ�
        if (playerD.Tec02_CoolTime >= 0)
        {
            Debug.Log("�Z2���g����܂�" + playerD.Tec02_CoolTime);
            playerD.Tec02_CoolTime -= Time.deltaTime;        //�J�E���g�_�E��
        }
        else
        {
            /*�����N�[���^�C����0�����������
            �󂫂̃N�[���^�C����0�֖߂�*/
            playerD.Tec02_CoolTime = 0.0f;
        }


        //�����c�o���Ԃ��̋Z�t���O��true�Ȃ�
        if (swallowReturn_F)
            Waza_time += Time.deltaTime;

        //�����Z1��true�Ȃ�Z�̔ԍ��𔻕ʌ��switch���甭��
            if (technicalFlg1)
            {
                //�Z1�ɕۑ������ԍ���tecNum�֓����
                technicalNumber = Fonts.Tec1;
            }
            else if (technicalFlg2)
            {
                technicalNumber = Fonts.Tec2;
            }
        
        

        //�Z�I�����̋�ʎ󂯎��
        switch (technicalNumber)
        {
            //�Z1 ���́u�n�l�g�o�V�v
            case 1:
                FeatherFlying();        //�Z1�̊֐����Ăяo��
                break;

            //�Z2 ���́u�c�o���Ԃ��v
            case 2:
                SwallowReturn();
                break;

            //�Z3 ���́u�X�g���C�N��back�v
            case 3:
                StrikeBack();
                break;

            //�Z4 ���́u�g�b�V���v
            case 4:
                Rush();
                break;

            //�Z�̑I���������I�΂Ȃ������������_���ŏ�������(��)
            case 5:
                break;
        }
    }

    //�҂�����1,2��PlayerTextManager�֎󂯓n��
    public int GetCoolTime1()
    {
        return (int)playerD.Tec01_CoolTime;
    }

    public int GetCoolTime2()
    {
        return (int)playerD.Tec02_CoolTime;
    }


    /*************�t���O�ŏ����̏���***************************/
    private void Rest1_2()
    {
        //����technicalFlg1��true�Ȃ�
        if (technicalFlg1)
            technicalFlg1 = false;
        //����technicalFlg2��true�Ȃ�
        else if (technicalFlg2)
            technicalFlg2 = false;

    }


    /*************���ꂼ��̋Z�̓�������***********************/

    //�n�l�g�o�V�̓���
    void FeatherFlying()
    {
        //�Z1�Ȃ�
        if (technicalFlg1)
        {
            //�����N�[���^�C����0�b�ȉ��Ȃ�
            if (playerD.Tec01_CoolTime <= 0)
            {
                if (wingCount < 3)
                {
                    //�e�̐���
                    //200�t���[����1�x�����e�𔭎˂���
                    if (Time.frameCount % 200 == 0)
                    {
                        //�n�l�g�o�V����
                        Instantiate(Attack_obj_wing,//��������I�u�W�F�N�g�̃v���n�u
                            this.transform.position,//�����ʒu
                            Quaternion.identity);//������]��
                        wingCount++;
                    }
                    //Debug.Log("wingCount��" + wingCount);
                }
                else
                {
                    //�H�̏�Ԃ�������
                    wingCount = 0;
                    technicalNumber = 0;
                    //Player��Data�ɂ���A�󂫂̃N�[���^�C����
                    //�Z1�̃N�[���^�C��������B
                    playerD.Tec01_CoolTime = playerD.FlyingFeather_CoolTime;
                    Rest1_2();              //�Z1or2���g�����Ō�Ƀ��Z�b�g����
                }
            }
        }


        //�Z2�Ȃ�
        if (technicalFlg2)
        {
            //�����N�[���^�C����0�b�ȉ��Ȃ�
            if (playerD.Tec02_CoolTime <= 0)
            {
                if (wingCount < 3)
                {
                    //�e�̐���
                    //200�t���[����1�x�����e�𔭎˂���
                    if (Time.frameCount % 200 == 0)
                    {
                        //�n�l�g�o�V����
                        Instantiate(Attack_obj_wing,//��������I�u�W�F�N�g�̃v���n�u
                            this.transform.position,//�����ʒu
                            Quaternion.identity);//������]��
                        wingCount++;
                    }
                    //Debug.Log("wingCount��" + wingCount);
                }
                else
                {
                    //�H�̏�Ԃ�������
                    wingCount = 0;
                    technicalNumber = 0;
                    //Player��Data�ɂ���A�󂫂̃N�[���^�C����
                    //�Z2�̃N�[���^�C��������B
                    playerD.Tec02_CoolTime = playerD.SwallowReturn_CoolTime;
                    Rest1_2();              //�Z1or2���g�����Ō�Ƀ��Z�b�g����
                }
            }
        }
    }

    //�c�o���Ԃ��̓���
    void SwallowReturn()
    {
        //�Z1�Ȃ�
        if (technicalFlg1)
        {
            //�����N�[���^�C����0�b�ȉ��Ȃ�
            if (playerD.Tec01_CoolTime <= 0)
            {
                //�������Ԃ�1.5�b�ȉ��Ȃ�
                if (Waza_time < 1.5f)
                {
                    //�s���s�̃t���O���ꎞ�I��ON�ɂ��A
                    //player�̑���script�ő����s�ɂ�����
                    inactionableFlg = true;
                    Attack_obj_tubame.SetActive(true);         //�Z�͈̔͂̓����蔻���\��
                    swallowReturn_F = true;
                }
                else if (Waza_time > 1.5f)
                {
                    inactionableFlg = false;
                    Attack_obj_tubame.SetActive(false);
                    swallowReturn_F = false;
                    Waza_time = 0.0f;
                    technicalNumber = 0;
                    //Player��Data�ɂ���A�󂫂̃N�[���^�C����
                    //�Z1�̃N�[���^�C��������B
                    playerD.Tec01_CoolTime = playerD.FlyingFeather_CoolTime;
                    Rest1_2();              //�Z1or2���g�����Ō�Ƀ��Z�b�g����
                }
            }
        }

        //�Z2�Ȃ�
        if (technicalFlg2)
        {
            //�����N�[���^�C����0�b�ȉ��Ȃ�
            if (playerD.Tec02_CoolTime <= 0)
            {
                //�������Ԃ�1.5�b�ȉ��Ȃ�
                if (Waza_time < 1.5f)
                {
                    //�s���s�̃t���O���ꎞ�I��ON�ɂ��A
                    //player�̑���script�ő����s�ɂ�����
                    inactionableFlg = true;
                    Attack_obj_tubame.SetActive(true);         //�Z�͈̔͂̓����蔻���\��
                    swallowReturn_F = true;
                }
                else if (Waza_time > 1.5f)
                {
                    inactionableFlg = false;
                    Attack_obj_tubame.SetActive(false);
                    swallowReturn_F = false;
                    Waza_time = 0.0f;
                    technicalNumber = 0;
                    //Player��Data�ɂ���A�󂫂̃N�[���^�C����
                    //�Z1�̃N�[���^�C��������B
                    playerD.Tec02_CoolTime = playerD.SwallowReturn_CoolTime;
                    Rest1_2();              //�Z1or2���g�����Ō�Ƀ��Z�b�g����
                }
            }
        }
    }

    

    //�X�g���C�N&back�̓���
    void StrikeBack()
    {
        //3�ɂ���ƘA�����ČĂяo����邩��ʂ̐�����^����
        technicalNumber = 5;

        //Vector3 move = Player.transform.position / MoveSpeed;

        //�����őO�i����
        if (player.stBackCount <= 1)
        {
            //���W��ۑ�
            PlayerLocation = Player.transform.position;

            Debug.Log("����");
            
            //�X�g���C�N�ʂ��ƈړ����鏈���i�Ăяo���j
            StartCoroutine(Move(Vector3.up));

            Instantiate(Mark,       //��������I�u�W�F�N�g�̃v���n�u(Mark)
            PlayerLocation,         //�����ʒu�͈ړ��O�ɂ����ꏊ
            Quaternion.identity);   //������]���
        }
        //�����Z�̃{�^����2�񉟂�����ȑO�L�^�����ꏊ�֖߂�
        if (player.stBackFlg)
        {
            //���W�o�^�����ƂɌ��݂̈ʒu�ɔ��f������
            this.transform.DOMove(PlayerLocation, 2.0f);
           // Player.transform.position = PlayerLocation;
            //StartCoroutine(Back());
            //����������
            technicalNumber = 0;
            player.stBackCount = 0;
            player.stBackFlg = false;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }


        /*********���l���������i�v)***************/
        ////�Z������
        //float x;
        //float y;
        ////���݈ʒu���Z��������ɋL�^
        //Vector2 posi = this.transform.position;

        ////��x�O�i���ċA��ꍇ�����邽�߈ʒu��ێ�
        //x = posi.x;
        //y = posi.y;
        //Debug.Log("�ێ��̓��e" + x + y);
        /********���l��������Part2*****************/
        if (false)
        {
            //�ړ��O�̍��W�ƈړ���̍��W��ۑ�
            PlayerLocation = Player.transform.position;
            PlayerLocationGoal = Player.transform.position + StBackLoc;

            Debug.Log(PlayerLocationGoal.y - this.transform.position.y);

            Debug.Log("y");

            Debug.Log("���݂̍��W" + Player.transform.position);

            //�ړ���֐i�񂾌�A�ړ��O�ɂ����ꏊ�Ƀ}�[�N��t�^����B
            Instantiate(Mark,           //��������I�u�W�F�N�g�̃v���n�u(Mark)
                PlayerLocation,         //�����ʒu�͈ړ��O�ɂ����ꏊ
                Quaternion.identity);   //������]���
        }
    }

    //�ړ�(�X�g���C�N����)
    IEnumerator Move(Vector3 TmpVector)
    {
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Debug.Log("�ڕW��"+NowDistance);
        while (true) 
            {
            //�I�������𖞂����Ă��邩�m�F
            //���݂̍��W���ړ�����
            //�������݂̍��W���ړ��斢���Ȃ�
            if (TargetDistance <= NowDistance +(MoveSpeed*Time.deltaTime))//�I������
            {
                transform.position += TmpVector * (TargetDistance - NowDistance);
                playerD.ActionFlg = true;   //�Z�������͑��̑�����󂯕t���Ȃ��i�������j
                NowDistance = 0;            //�ړ��O�̍��W���Z�b�g
                yield break;
            }
            //���͕����Ɉړ�
            transform.position += TmpVector * Time.deltaTime * MoveSpeed;
            
            //�ړ������������X�V
            NowDistance += MoveSpeed * Time.deltaTime;
            Debug.Log("�O�񂩂�̈ړ�����" + MoveSpeed * Time.deltaTime);
            Debug.Log("�݌v�ړ�����"+NowDistance);
            yield return null;
            }
    }

    //�ړ��i�o�b�N�j����
    IEnumerator Back()
    {
        Vector3 distance = PlayerLocation - player.transform.position;
       // Vector3 BackAngleVector = new Vector3(Cos(), distance.y / MoveSpeed, 0);
            //�I�������𖞂����Ă��邩�m�F
            //���݂̍��W���ړ�����
            //�������݂̍��W���ړ��斢���Ȃ�
            if (PlayerLocation.y >= Player.transform.position.y)//�I������
            {
                playerD.ActionFlg = true;   //�Z�������͑��̑�����󂯕t���Ȃ��i�������j
                NowDistance = 0;            //�ړ��O�̍��W���Z�b�g
                Return = 0;                 //���W���Z�b�g
                yield break;
            }

            //���͕����Ɉړ�
          //  transform.position += BackAngleVector * Time.deltaTime;
            //�ړ������������X�V
            Return -= MoveSpeed * Time.deltaTime;
            yield return null;
        
    }
    //�g�b�V���̓���
    void Rush()
    {
        /*���̊֐��֐؂�ւ���PlayerData�ł����̊֐��ԍ�����Z���������m��
         * playerData�ŃX�^���������s���܂�*/
        /*�����̊֐��̏����͎����̎��͂���ł��߂�Player�����m���ړ����鏈��*/
    }
}
