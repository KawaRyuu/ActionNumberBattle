using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GimmickManager : MonoBehaviour
{
    List<GameObject> gimmicks = new List<GameObject>();

    public void Create(GameObject gimmick,Vector3 position)
    {
        Instantiate(gimmick,position, Quaternion.identity);
        //gimmicks.Add(gimmick);
    }
}
