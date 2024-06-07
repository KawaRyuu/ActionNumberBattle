using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerTextManager : MonoBehaviour
{
    //�Q��
    NumberData numD;
    TechnicalData tecD;

    /***Player�������Ă��鐔���̕\��***/
    public Text MyNnm1;                  //UI�̕\��
    public Text MyNnm2;                  //UI�̕\��

    /***Player�������Ă���Z�̑҂����ԕ\��***/
    public Text TecCool1;                //�Z1�̃N�[���^�C���\��
    public Text TecCool2;                //�Z2�̃N�[���^�C���\��


    // Start is called before the first frame update
    void Start()
    {
        //���݂�"1P�̂Ƃ���ɂ���"�X�N���v�gData�������Ă��邽�ߒ���
        numD = GameObject.Find("Player").GetComponent<NumberData>();
        tecD = GameObject.Find("Player").GetComponent<TechnicalData>();
    }

    // Update is called once per frame
    void Update()
    {
        //�����̐���Unity�̉�ʂŕ\��
        MyNnm1.text = numD.GetData(0).ToString()
            + (" �E ") + numD.GetData(1).ToString();

        MyNnm2.text = numD.GetData(2).ToString()
            + (" �E ") + numD.GetData(3).ToString();

        /**************�Z�̃N�[���^�C��*************************/
        //�����\��
        TecCool1.text = tecD.GetCoolTime1()+ "�b";
        TecCool2.text = tecD.GetCoolTime2() + "�b";
    }
}
