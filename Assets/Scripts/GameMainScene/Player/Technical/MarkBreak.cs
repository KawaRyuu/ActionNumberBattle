using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBreak : MonoBehaviour
{
    //Player�X�N���v�g���Q�Ƃ���
    Player player;

    //10�b��ɍ폜������ݒ�
    public float deleteTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        //�j��(���̃X�N���v�g�����Ă���ob��deleteTime��ɔ���)
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        //�����X�g���C�N&back���I�������}�[�N�j��
        if (player.stBackFlg)
        {
            Debug.Log("�j��");
            Destroy(gameObject);
        }
    }
   
}
