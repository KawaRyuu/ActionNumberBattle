using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Fonts : MonoBehaviour
{
    TechnicalData tec;

    //������\��
    public Text SelectionFonts;                 //�w������
    public Text OnePlayer_Tec_Select_F;         //1Player�̋Z1�̑I�𕶎�



    // Start is called before the first frame update
    void Start()
    {
        tec = GetComponent<TechnicalData>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectionFonts.text = "4�̒�����2���Z��I��ŉ������I�I";

        //�I�񂾋Z�ɂ���ĕ����\�L�����򂷂�
        //if(1Player&&OneTecFlg)
        //{
        OnePlayer_Tec_Select_F.text = "1P�̋Z�͑I��ł܂���B";
        //switch (tec.technicalNumber)
        //{
        //    case 1:
        //        OnePlayer_Tec_Select_F.text = "1P�̋Z"+"��"+":�n�l�g�o�V(����)";
        //        break;

        //    case 2:
        //        OnePlayer_Tec_Select_F.text = "1P�̋Z" + "��" + ":�c�o���Ԃ�(����)";
        //        break;
        //    case 3:
        //        OnePlayer_Tec_Select_F.text = "1P�̋Z" + "��" + ":�X�g���C�N&back(����)";
        //        break;
        //    case 4:
        //        OnePlayer_Tec_Select_F.text = "1P�̋Z" + "��" + ":�g�b�V��(����)";
        //        break;
        //}
        //}
    }
}
