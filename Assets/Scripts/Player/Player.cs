using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�v���C���[�̃f�[�^����Q��
    PlayersData info;

    public float Count;

    // Start is called before the first frame update
    void Start()
    {
        info = GetComponent<PlayersData>();
        Count = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //���G�t���O��ON���������G���Ԃ�0.5�b�ȉ��Ȃ�
        if (info.Invincibility_Flg && info.invincibility >= Count)
        {
            Count += Time.deltaTime;
        }
        else
        {
            //���G����
            info.Invincibility_Flg = false;
            Count = 0;
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //�G�ꂽ�Ƃ�
        if(other.collider)
        {
            //�������G��Ԃ���Ȃ����C�₵�ĂȂ��Ƃ��Ȃ�
            if (!info.Invincibility_Flg&&!info.SwoonFlg)
            {
                info.Hp -= info.Attack;         //�̗͂�Attack�̍U���Q��
                info.Invincibility_Flg = true;  //���G�t���OON
            }

            //�����̗͂�0�ȉ��ɂȂ�����
            if (info.Hp >= 0)
            {
                //�C�⏈��
                info.SwoonFlg = true;
                info.Hp = 3;
            }
        }
    }
}
