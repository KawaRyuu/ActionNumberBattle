using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public Text   TextCountDown;                  //UI�̕\��
           int    CountDownTime           = 4;    //��
           float  CountDownSecond         = 60f;  //�b
           bool   CountUpFlg              = false;


    // Start is called before the first frame update
    void Start()
    {
        //������
        CountDownTime = 4 - 1;      //���߂Ɉ���
        CountDownSecond = 60f;
        CountUpFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        //����CountUpFlg��false�Ȃ�
        if (!CountUpFlg)
        {
            //����"��"��0���ȏ�Ȃ�
            if (CountDownTime > 0)
            {
                //�J�E���g�_�E��
                CountDownSecond -= Time.deltaTime;

                //�����b����0�Ȃ�

                if (CountDownSecond <= 0)
                {
                    CountDownTime--;
                    CountDownSecond = 60f;      //60�b�ǉ�
                }

                //�����\��
                TextCountDown.text = "�c��" + CountDownTime + "��"
                  + (int)CountDownSecond + "�b";
            }
            else
            {
                //�����c��b����0���傫���Ȃ�
                if(CountDownSecond >0)
                {
                    //�J�E���g�_�E��
                    CountDownSecond -= Time.deltaTime;
                }
                else
                {
                    CountDownSecond = 0.0f;
                    CountUpFlg = true;
                    Invoke("SceneChange", 1.5f);
                }
                //�����\��
                TextCountDown.text = "�c��" + (int)CountDownSecond + "�b";
            }
        }
    }

    //�V�[���؂�ւ�
    void SceneChange()
    {
        SceneManager.LoadScene("Result");
    }

    public int GetCountDownTime()
    {
        return CountDownTime;
    }
}
