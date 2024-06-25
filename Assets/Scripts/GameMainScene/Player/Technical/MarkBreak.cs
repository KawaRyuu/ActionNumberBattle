using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBreak : MonoBehaviour
{
    //Player�X�N���v�g���Q�Ƃ���
    Player player;

    //10�b��ɍ폜������ݒ�
    public float deleteTime = 10.0f;
    //�j��t���O
    bool destroyFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        destroyFlg = false;
       
        //�j��(���̃X�N���v�g�����Ă���ob��deleteTime��ɔ���)
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        //�o�b�N�t���O��true�Ȃ�
        if (player.stBackFlg)
        {
            destroyFlg = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�����}�[�N��Player���G�ꂽ�Ȃ�
        if(collision.gameObject.tag=="Player")
        {
            //�j��t���O���擾�ς݂Ȃ�
            if (destroyFlg)
            {
                Debug.Log("�j��");
                //�}�[�N�j��
                Destroy(gameObject);
            }
        }
    }
}
