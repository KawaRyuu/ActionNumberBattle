using UnityEngine;
using UnityEngine.UIElements;


//�A�C�e���̎�ނƏ����̃N���X
//�v���C���[�̃X�e�[�^�X�Q��
public class Item
{
    PlayerData Pl;

    /*�A�C�e�������F������ނ̃A�C�e���͏����s�\*/

    /*�A�C�e���������Ɏg���ϐ�*/
    public float speed_up_timer = 0.0f;//�֐�Speed_Up�Ŏg�p����^�C�}�[

    /*�A�C�e���̏���������֐�*/

    //�����I�Ƀ����_���D�悪�Ⴂ���֓K�p�����B
    public int Change_min(int []num,int size)//�����F�l���Ƃ̐����̃f�[�^�����Ă���z��A�z��̃T�C�Y
    {
        int min = 9;
        for(int i = 0; i <= size;i++)
        {
            if (num[i] < min)//�z��a��i�Ԗڂ�min��菬���������Ȃ�
                min= i;//min�ɔz��ԍ�i��������
        }

        //��ԏ����������������Ă����z��ԍ���Ԃ�
        return min;
        /*
         * �����F���̃A�C�e���g�p�������
         * ���g�̐�����ύX����A�C�e�����g�p������
         * �Ԃ����z��ԍ����ŏ��ł͂Ȃ��l�ɂȂ�\��������
         * 
         *�������āF�����ύX����A�C�e���g�p���ɂ�����x���̊֐����Ăяo��
         */

    }

    //4���̐��̂����A��ԒႢ�����P�`�X�Ƀ����_���ŕς��B
    public void NUM_Random(int[] num, int size)
    {
        //��ԒႢ�����������Ă���z��ԍ����擾(min�ɑ��)
        int min = Change_min(num,size);
        num[min] = Random.Range(1, 10);
    }

    //4���̐��̒���1�ȊO�̏���������1��1�ɕς��B
    public void NumMin_One(int[] num, int size)
    {
        //��ԒႢ�����������Ă���z��ԍ����擾(min�ɑ��)
        int min = Change_min(num, size);
        num[min] = 1;

        /*���ӓ_�A�܂��P���P�ɕς���Script�ɂȂ��Ă��܂�*/
    }

    //���g�̐����������_���Ȑl�ƌ����i�������Ă���l���m�͌�����j
    public void Change_Random()
    {
        
    }

    //���g�ړ����xUP�i3�b�ԁj
    /*�A�C�e���g�����Ƃ��Ɉړ����x�A�b�v�̊֐��������Ă���*/
    public void Item_Speed_Up()
    {
        Pl.Speed = 300.0f;
        speed_up_timer += Time.deltaTime;
        //�ʂ̃A�C�e���g�p���Ƀo�O�N����\������
        if (speed_up_timer > 3.0f)
            Pl.Speed = 5.0f;

    }

    //����ɓ�����΂��Ĉړ����xDown�i2�b�ԁj
    /*PlayerData�ɓݑ��������聨�ݑ��^�O�����I�u�W�F�N�g�����v���O������*/
    public void Item_SpDown()
    {
        //plefab�������v���C���[�������Ă�������ɔ���(3�}�X���i�񂾂������)
        //plefab����

        //��������plefab���������ɔ���
        //�R�}�X�i�񂾂�delete����Fif��
    }

    //�ꌂ�C��̃A�C�e��
    public void Critical()
    {
        Pl.Attack = 3;
    }

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
