using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Player��InputSystem
    PlayerInput playerInput;

    //�v���C���[�̃f�[�^�N���X����Q��
    PlayerData info;
    TechnicalData waza;


    public int stBackCount = 0;             //�X�g���C�N&back��2�x�����J�E���g
    public bool stBackFlg = false;         //�X�g���C�N&back��2�x�����t���O
    public bool StBc_TimeOverFlg = false;   //StBack�̋Z2�񉟂��Ȃ��������̃t���O

    const float time = 5.0f;      //�o�b�N�̓��͎�t����(�萔��)
    public float num = 0;        //��������

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }


    // ������
    void Start()
    {
        info = GetComponent<PlayerData>();
        waza = GetComponent<TechnicalData>();
        stBackCount = 0;
        num = time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        //InputSystem��actionmap����Move���擾
        var input_value = playerInput.actions["Move"].ReadValue<Vector2>();

        //Player�̊�{�̓���
        //�����C�⒆�Ȃ�s���s��
        if (!info.Swoon_Flg)
        {
            //�Z�������͍s���s��
            if (!waza.inactionableFlg)
            {
                //����O�{�^�����������Ƃ����N�[���^�C����0�̎��̂�(�Z1)
                if (Input.GetKeyDown(KeyCode.O) && info.Tec01_CoolTime <= 0)
                {
                    //�Z�g�P��ture�ɂ���
                    waza.technicalFlg1 = true;

                    //�����Z3�������ꍇ
                    if (waza.technicalNumber == 3)
                        StrikeBack();//2�񉟂������̌Ăяo��
                }

                //����P�{�^�����������Ƃ����N�[���^�C����0�̎��̂�(�Z2)
                if (Input.GetKeyDown(KeyCode.P) && info.Tec02_CoolTime <= 0)
                {
                    //�Z�g2��ture�ɂ���
                    waza.technicalFlg2 = true;

                    //�����Z3�������ꍇ
                    if (waza.technicalNumber == 3)
                        StrikeBack();//2�񉟂������̌Ăяo��
                }

                Debug.Log("����ł���");

                //Player�̃|�W�V�����ɑ��x��������
                position.x = input_value.x * info.Speed;
                position.y = input_value.y * info.Speed;

                //�������֌�������
                if (position.x < 0)
                {

                }

                //if (Input.GetKey("left"))
                //{
                //    position.x -= info.Speed * Time.deltaTime;          //������
                //}
                //else if (Input.GetKey("right"))
                //{
                //    position.x += info.Speed * Time.deltaTime;          //�E����
                //}
                //if (Input.GetKey("up"))
                //{
                //    position.y += info.Speed * Time.deltaTime;          //�����
                //}
                //else if (Input.GetKey("down"))
                //{
                //    position.y -= info.Speed * Time.deltaTime;          //������
                //}

                transform.position += Time.deltaTime * position;
            }
        }
        Debug.Log("���̗̑͂�" + info.Hp);

        //�g�b�V���i���j
        if (Input.GetKeyDown(KeyCode.T))
        {
            waza.technicalNumber = 2;
        }

        //�����Z3�����銎�A�Z�g1�̃t���O��True�Ȃ�
        if (waza.technicalNumber == 3 && waza.technicalFlg1)
            Timer();

        //�����Z3�����銎�A�Z�g2�̃t���O��True�Ȃ�
        else if (waza.technicalNumber == 3 && waza.technicalFlg2)
            Timer();
    }

    //�Z�X�g���C�N&back�̋Z���ő�2�񕪃J�E���g����B
    void StrikeBack()
    {
        //�����Z�{�^����3���菬�����Ȃ�J�E���gup
        if (stBackCount < 3)
        {
            stBackCount++;
            Debug.Log("�X�g���C�N&back�J�E���g" + stBackCount);

            //�{�^�����ő�����ɒB������
            if (stBackCount == 3)
            {
                //�����Z�{�^����2�񉟂����Ȃ�
                Debug.Log("2�x�ʂ���");
                num = time;
                stBackFlg = true;   //���̈ʒu�֖߂�t���O
            }
        }
    }

    //�X�g���C�N&�o�b�N�̃^�C�}�[(��x��t��)
    void Timer()
    {
        Debug.Log("�Z��x��t�\�c��" + num);
        if (!StBc_TimeOverFlg)
        {
            //�����������Ԃ�0�b�ȏ�Ȃ�
            if (num >= 0)
            {
                //�J�E���g�_�E����������
                num -= Time.deltaTime;
            }
            //�������Ԃ𒴂�����
            else
            {
                num = time;
                StBc_TimeOverFlg = true;
                stBackFlg = true;
            }
        }
    }

    //Player�̎���
    public int GetPlayer()
    {
        return playerInput.user.index;
    }
}
