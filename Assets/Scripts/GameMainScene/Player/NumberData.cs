using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class NumberData : MonoBehaviour
{
    PlayerData pD;

    //���g�������Ă���Number��z��ŕێ�
    public int []MyNumber = {0,0,0,0};
    
    // Start is called before the first frame update
    void Start()
    {
        pD = GameObject.Find("1Player").GetComponent<PlayerData>();

        //�Q�[���J�n���Ɠ����ɐ���1�`9�̃����_���Ŏ擾����
        for (int i = 0; i < 4; i++)
        {
                MyNumber[0 + i] = Random.Range(1, 10);
        }
        Debug.Log("���݂̐���" + MyNumber[0] + MyNumber[1] + 
            MyNumber[2] + MyNumber[3]);
    }

    // Update is called once per frame
    void Update()
    {
        //������A�C�e���g�p���ɖ��x����ւ��B
        Sort();
    }

    //�\�[�g�֐�
    void Sort()
    {
        int num = 0;
        for(int i=0;i<3;i++)
        {
            for(int j =i+1;j<4;j++)
            {
                //�z���i�Ԗڂ̐����z��ԍ�j�̐����傫���Ȃ����ւ���
                if (MyNumber[i] < MyNumber[j])
                {
                    //��������MyNumberi�Ԗڂ̐���num�ɓ����B
                    num = MyNumber[i];

                    //�z��i�Ԗڂ̒��ɔz��ԍ�j������B
                    MyNumber[i] = MyNumber[j];

                    //�z��ԍ�j��num(i�ԍ��̐�)�����ă\�[�g����
                    MyNumber[j] = num; 
                }
            }
        }
    }

    //�󂯓n���֐�
    public int GetData(int num)
    {
        return MyNumber[num];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���񂱂肶���");
        if (collision.gameObject.tag == "Player" &&pD.Swaps_Flg)
        {
            Debug.Log("����");
            //�G�ꂽ�����NumberData���擾
            NumberData num_data = collision.gameObject.GetComponent<NumberData>();
            PlayerData player_data = collision.GetComponent<PlayerData>();
            //���肪�C�₵���Ȃ�
            if (player_data.Swoon_Flg)
            {
                int num = Random.Range(0, 4);           //�z��ԍ��������_���őI��
                int tmp = 0;                            //����������ۂ̔�
                tmp = num_data.MyNumber[num];           //�����MyNumber�̐���tmp�ɓ����
                num_data.MyNumber[num] = MyNumber[3];   /*����̃����_���őI�΂ꂽ�z��ԍ���
            �����̍ł�����������n���B�i�����j*/
                MyNumber[3] = tmp;                      //����̐����������̏��֓����B
                pD.Swaps_Flg = false;                   //�����t���O��OFF�ɂ���B
            }
        }
    }
}