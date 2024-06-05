using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;

//�M�~�b�N�̐e�I�u�W�F�N�g
public class BaseGimmick : MonoBehaviour 
{
    protected GIMMICK_ID     gimmick_id;                        //���ʎq
    protected SpriteRenderer gimmick_sprite;                    //�`��R���|�[�l���g
    protected Vector3        gimmick_position = Vector3.zero;   //�ʒu���W
    protected Vector3        gimmick_velocity = Vector3.zero;   //�����x
    protected Vector3        gimmick_direction= Vector3.zero;   //����
    protected float          gimmick_speed    = 1.0f;           //�ړ����x
    protected bool           destroy_flag     = false;          //�j�󔻒�t���O
    

    //������
    public virtual void GimmickInitialize(float speed,GIMMICK_ID id)
    {
        //�e�평����
        gimmick_position    = this.transform.position;
        gimmick_sprite      = this.GetComponent<SpriteRenderer>();
        gimmick_velocity    = Vector3.zero;
        gimmick_direction   = Vector3.zero;
        gimmick_speed       = speed;
        gimmick_id          = id;
        destroy_flag        = false;

        //�i�ޕ��������߂�
        DecideGimmckDirection();
    }

    //�X�V
    public virtual void GimmickUpdate()
    {
        GimmickMove();
        CheckOffScreen();
    }

    

    //��ʊO����
    public  void CheckOffScreen()
    {
        


      
    }

    //�M�~�b�N�̐i�ތ���(����)�����߂�
   public virtual void DecideGimmckDirection()
    {
        //��ʃT�C�Y
        Vector2 screen_size = new Vector3(Screen.width, Screen.height);

        //��ʃT�C�Y�ƃM�~�b�N(���g)�̍��W���r���[�|�[�g���W�ɕϊ�
        Vector3 screen_view_position = Camera.main.ScreenToViewportPoint(screen_size);
        Vector3 gimmick_view_position = Camera.main.WorldToViewportPoint(gimmick_position);

        //��ʘg��̈�ӏ��̍��W
        Vector3 screen_frame_position = Vector3.zero;

        //�M�~�b�N�Ɖ�ʂ̔�r�����ʒu�ɉ����Ď擾����g�����߂�
        if (gimmick_view_position.x <= 0)
        {
            //����

            //��ʘg�E�̈�_�������_���擾
            screen_frame_position.x = 1;
            screen_frame_position.y = Random.value;

        }
        else if (gimmick_view_position.x >= screen_view_position.x)
        {
            //�E��

            //��ʘg���̈�_�������_���擾
            screen_frame_position.x = 0;
            screen_frame_position.y = Random.value;

        }
        else if (gimmick_view_position.y <= 0)
        {
            //�㑤

            //��ʘg���̈�_�������_���擾
            screen_frame_position.x = Random.value;
            screen_frame_position.y = 1;
        }
        else if (gimmick_view_position.y >= screen_view_position.y)
        {
            //����

            //��ʘg��̈�_�������_���擾
            screen_frame_position.x = Random.value;
            screen_frame_position.y = 0;
        }

        //�擾������ʘg���W����i�ތ������Z�o
        gimmick_direction = Camera.main.ViewportToWorldPoint(screen_frame_position) - gimmick_position;

        //��ʘg�ƃM�~�b�N��z���W�͓����Ƃ���
        gimmick_direction.z = 0;

        //���߂������ɃM�~�b�N������
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, gimmick_direction);

    }


    public virtual void GimmickMove()
    {
        gimmick_velocity = transform.right * gimmick_speed;

        this.transform.position = gimmick_velocity * Time.deltaTime;
    }
   


    //�M�~�b�N��ID���擾
    public GIMMICK_ID GetGIMMICK_ID()
    {
        return gimmick_id;
    }


}
