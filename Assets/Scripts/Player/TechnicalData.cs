using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnicalData : MonoBehaviour
{
    int technicalNomber1 = 0;            //��ڂ̋Z�̋�ʂ���Number
    int technicalNomber2 = 0;            //��ڂ̋Z�̋�ʂ���Number



    // Start is called before the first frame update
    void Start()
    {
        //������
        technicalNomber1 = 0;
        technicalNomber2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�Z�I�����̋�ʎ󂯎��
        switch (technicalNomber1)
        {
            //�Z1 ���́u�n�l�g�o�V�v
            case 1:
                FeatherFlying();        //�Z1�̊֐����Ăяo��
                break;

                //�Z2 ���́u�c�o���Ԃ��v
            case 2:
                SwallowReturn();
                break;

                //�Z3 ���́u�X�g���C�N��back�v
            case 3:
                break;

                //�Z4 ���́u�g�b�V���v
            case 4:
                break;

                //�Z�̑I���������I�΂Ȃ������������_���ŏ�������(��)
            case 0:
                break;
            }
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
}
