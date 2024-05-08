using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //�v���C���[�̑���
    //private float playerSpeed = 5.0f;

    //�v���C���[�̃f�[�^�N���X����Q��
    PlayerData info;
    TechnicalData waza;

    public int stBackCount = 0;     //�X�g���C�N&back��2�x�����J�E���g
    public bool stBackFlg  = false; //�X�g���C�N&back��2�x�����t���O

    // ������
    void Start()
    {
        info = GetComponent<PlayerData>();
        waza = GetComponent<TechnicalData>();
        stBackCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        //Player�̊�{�̓���
        //�����C�⒆�Ȃ�s���s��
        if (!info.Swoon_Flg)
        {
            //�Z�������͍s���s��
            if (!waza.inactionableFlg)
            {
                if (Input.GetKey("left"))
                {
                    position.x -= info.Speed * Time.deltaTime;          //������
                }
                else if (Input.GetKey("right"))
                {
                    position.x += info.Speed * Time.deltaTime;          //�E����
                }
                else if (Input.GetKey("up"))
                {
                    position.y += info.Speed * Time.deltaTime;          //�����
                }
                else if (Input.GetKey("down"))
                {
                    position.y -= info.Speed * Time.deltaTime;          //������
                }
                transform.position = position;
            }
        }
        //Debug.Log(("���̃X�s�[�h��") + info.Speed);
        Debug.Log("���̗̑͂�" + info.Hp);
        //Debug.Log("���G�t���O��:" + info.Invincibility_Flg);


        //�Z�X�g���C�N&back�̋Z���ő�2�񕪃J�E���g����B
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("�X�g���C�N&back");
            //�����Z�{�^����3���菬�����Ȃ�J�E���gup
            if (stBackCount < 3)
            {
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

        //�������{�^�����������Ƃ����N�[���^�C����0�̎��̂�
        if (Input.GetKeyDown(KeyCode.P) && info.Tec0_CoolTime <= 0)
        {
            waza.technicalNumber = 1;
        }
    }
}
