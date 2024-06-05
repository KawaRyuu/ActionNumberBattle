using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;


public class Kite : BaseGimmick
{
    Vector3     kite_velocity = Vector3.zero;  //�����L�̗�
    float       kite_angle    = 0.0f;          //�p�x
    const float kite_speed    = 5.0f;          //���̑��x

    private void Start()
    {
        GimmickInitialize(kite_speed, GIMMICK_ID.KITE);

      
    }

    private void Update()
    {
        GimmickUpdate();
    }

  

    public override void GimmickMove()
    {
        //�M�~�b�N�Ƃ��Ẵx�N�g��
        gimmick_velocity = transform.right * gimmick_speed;

        ///�J�C�g���L�̍��E�ɗh���
        kite_velocity = transform.up * Mathf.Sin(kite_angle) * gimmick_speed;
        kite_angle += 0.01f;

        //�M�~�b�N�ƃJ�C�g���L�����킹���x�N�g��
        this.transform.position += (gimmick_velocity + kite_velocity) * Time.deltaTime;
    }


}
