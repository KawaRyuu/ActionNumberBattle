using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class Fonts : MonoBehaviour
{
    TecButton tecButton;
    TechnicalData tecD;

    //������\��
    public Text SelectionFonts;                 //�w������
    public Text OnePlayer_Tec_Select_F;         //1Player�̋Z1�̑I�𕶎�
    public Text OnePlayer_Tec_Select_F2;         //1Player�̋Z2�̑I�𕶎�

    bool oneTecFlg = false;
    bool twoTecFlg = false;
    int TecSelectionCoverNum = 0;               //�Z��I�񂾍�1�T�ڂ�number��ێ�
    float Pre_start_time = 6.0f;                //����(mainScene)�ɔ�΂��J�E���g�_�E��

    public  int Tec1 = 0;                       //�Z�I��g1�̋Z�̏��ۑ���Num
    public  int Tec2 = 0;                       //�Z�I��g2�̋Z�̏��ۑ���Num

    // Start is called before the first frame update
    void Start()
    {
        tecButton = GetComponent<TecButton>();
        tecD = GetComponent<TechnicalData>();
        oneTecFlg = false;
        twoTecFlg = false;
        TecSelectionCoverNum = 0;
        Pre_start_time = 6.0f;
        Tec1 = 0;
        Tec2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�I�񂾋Z�ɂ���ĕ����\�L�����򂷂�
        //if(1Player&&OneTecFlg)
        //{

        //����2�T���Ă��Ȃ��Ȃ�
        if (!twoTecFlg)
        {
            SelectionFonts.text = "4�̒�����2���Z��I��ŉ������I�I";

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
                        Tec1 = tecButton.public_number;                //�Z1�I��ێ�
                        OneTec();
                    }
                    //�����P�T�ڊ��{�^���������ꂽ��
                    else if (oneTecFlg && tecButton.PushButtonFlg)
                    {
                        OnePlayer_Tec_Select_F2.text = "1P�̋Z" + 2 + ":�n�l�g�o�V(����)";
                        Tec2 = tecButton.public_number;                //�Z2�I��ێ�
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
                    tecButton.Tec2Flg = true;
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
                    tecButton.Tec3Flg = true;
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
                    tecButton.Tec4Flg = true;
                    break;
            }
        }
        else
        {
            //�J�E���g�_�E��
            Pre_start_time -= Time.deltaTime;
            SelectionFonts.text = "�܂��Ȃ��������J�n���܂��B                          "+ (int)Pre_start_time;
            Invoke("GotoGameMainScene", 6.0f);
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
            //2�Z�I������
            twoTecFlg = true;
            UnityEngine.Debug.Log("�Z�I���I��");
        }
    }
    //�Z��������ۂ̊֐�
    void Cover()
    {
        OnePlayer_Tec_Select_F2.text = "�Z�����܂����B                                   ������x�I�����Ă��������B";
    }

    //�Q�[�����C���̉�ʂփV�[���؂�ւ�
    void GotoGameMainScene()
    {
        SceneManager.LoadScene("GameMain");
    }
}
