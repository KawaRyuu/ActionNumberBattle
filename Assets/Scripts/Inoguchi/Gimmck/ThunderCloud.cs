using GimmickInfomation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCloud : BaseGimmick
{
    const float thunder_cloud_speed = 3.0f;

    private void Start()
    {
        GimmickInitialize(thunder_cloud_speed, GIMMICK_ID.THUNDERCLOUD);

        //i‚Ş•ûŒü‚ğŒˆ‚ß‚é
        DecideGimmckDirection();
    }

    private void Update()
    {
        GimmickUpdate();
    }
}
