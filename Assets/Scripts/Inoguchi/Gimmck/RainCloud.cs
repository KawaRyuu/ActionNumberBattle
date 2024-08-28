using GimmickInfomation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloud : BaseGimmick
{
    const float rain_cloud_speed = 3.0f;

    private void Start()
    {
        GimmickInitialize(rain_cloud_speed, GIMMICK_ID.RAINCLOUD);

        //i‚Ş•ûŒü‚ğŒˆ‚ß‚é
        DecideGimmckDirection();
    }

    private void Update()
    {
        GimmickUpdate();
    }
}
