using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�A�C�e���̎�ނƏ����̃N���X
//�v���C���[�̃X�e�[�^�X�Q��
public class Item
{
    //�ǂ̃A�C�e�����������Ă��邩�̕ϐ�


    //�A�C�e���̏���������֐�

    //�A�C�e���Ŗ��G�ł͂Ȃ����A�����I�Ƀ����_���D�悪�Ⴂ���֓K�p�����B
    public void Change_min(){}

    //4���̐��̂����A��ԒႢ�����P�`�X�Ƀ����_���ŕς��B
    public void NUM_Random() {}

    //4���̐��̒���1�ȊO�̏���������1��1�ɕς��B
    public void NumMin_One() {}

    //���g�̐����������_���Ȑl�ƌ����i�������Ă���l���m�͌�����j
    public void Change_Random() { }

    //���g�ړ����xUP�i3�b�ԁj
    public void Speed_Up() { }

    //����ɓ�����΂��Ĉړ����xDown�i2�b�ԁj
    public void SpDown() { }

    //�ꌂ�C��̃A�C�e��
    public void Critical() {  }	        

}

public class ItemSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
