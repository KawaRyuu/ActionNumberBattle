using GimmickInfomation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : BaseGimmick
{
    const float star_speed = 15.0f;

    private void Start()
    {
        GimmickInitialize(star_speed, GIMMICK_ID.KITE);
    }

    private void Update()
    {
        GimmickUpdate();
    }

    public override void DecideGimmckDirection()
    {
        
    }

    public override void GimmickMove()
    {
       
    }

}
