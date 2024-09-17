using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    //プレイヤーに触れられたらこのオブジェクトを削除
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Destroy(gameObject);
        }


    }
}
