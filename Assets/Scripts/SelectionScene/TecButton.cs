using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TecButton : MonoBehaviour
{
    public int public_number = 0;
    public bool Tec1Flg = false;
    public bool Tec2Flg = false;
    public bool Tec3Flg = false;
    public bool Tec4Flg = false;
    public bool PushButtonFlg = false;
    public int TecFontsNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        Tec1Flg = false;
        Tec2Flg = false;
        Tec3Flg = false;
        Tec4Flg = false;
        PushButtonFlg = false;
        TecFontsNum = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�{�^��������������s����֐��@���s���邽�߂ɂ̓{�^���֊֐��o�^���K�v
    public void Push_Button(int number)
    {
        public_number = number;
        PushButtonFlg = true;       //�{�^����������true�ɂ��A
                                    //fonts��switch�ŏ�����1�T�I�������false��
    }
}
