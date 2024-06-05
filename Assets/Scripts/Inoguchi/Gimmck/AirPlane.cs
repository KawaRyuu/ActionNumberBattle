using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GimmickInfomation;

public class AirPlane : BaseGimmick
{
    const float airplane_speed = 12.0f;

    private void Start()
    {
        GimmickInitialize(airplane_speed, GIMMICK_ID.AIRPLANE);
    }

    private void Update()
    {
        GimmickUpdate();
    }

 
}
