using System;
using System.Collections.Generic;
using UnityEngine;


/*�v���C���[�i���C�o�����܂ށj�̃f�[�^���Ǘ�����B*/

//�v���C���[�̊�b�I�ȃf�[�^
public class PlayersData
{
    private string Id = ("");     //�����炭���O
    private int Hp = 3;           //�̗́i�ϋv�l�j
    private int Attack = 1;       //�U��
    private int NomberBox = 4;    //4�̐���ێ�����
    private float Speed = 1.0f;   //�ړ����x

    private float CoolTime = 4f;
    //�Z��D��i�����j���܂߂��N�[���^�C���S��

    private float invincibility = 0.5f;     //�_���[�W�������ۂ̖��G����
    private int  RandomNomber = 0;          //�����̐����o�������_������������
    private bool SwoonFlg = false;          //�C��t���O
    private bool RecoveryTime_Flag = false; //�񕜃^�C���ɓ���t���O

}
public class PlayerData : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        
    }
}
