using System;
using System.Collections.Generic;
using UnityEngine;


/*�v���C���[�i���C�o�����܂ށj�̃f�[�^���Ǘ�����B*/
//�v���C���[�̊�b�I�ȃf�[�^
public class PlayerData : MonoBehaviour
{

    public string Id = ("");     //�����炭���O
    public int Hp = 3;        //�̗́i�ϋv�l�j
    public int Attack = 1;        //�U��
    public int NomberBox = 4;        //4�̐���ێ�����
    public int RandomNomber = 0;        //�����̐����o�������_������������
    public float Speed = 5.0f;     //�ړ����x
    public float CoolTime = 4f;
    //�Z��D��i�����j���܂߂��N�[���^�C���S��

    public float invincibility = 0.5f;     //�_���[�W�������ۂ̖��G����
    public bool SwoonFlg = false;    //�C��t���O
    public bool StunFlg = false;     //�X�^���t���O
    public bool RecoveryTime_Flg = false;    //�񕜃^�C���ɓ���t���O
    public bool Invincibility_Flg = false;    //���G�t���O
    public bool ExchangeTakeover_Flg = false;    //�����D�悷��t���O
    public bool BluntFootEffect_Flg = false;    //�ݑ����ʂ̃t���O

    public float inv_count = 0.0f;      //���G���Ԓ��̃J�E���g

    //������
    void Start()
    {
        Invincibility_Flg   = false;
        SwoonFlg            = false;
        BluntFootEffect_Flg = false;
        inv_count = 0.0f;
    }

     void Update()
    {
        //���G�t���O��ON���������G���Ԃ�0.5�b�ȉ��Ȃ�
        if (Invincibility_Flg && invincibility >= inv_count)
        {
            inv_count += Time.deltaTime;
        }
        else
        {
            //���G����
            Invincibility_Flg = false;
            inv_count = 0;
        }

        //�����ݑ����ʂ̃t���O��false�Ȃ�ʏ�̑��x
        if(!BluntFootEffect_Flg)
        {
            Speed = 5.0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //�G�̍U���iEnemyAttack�Ƃ���tag�j�ɐG�ꂽ�Ƃ�
        if (other.gameObject.tag == "EnemyAttack")
        {
            //�������G��Ԃ���Ȃ����C�₵�ĂȂ��Ƃ��Ȃ�
            if (!Invincibility_Flg && !SwoonFlg)
            {
                Hp -= Attack;         //�̗͂�Attack�̍U���Q�ƂŌ���
                Invincibility_Flg = true;  //���G�t���OON
            }

            //�����̗͂�0�ȉ��ɂȂ�����
            if (Hp >= 0)
            {
                //�C�⏈��
                SwoonFlg = true;
                Hp = 3;
            }
        }

        //�����ݑ����ʂ�Tag�ɐG�ꂽ��
        if (other.gameObject.tag == "BluntFootEffect")
        {
            //���x��1����0.05�̑��x�ɕω�����B
            Speed = 0.05f;
            BluntFootEffect_Flg = true;
        }
        else BluntFootEffect_Flg = false;
    }
}
