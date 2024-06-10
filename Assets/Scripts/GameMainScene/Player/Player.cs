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


    public int stBackCount = 0;     //�X�g���C�N&back��2�x�����J�E���g
    public bool stBackFlg  = false; //�X�g���C�N&back��2�x�����t���O

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
                    waza.technicalFlg1 = true;
                }
                //����P�{�^�����������Ƃ����N�[���^�C����0�̎��̂�(�Z2)
                if (Input.GetKeyDown(KeyCode.P) && info.Tec02_CoolTime <= 0)
                {
                    waza.technicalFlg2 = true;
                }


                Debug.Log("����ł���");
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

        //�Z�X�g���C�N&back�̋Z���ő�2�񕪃J�E���g����B
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("�X�g���C�N&back");
            //�����Z�{�^����3���菬�����Ȃ�J�E���gup
            if (stBackCount < 2)
            {
                stBackFlg = false;
                stBackCount++;
                waza.technicalNumber = 3;
                //�����Z�{�^����2�񉟂����Ȃ�
                if (stBackCount == 2)
                {
                    Debug.Log("2�x�ʂ���");
                    stBackFlg = true;   //���̈ʒu�֖߂�t���O
                    
                    //�J�E���g�����������A
                    //�Z�̃N�[���^�C�����͂���
                    stBackCount = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            waza.technicalNumber = 2;
        }
    }

    //Player�̎���
    public int GetPlayer()
    {
        return playerInput.user.index;
    }
}
