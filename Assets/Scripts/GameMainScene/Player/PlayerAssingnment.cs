using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//���̃X�N���v�g�͊e�v���C���[�ɐ������蓖�Ă�}�l�W�����g�ł���
public class PlayerAssingnment : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] Text[] texts = new Text[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //�v���C���[����p�����󂯓n������
    public void PlayerAssingnmentProcess()
    {
        //Player��index�̐��ɂ��Player����P���𔻕ʂ�������̂�u��
        switch (playerInput.user.index)
        {
            //1p
            case 0:
                
                break;

            //2p
            case 1:
                break;

            //3p
            case 2:
                break;

            //4p
            case 3:
                break;
        }
    }
   
}
