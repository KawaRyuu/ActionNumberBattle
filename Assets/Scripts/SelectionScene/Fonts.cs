using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Fonts : MonoBehaviour
{
    TecButton tecButton;

    //������\��
    public Text SelectionFonts;                 //�w������
    public Text OnePlayer_Tec_Select_F;         //1Player�̋Z1�̑I�𕶎�
    public Text OnePlayer_Tec_Select_F2;         //1Player�̋Z2�̑I�𕶎�

    bool oneTecFlg = false;
    bool twoTecFlg = false;
    int TecSelectionCoverNum = 0;               //�Z��I�񂾍�1�T�ڂ�number��ێ�

    // Start is called before the first frame update
    void Start()
    {
        tecButton = GetComponent<TecButton>();
        oneTecFlg = false;
        twoTecFlg = false;
        TecSelectionCoverNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SelectionFonts.text = "4�̒�����2���Z��I��ŉ������I�I";

        //�I�񂾋Z�ɂ���ĕ����\�L�����򂷂�
        //if(1Player&&OneTecFlg)
        //{

        //����2�T���Ă��Ȃ��Ȃ�
        if (!twoTecFlg)
        {
            switch (tecButton.public_number)
            {
                case 0:
                    OnePlayer_Tec_Select_F.text = "1P�̋Z1�͑I��ł܂���B";
                    OnePlayer_Tec_Select_F2.text = "1P�̋Z2�͑I��ł܂���B";
                    break;
                case 1:
                    //����1�T���炵�ĂȂ��Ȃ�
                    if (!oneTecFlg)
                    {
                        OnePlayer_Tec_Select_F.text = "1P�̋Z" + 1 + ":�n�l�g�o�V(����)";
                        TecSelectionCoverNum = tecButton.public_number;//�ێ�
                        OneTec();
                    }
                    //�����P�T�ڊ��{�^���������ꂽ��
                    else if (oneTecFlg && tecButton.PushButtonFlg)
                    {
                        OnePlayer_Tec_Select_F2.text = "1P�̋Z" + 2 + ":�n�l�g�o�V(����)";
                        TwoTec();
                    }
                   
                    break;

                case 2:
                    //����1�T���炵�ĂȂ��Ȃ�
                    if (!oneTecFlg)
                    {
                        OnePlayer_Tec_Select_F.text = "1P�̋Z" + 1 + ":�c�o���Ԃ�(����)";
                        TecSelectionCoverNum = tecButton.public_number;//�ێ�
                        OneTec();
                    }
                    //�����P�T�ڊ��{�^���������ꂽ��
                    else if (oneTecFlg && tecButton.PushButtonFlg)
                    {
                        OnePlayer_Tec_Select_F2.text = "1P�̋Z" + 2 + ":�c�o���Ԃ�(����)";
                        TwoTec();
                    }
                    break;

                case 3:
                    //����1�T���炵�ĂȂ��Ȃ�
                    if (!oneTecFlg)
                    {
                        OnePlayer_Tec_Select_F.text = "1P�̋Z" + 1 +
                            ":�X�g���C�N&back(����)";
                        TecSelectionCoverNum = tecButton.public_number;//�ێ�
                        OneTec();
                    }
                    //�����P�T�ڊ��{�^���������ꂽ��
                    else if (oneTecFlg && tecButton.PushButtonFlg)
                    {
                        OnePlayer_Tec_Select_F2.text = "1P�̋Z" + 2 +
                            ":�X�g���C�N&back(����)";
                        TwoTec();
                    }                    
                    break;

                case 4:
                    //����1�T���炵�ĂȂ��Ȃ�
                    if (!oneTecFlg)
                    {
                        OnePlayer_Tec_Select_F.text = "1P�̋Z" + 1 + ":�g�b�V��(����)";
                        TecSelectionCoverNum = tecButton.public_number;//�ێ�
                        OneTec();
                    }
                    //�����P�T�ڊ��{�^���������ꂽ��
                    else if (oneTecFlg && tecButton.PushButtonFlg)
                    {
                        OnePlayer_Tec_Select_F2.text = "1P�̋Z" + 2 + ":�g�b�V��(����)";
                        TwoTec();
                    }
                    break;
            }
        }
    }

    //�Z�I��1�T�ڂ̃t���O�֐�
    void OneTec()
    {
        oneTecFlg = true;                   //1�T�̃t���OON
        tecButton.PushButtonFlg = false;    //�����߂�
    }
    //�Z�I��2�T�ڂ̃t���O�֐�
    void TwoTec()
    {
        //����2�T�ڂ�I�������۔������
        if (TecSelectionCoverNum == tecButton.public_number)
        {
            Cover();       //������ۂ̏����֐���
        }
        else
        {
            twoTecFlg = true;
            UnityEngine.Debug.Log("�Z�I���I��");
        }
    }
    //�Z��������ۂ̊֐�
    void Cover()
    {
        OnePlayer_Tec_Select_F2.text = "�Z�����܂����B                                   ��蒼���Ă��������B";
    }
}
