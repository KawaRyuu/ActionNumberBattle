using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�A�C�e���̎�ނƏ����̃N���X
//�v���C���[�̃X�e�[�^�X�Q��
public class Item
{
    PlayerData Pl;

    /*�A�C�e�������F������ނ̃A�C�e���͏����s�\*/

    /*�ǂ̃A�C�e�����������Ă��邩�̕ϐ�*/
    public bool Change_min_Flg      = false;//�����I�Ƀ����_���D�悪�Ⴂ���֓K�p�����B
    public bool Num_Random_Flg      = false;//4���̐��̂����A��ԒႢ�����P�`�X�Ƀ����_���ŕς��B
    public bool Nummin_one_Flg      = false;//4���̐��̒���1�ȊO�̏���������1��1�ɕς��B
    public bool Change_random_Flg   = false;//���g�̐����������_���Ȑl�ƌ����i�������Ă���l���m�͌�����j
    public bool Speed_up_Flg        = false;//���g�ړ����xUP�i3�b�ԁj
    public bool Spdown_Flg          = false;//����ɓ�����΂��Ĉړ����xDown�i2�b�ԁj
    public bool Critical_Flg        = false;//�ꌂ�C��̃A�C�e��

    /*�A�C�e���������Ɏg���ϐ�*/
    public float speed_up_timer = 0.0f;//�֐�Speed_Up�Ŏg�p����^�C�}�[

    /*�A�C�e���̏���������֐�*/

    //�����I�Ƀ����_���D�悪�Ⴂ���֓K�p�����B
    public int Change_min()
    {
        int min = 9;
        //�����������Ă���z��̒��ň�ԏ�������������点��
        //��ԏ������������Z�o


        return min;
        /*
         * �����F���̃A�C�e���g�p�������
         * ���g�̐�����ύX����A�C�e�����g�p������
         * �Ԃ����z��ԍ����ŏ��ł͂Ȃ��l�ɂȂ�\��������
         * 
         *�������āF�����ύX����A�C�e���g�p���ɂ�����x���̊֐����Ăяo��
         */

    }

    //4���̐��̂����A��ԒႢ�����P�`�X�Ƀ����_���ŕς��B
    public void NUM_Random()
    {
        
    }

    //4���̐��̒���1�ȊO�̏���������1��1�ɕς��B
    public void NumMin_One()
    {
        
    }

    //���g�̐����������_���Ȑl�ƌ����i�������Ă���l���m�͌�����j
    public void Change_Random()
    {
        
    }

    //���g�ړ����xUP�i3�b�ԁj
    /*�A�C�e���g�����Ƃ��Ɉړ����x�A�b�v�̊֐��������Ă���*/
    public void Speed_Up()
    {
        Pl.Speed = 300.0f;
        speed_up_timer += Time.deltaTime;
        //�ʂ̃A�C�e���g�p���Ƀo�O�N����\������
        if (speed_up_timer > 3.0f)
            Pl.Speed = 5.0f;

    }

    //����ɓ�����΂��Ĉړ����xDown�i2�b�ԁj
    /*PlayerData�ɓݑ��������聨�ݑ��^�O�����I�u�W�F�N�g�����v���O������*/
    public void SpDown()
    {
        //plefab�������v���C���[�������Ă�������ɔ���(3�}�X���i�񂾂������)
        //plefab����

        //��������plefab���������ɔ���
        //�R�}�X�i�񂾂�delete����Fif��
    }

    //�ꌂ�C��̃A�C�e��
    public void Critical()
    {
        Pl.Attack = 3;
    }

}

public class ItemSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
