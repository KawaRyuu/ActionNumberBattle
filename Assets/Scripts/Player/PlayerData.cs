using System;
using System.Collections.Generic;
using UnityEngine;


/*�v���C���[�i���C�o�����܂ށj�̃f�[�^���Ǘ�����B*/

//�v���C���[�̊�b�I�ȃf�[�^
public class PlayersData
{
    public string Id = ("");     //�����炭���O
    public int Hp = 3;           //�̗́i�ϋv�l�j
    public int Attack = 1;       //�U��
    public int NomberBox = 4;    //4�̐���ێ�����
    public float Speed = 1.0f;   //�ړ����x

    public float CoolTime = 4f;
    //�Z��D��i�����j���܂߂��N�[���^�C���S��

    public float invincibility = 0.5f;     //�_���[�W�������ۂ̖��G����
    public int RandomNomber = 0;           //�����̐����o�������_������������
    public bool SwoonFlg = false;          //�C��t���O
    public bool RecoveryTime_Flg = false;  //�񕜃^�C���ɓ���t���O
    public bool Invincibility_Flg = false; //���G�t���O

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
