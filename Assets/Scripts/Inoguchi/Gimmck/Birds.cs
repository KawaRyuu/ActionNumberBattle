using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;

public class Birds : BaseGimmick
{
    const float bird_speed = 7.0f;

    private void Start()
    {
        GimmickInitialize(bird_speed, GIMMICK_ID.BIRD);
    }

    private void Update()
    {
        GimmickUpdate();
    }

    public override void GimmickInitialize(float speed, GIMMICK_ID id)
    {
        base.GimmickInitialize(speed, id);

        DecideGimmckDirection();
    }

  
}
