using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappears : MonoBehaviour
{
    //3�b��ɍ폜������ݒ�
    public float deleteTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //�j��(���̃X�N���v�g�����Ă���ob��deleteTime��ɔ���)
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
