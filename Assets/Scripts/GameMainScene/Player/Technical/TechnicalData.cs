using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TechnicalData : MonoBehaviour
{
    //Player�̃X�N���v�g����Q��
    Player player;
    //PlayerData�̃X�N���v�g����Q��
    PlayerData playerD;

    Fonts font;

    //�c�o���Ԃ����ɏo�鎩�����ӂ̓����蔻��Obj
    [SerializeField] GameObject Attack_obj_tubame;
    //�n�l�g�o�V���ɏo��U���I�u�W�F�N�g
    [SerializeField] GameObject Attack_obj_wing;

    public Text TecCool1;                //�Z1�̃N�[���^�C���\��
    public Text TecCool2;                //�Z2�̃N�[���^�C���\��
    public int technicalNumber = 0;      //�I�񂾎��_(Tec.1or2)�ł̔��̖���
    public bool inactionableFlg = false; //�ꕔ�̋Z���������A
                                         //�������莞�Ԗ����ɂ���t���O

    public int technicalNumber1 = 0;            //��ڂ̑I�����Ɍ��߂��킴��ۑ�
    public int technicalNumber2 = 0;            //��ڂ̑I�����Ɍ��߂��킴��ۑ�
    bool technicalFlg = false;                  //�킴�����������̃t���O
    public bool technicalFlg1 = false;          //�킴1�𔭓��������̃t���O
    public bool technicalFlg2 = false;          //�킴2�𔭓��������̃t���O
    bool swallowReturn_F = false;               //�c�o���Ԃ��̃t���O

    float Waza_time = 0.0f;                     //�킴�𔭓����̎���
    int wingCount = 0;                          //�n�l�g�o�V�̃J�E���g

    // Start is called before the first frame update
    void Start()
    {
        //������
        player = GetComponent<Player>();
        playerD = GetComponent<PlayerData>();
        font = GetComponent<Fonts>();
        Attack_obj_tubame.SetActive(false);
        technicalFlg = false;
        technicalFlg1 = false;
        technicalFlg2 = false;
        inactionableFlg = false;
        swallowReturn_F = false;
        technicalNumber = 0;
        technicalNumber1 = 0;
        technicalNumber2 = 0;
        Waza_time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        //�����\��
        TecCool1.text = (int)playerD.Tec0_CoolTime + "�b";

        //�����N�[���^�C��������Ȃ�
        if (playerD.Tec0_CoolTime >= 0)
        {
            Debug.Log("�Z1���g����܂�" + playerD.Tec0_CoolTime);
            playerD.Tec0_CoolTime -= Time.deltaTime;        //�J�E���g�_�E��
        }
        else
        {
            /*�����N�[���^�C����0�����������
            �󂫂̃N�[���^�C����0�֖߂�*/
            playerD.Tec0_CoolTime = 0.0f;
        }


        //�����c�o���Ԃ��̋Z�t���O��true�Ȃ�
        if (swallowReturn_F)
            Waza_time += Time.deltaTime;

        //�����Z���g�p�����Ȃ�
        if (technicalFlg)
        {
            //�����Z1��true�Ȃ�Z�̔ԍ��𔻕ʌ��switch���甭��
            if (technicalFlg1)
            {
                //�Z1�ɕۑ������ԍ���tecNum�֓����
                technicalNumber = font.Tec1;
            }
            else if (technicalFlg2)
            {
                technicalNumber = font.Tec2;
            }
        }
        

        //�Z�I�����̋�ʎ󂯎��
        switch (technicalNumber)
        {
            //�Z1 ���́u�n�l�g�o�V�v
            case 1:
                FeatherFlying();        //�Z1�̊֐����Ăяo��
                Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                break;

            //�Z2 ���́u�c�o���Ԃ��v
            case 2:
                SwallowReturn();
                Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                break;

            //�Z3 ���́u�X�g���C�N��back�v
            case 3:
                StrikeBack();
                Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                break;

            //�Z4 ���́u�g�b�V���v
            case 4:                                                                                                                                                                                         
                Rush();
                Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                break;

            //�Z�̑I���������I�΂Ȃ������������_���ŏ�������(��)
            case 5:
                Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                break;
        }

        //�Z1or2���g�����Ō�Ƀ��Z�b�g����
        Rest1_2();
    }

    /*************�t���O�ŏ����̏���***************************/
    private void Reset()
    {
        technicalFlg = false;
    }
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
        //�����N�[���^�C����0�b�ȉ��Ȃ�
        if (playerD.Tec0_CoolTime <= 0)
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
                playerD.Tec0_CoolTime = playerD.Tec1_CoolTime;
            }
        }
    }

    //�c�o���Ԃ��̓���
    void SwallowReturn()
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
        }
    }

    //�X�g���C�N&back�̓���
    void StrikeBack()
    {
        int count = 0;
        //�Z������
        float x;
        float y;
        //���݈ʒu���Z��������ɋL�^
        Vector2 posi = this.transform.position;

        //��x�O�i���ċA��ꍇ�����邽�߈ʒu��ێ�
        x = posi.x;
        y = posi.y;
        Debug.Log("�ێ��̓��e" + x + y);

        //�����őO�i����
        if (count < 1)
        {
            Debug.Log("�J�E���g" + count);
            posi.y = 2.0f + Time.deltaTime;
            this.transform.position = posi;
            count++;
        }
        //�����Z�̃{�^����2�񉟂�����ȑO�L�^�����ꏊ�֖߂�
        if (player.stBackFlg)
        {
            posi.x = x;
            posi.y = y;
            Debug.Log("���݂̃|�W�V������" + posi.x + posi.y);
            //����������
            player.stBackFlg = false;
            technicalNumber = 0;
        }

    }

    //�g�b�V���̓���
    void Rush()
    {
        /*���̊֐��֐؂�ւ���PlayerData�ł����̊֐��ԍ�����Z���������m��
         * playerData�ŃX�^���������s���܂�*/
        /*�����̊֐��̏����͎����̎��͂���ł��߂�Player�����m���ړ����鏈��*/
    }
}
