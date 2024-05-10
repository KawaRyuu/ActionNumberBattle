using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*�v���C���[�i���C�o�����܂ށj�̃f�[�^���Ǘ�����B*/
//�v���C���[�̊�b�I�ȃf�[�^
public class PlayerData : MonoBehaviour
{
    public SpriteRenderer player;
    public TechnicalData tec;

    public string Id = ("");         //�����炭���O
    public int Hp = 3;               //�̗́i�ϋv�l�j
    public int Attack = 1;           //�U��
    public int NomberBox = 4;        //4�̐���ێ�����
    public int RandomNomber = 0;     //�����̐����o�������_������������
    public float Speed = 5.0f;       //�ړ����x

    /********�N�[���^�C���̃J�E���g����************/
    public float Tec01_CoolTime = 0.0f;           //�Z1:��̃N�[���^�C��
    public float Tec02_CoolTime = 0.0f;           //�Z2:��̃N�[���^�C��
    public float Tec1_CoolTime = 11.0f;          //�Z1��p�̃N�[���^�C������
    public float Tec2_CoolTime = 11.0f;          //�Z2��p�̃N�[���^�C������
    public float Tec3_CoolTime = 11.0f;          //�Z3��p�̃N�[���^�C������
    public float Tec4_CoolTime = 11.0f;          //�Z4��p�̃N�[���^�C������

    public float invincibility = 0.5f;          //�_���[�W�������ۂ̖��G����
    public bool Swoon_Flg = false;              //�C��t���O
    public bool Stun_Flg = false;               //�X�^���t���O
    public bool RecoveryTime_Flg = false;       //�񕜃^�C���ɓ���t���O
    public bool Invincibility_Flg = false;      //���G�t���O
    public bool ExchangeTakeover_Flg = false;   //�����D�悷��t���O
    public bool BluntFootEffect_Flg = false;    //�ݑ����ʂ̃t���O

    public float inv_count = 0.0f;              //���G���Ԓ��̃J�E���g
    public float stun_count = 0.0f;             //�X�^�����̃J�E���g
    public float blunt_count = 0.0f;            //�ݑ����̃J�E���g
    public float hael_count = 0.0f;             //�񕜒��̃J�E���g
    public float swoon_count = 0.0f;            //�C�⒆�̃J�E���g

    //������
    void Start()
    {
        tec = GetComponent<TechnicalData>();
        Invincibility_Flg = false;
        Swoon_Flg = false;
        Stun_Flg = false;
        BluntFootEffect_Flg = false;
        inv_count = 0.0f;
        blunt_count = 0.0f;
        hael_count = 0.0f;
        swoon_count = 0.0f;
        stun_count = 0.0f;
        Tec01_CoolTime = 0.0f;
        Tec02_CoolTime = 0.0f;
        Tec1_CoolTime = 10.0f;
        Tec2_CoolTime = 10.0f;
        Tec3_CoolTime = 10.0f;
        Tec4_CoolTime = 10.0f;
    }

    void Update()
    {
        /**********�X�^���̏���************/
        //�����X�^���t���O��true���J�E���g��1.0�b�ȉ��Ȃ�
        if (Stun_Flg && stun_count <= 1.0f)
        {
            Debug.Log("�X�^����");
            stun_count += Time.deltaTime;   //�J�E���g���Z
        }
        else if (stun_count >= 1)           //�����J�E���g��1�b�𒴂�����
        {
            //�X�^����Ԃ�����
            stun_count = 0.0f;
            Stun_Flg = false;
        }

        /**********�C��̏���*************/
        //�����C��t���O��true���J�E���g��1.5�b�ȉ��Ȃ�
        if (Swoon_Flg && swoon_count <= 1.5f)
        {
            Debug.Log("�C��now");
            swoon_count += Time.deltaTime;    //�J�E���g�̉��Z
        }
        else if (swoon_count >= 1.5)          //�����J�E���g��1.5�b�𒴂�����
        {
            swoon_count = 0.0f;               //�C��J�E���g�����Z�b�g
            Swoon_Flg = false;              //�C��t���O��false�ɕς���
            Hp = 3;                  //HP�͋����őS��
        }


        /**********�񕜂̏���*************/
        //�����񕜃t���O��true���J�E���g��5.0�b�ȉ��Ȃ�
        //�����̍U��������̂�5�b�ȍ~�ɂȂ�Ȃ�S�񕜂���B
        if (RecoveryTime_Flg && hael_count <= 5.0f)
        {
            hael_count += Time.deltaTime;
            Debug.Log("�񕜒�");
            //Debug.Log("�񕜃J�E���g��" + hael_count);
        }
        //�����񕜂̃J�E���g��5�b�𒴂����Ȃ�
        else if (hael_count > 5.0)
        {
            Debug.Log("��");
            Hp = 3;                                 //Hp���񕜂���B
            hael_count = 0;                         //�J�E���g���Z�b�g
            RecoveryTime_Flg = false;               //�񕜃t���OOFF
        }


        /**********���G�̏���*************/
        //���G�t���O��ON���������G���Ԃ�0.5�b�ȉ��Ȃ�
        if (Invincibility_Flg && invincibility >= inv_count)
        {
            inv_count += Time.deltaTime;
            RecoveryTime_Flg = true;                //�񕜃t���O��ON�ɂ���
            StartCoroutine(BlinkingControl());
            Invincibility();                        //���G���̓_�ŏ����֐���
            Debug.Log("���G����" + inv_count);
        }
        else
        {
            //���G����
            Invincibility_Flg = false;
            inv_count = 0;
        }


        /**********�ݑ����ʂ̏���************/
        //�����ݑ����ʂ̃t���O��false�Ȃ�ʏ�̑��x
        if (!BluntFootEffect_Flg)
        {
            Speed = 5.0f;
        }

        //�ݑ����ʂ�true���ݑ��J�E���g��2.0�b�ȉ��Ȃ�
        if (BluntFootEffect_Flg && blunt_count <= 2.0)
        {
            blunt_count += Time.deltaTime;
        }
        else
        {
            //�ݑ���ԉ���
            BluntFootEffect_Flg = false;
            blunt_count = 0;
        }
    }

    //�ړ����xUP�֐�
    public void SpeedUp()
    {
    }

    //���G�����̊֐�(�_�ŏ���)
    public void Invincibility()
    {
        //�_�ł̏���(*20�͓_�ő��x)
        float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
        player.color = new Color(1f, 1f, 1f, level);
    }

    //�_�ł̐���֐�
    public IEnumerator BlinkingControl()
    {
        //0.5�b�̊ԓ_�ł��J��Ԃ�
        yield return new WaitForSeconds(0.5f);
        // �ʏ��Ԃɖ߂�
        player.color = new Color(1f, 1f, 1f, 1f);
    }






    /************�����������̏���(�����̓���������)*****************/
    private void OnTriggerEnter2D(Collider2D other)
    {
        //�G�̍U���iEnemyAttack�Ƃ���tag�j�ɐG�ꂽ�Ƃ�
        if (other.gameObject.tag == "EnemyAttack")
        {

            //�������G��Ԃ���Ȃ����C�₵�ĂȂ��Ƃ��Ȃ�
            if (!Invincibility_Flg && !Swoon_Flg)
            {
                //�t���O��true�̎��A�ǌ������ŗ�����
                if (RecoveryTime_Flg)
                {
                    //�񕜂̃L�����Z��
                    RecoveryTime_Flg = false;
                    hael_count = 0.0f;
                }

                Hp -= Attack;               //�̗͂�Attack�̍U���Q�ƂŌ���
                Invincibility_Flg = true;   //���G�t���OON
            }

            //�����̗͂�0�ȉ��ɂȂ�����
            if (Hp <= 0)
            {
                //�C��t���OON
                Swoon_Flg = true;
            }
        }

        //�����ݑ����ʂ�Tag�ɐG�ꂽ��
        if (other.gameObject.tag == "BluntFootEffect")
        {
            //���x��5����2.5�̑��x�ɕω�����B
            Speed = 2.5f;
            BluntFootEffect_Flg = true;
        }
        //�g�b�V��(�Z)������������Player�ɐG�ꂽ�Ƃ�
        if (other.gameObject.tag == "Player" && tec.technicalNumber == 4)
        {
            Stun_Flg = true;
        }
    }
}
