using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TecButton : MonoBehaviour
{
    //�Q��
    TechnicalData tec;

    public GameObject Tec1;
    public GameObject Tec2;
    public GameObject Tec3;
    public GameObject Tec4;

    // Start is called before the first frame update
    void Start()
    {
        tec.GetComponent<TechnicalData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�{�^��������������s����֐��@���s���邽�߂ɂ̓{�^���֊֐��o�^���K�v
    public void Push_Button()
    {
        //�����Z1�Ȃ�Z1�̃f�[�^������
        if (Tec1)
            tec.technicalNumber = 1;

        //�����Z2�Ȃ�Z2�̃f�[�^������
        if (Tec2)
            tec.technicalNumber = 2;

        //�����Z3�Ȃ�Z3�̃f�[�^������
        if (Tec3)
            tec.technicalNumber = 3;

        //�����Z4�Ȃ�Z4�̃f�[�^������
        if (Tec1)
            tec.technicalNumber = 4;
    }
}
