using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerTextManager : MonoBehaviour
{
    //�Q��
    NumberData numD;
    TechnicalData tecD;
    PlayerInput playerInput;

    [SerializeField] Player[] players = new Player[2];

    /***Player�������Ă��鐔���̕\��***/
    public Text MyNnm1;                  //UI�̕\��
    public Text MyNnm2;                  //UI�̕\��

    /***Player�������Ă���Z�̑҂����ԕ\��***/
    public Text TecCool1;                //�Z1�̃N�[���^�C���\��
    public Text TecCool2;                //�Z2�̃N�[���^�C���\��


    ////Player(2p���}���u)
    //public Text p2MyNum1;
    //public Text p2MyNum2;
    //public Text p2TecCool1;
    //public Text p2TecCool2;

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




        //p2MyNum1.text = numD.GetData(0).ToString()
        //    + (" �E ") + numD.GetData(1).ToString();
        //p2MyNum2.text = numD.GetData(2).ToString()
        //    + (" �E ") + numD.GetData(3).ToString();

        //p2TecCool1.text = tecD.GetCoolTime1() + "�b";
        //p2TecCool2.text = tecD.GetCoolTime2() + "�b";
    }
}
