using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class NumberData : MonoBehaviour
{
    //���g�������Ă���Number
    public int []MyNumber = {0,0,0,0};

    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���J�n���Ɠ����ɐ��������_���Ŏ擾����
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
        
    }
}
