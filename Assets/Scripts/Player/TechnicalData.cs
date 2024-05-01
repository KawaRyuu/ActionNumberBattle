using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class TechnicalData : MonoBehaviour
{
    //Player�̃X�N���v�g����Q��
    Player player;
    public int technicalNomber  = 0;            //�I�񂾎��_�ł̔��̖���
    int technicalNomber1 = 0;            //��ڂ̑I�����Ɍ��߂��킴��ۑ�
    int technicalNomber2 = 0;            //��ڂ̑I�����Ɍ��߂��킴��ۑ�
    bool technicalFlg = false;           //�킴�����������̃t���O
    bool technicalFlg1 = false;          //�킴1�𔭓��������̃t���O
    bool technicalFlg2 = false;          //�킴2�𔭓��������̃t���O

    // Start is called before the first frame update
    void Start()
    {
        //������
        player = GetComponent<Player>();
        technicalFlg = false;
        technicalFlg1 = false;
        technicalFlg2 = false;
        technicalNomber  = 0;
        technicalNomber1 = 0;
        technicalNomber2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�����Z���g�p�����Ȃ�
        if (technicalFlg)
        {
            //�����Z1��true�Ȃ�Z�̔ԍ��𔻕ʌ��switch���甭��
            if (technicalFlg1)
                technicalNomber = technicalNomber1;
            else if (technicalFlg2)
                technicalNomber = technicalNomber2;
        }


            //�Z�I�����̋�ʎ󂯎��
            switch (technicalNomber)
            {
                //�Z1 ���́u�n�l�g�o�V�v
                case 1:
                    FeatherFlying();        //�Z1�̊֐����Ăяo��
                    Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                    break;

                //�Z2 ���́u�c�o���Ԃ��v
                case 2:
                    SwallowReturn();
                    Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                break;

                //�Z3 ���́u�X�g���C�N��back�v
                case 3:
                    StrikeBack();
                    Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                break;

                //�Z4 ���́u�g�b�V���v
                case 4:
                    Rush();
                    Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                break;

                //�Z�̑I���������I�΂Ȃ������������_���ŏ�������(��)
                case 5:
                    Reset();                //�Z�g�p���ɋZ�g�p�t���O�����Z�b�g
                break;
            }

        //�Z1or2���g�����Ō�Ƀ��Z�b�g����
        Rest1_2();
    }
    /*************�t���O�ŏ����̏���***************************/
    private void Reset()
    {
        technicalFlg = false;
    }
    private void Rest1_2()
    {
        //����technicalFlg1��true�Ȃ�
        if (technicalFlg1)
        technicalFlg1 = false;
        //����technicalFlg2��true�Ȃ�
        else if (technicalFlg2)
        technicalFlg2 = false;

    }

    /*************���ꂼ��̋Z�̓�������***********************/

    //�n�l�g�o�V�̓���
    void FeatherFlying()
    {
    }

    //�c�o���Ԃ��̓���
    void SwallowReturn()
    {
    }

    //�X�g���C�N&back�̓���
    void StrikeBack()
    {
        int count = 0;
        //�Z������
        float x ;
        float y ;
        //���݈ʒu���Z��������ɋL�^
        Vector2 posi = this.transform.position;

        //��x�O�i���ċA��ꍇ�����邽�߈ʒu��ێ�
        x = posi.x;
        y = posi.y;
        Debug.Log("�ێ��̓��e" + x + y);

        //�����őO�i����
        if (count < 1)
        {
            Debug.Log("�J�E���g" + count);
            posi.y = 2.0f + Time.deltaTime;
            this.transform.position = posi;
            count++;
        }
            //�����Z�̃{�^����2�񉟂�����ȑO�L�^�����ꏊ�֖߂�
            if (player.stBackFlg)
            {
                posi.x = x;
                posi.y = y;
                Debug.Log("���݂̃|�W�V������" + posi.x + posi.y);
                //����������
                player.stBackFlg = false;
                technicalNomber = 0;
            }
        
    }

    //�g�b�V���̓���
    void Rush()
    {
    }
}
