using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�v���C���[�̑���
    //private float playerSpeed = 5.0f;

    //�v���C���[�̃f�[�^�N���X����Q��
    PlayerData info;

    // ������
    void Start()
    {
        info = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        //Player�̊�{�̓���
        //�����C�⒆�Ȃ�s���s��
        if (!info.Swoon_Flg)
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
        //Debug.Log(("���̃X�s�[�h��") + info.Speed);
        Debug.Log("���̗̑͂�" + info.Hp);
        //Debug.Log("���G�t���O��:" + info.Invincibility_Flg);
    }
}
